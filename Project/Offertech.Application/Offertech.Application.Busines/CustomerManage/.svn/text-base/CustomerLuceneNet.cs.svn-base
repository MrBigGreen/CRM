//using log4net;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.Service.CustomerManage;
using Offertech.Util;
using Offertech.Util.Extension;
using Offertech.Util.Log;
using PanGu;
using PanGu.HighLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Offertech.Application.Busines.CustomerManage
{
    public class CustomerLuceneNet
    {
        //private static ILog Logger = LogManager.GetLogger(typeof(CustomerLuceneNet));

        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }



        private ICustomerService service = new CustomerService();

        #region 创建单例
        // 定义一个静态变量来保存类的实例
        private static CustomerLuceneNet uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();


        // 定义私有构造函数，使外界不能创建该类实例
        private CustomerLuceneNet()
        { }

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static CustomerLuceneNet GetInstance()
        {
            // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
            // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            lock (locker)
            {
                // 如果类的实例不存在则创建，否则直接返回
                if (uniqueInstance == null)
                {
                    uniqueInstance = new CustomerLuceneNet();
                }
            }

            return uniqueInstance;
        }
        #endregion

        private Queue<IndexJob> jobs = new Queue<IndexJob>();       //任务队列,保存生产出来的任务和消费者使用,不使用list避免移除时数据混乱问题

        /// <summary>
        /// 任务类,包括任务的Id ,操作的类型
        /// </summary>
        class IndexJob
        {
            public string Id { get; set; }
            public JobType JobType { get; set; }
        }
        /// <summary>
        /// 枚举,操作类型是增加还是删除
        /// </summary>
        enum JobType { Add, Remove }

        #region 任务索引
        /// <summary>
        /// 启动消费者线程
        /// </summary>
        public void CustomerStart()
        {
            PanGu.Segment.Init(PanGuPath);

            Thread threadIndex = new Thread(IndexOn);
            threadIndex.IsBackground = true;
            threadIndex.Start();
        }

        /// <summary>
        /// 索引任务线程
        /// </summary>
        private void IndexOn()
        {
            Logger.Debug("索引任务线程启动");
            while (true)
            {
                if (jobs.Count <= 0)
                {
                    Thread.Sleep(5 * 1000);
                    continue;
                }
                //创建索引目录
                if (!System.IO.Directory.Exists(IndexDic))
                {
                    System.IO.Directory.CreateDirectory(IndexDic);
                }
                FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexDic), new NativeFSLockFactory());
                bool isUpdate = IndexReader.IndexExists(directory);
                Logger.Debug("索引库存在状态" + isUpdate);
                if (isUpdate)
                {
                    //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁
                    if (IndexWriter.IsLocked(directory))
                    {
                        Logger.Debug("开始解锁索引库");
                        IndexWriter.Unlock(directory);
                        Logger.Debug("解锁索引库完成");
                    }
                }
                IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
                ProcessJobs(writer);
                writer.Close();
                directory.Close();//不要忘了Close，否则索引结果搜不到
                Logger.Debug("全部索引完毕");
            }
        }

        private void ProcessJobs(IndexWriter writer)
        {
            while (jobs.Count != 0)
            {
                IndexJob job = jobs.Dequeue();
                writer.DeleteDocuments(new Term("number", job.Id));
                //如果“添加文章”任务再添加，
                if (job.JobType == JobType.Add)
                {
                    CustomerEntity art = service.GetEntity(job.Id);
                    if (art == null)//有可能刚添加就被删除了
                    {
                        continue;
                    }


                    string EnCode = art.EnCode.ToString();
                    string FullName = art.FullName;

                    string CreateDate = art.CreateDate.ToDateString();
                    string CreateUserName = art.CreateUserName;

                    Document document = new Document();
                    //只有对需要全文检索的字段才ANALYZED
                    document.Add(new Field("number", job.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("FullName", FullName, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("EnCode", EnCode, Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("CreateDate", CreateDate, Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("CreateUserName", CreateUserName, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                    writer.AddDocument(document);
                    Logger.Debug("索引" + job.Id + "完毕");
                }
            }
        }
        #endregion

        #region 任务添加
        public void AddCustomer(string customerId)
        {
            IndexJob job = new IndexJob();
            job.Id = customerId;
            job.JobType = JobType.Add;
            Logger.Debug(customerId + "加入任务列表");
            jobs.Enqueue(job);//把任务加入商品库
        }

        public void RemoveCustomer(string customerId)
        {
            IndexJob job = new IndexJob();
            job.JobType = JobType.Remove;
            job.Id = customerId;
            Logger.Debug(customerId + "加入删除任务列表");
            jobs.Enqueue(job);//把任务加入商品库
        }
        #endregion

        #region 从索引搜索结果
        /// <summary>
        /// 从索引搜索结果
        /// </summary>
        public List<CustomerEntity> SearchIndex(string Words, int PageSize, int PageIndex, out int _totalcount)
        {
            _totalcount = 0;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            BooleanQuery bQuery = new BooleanQuery();
            string title = string.Empty;
            title = GetKeyWordsSplitBySpace(Words);
            QueryParser parse = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "FullName", new PanGuAnalyzer());
            Query query = parse.Parse(title);
            parse.SetDefaultOperator(QueryParser.Operator.AND);
            bQuery.Add(query, BooleanClause.Occur.SHOULD);
            dic.Add("FullName", Words);

            if (bQuery != null && bQuery.GetClauses().Length > 0)
            {
                return GetSearchResult(bQuery, dic, PageSize, PageIndex, out _totalcount);
            }
            return null;
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="bQuery"></param>
        private List<CustomerEntity> GetSearchResult(BooleanQuery bQuery, Dictionary<string, string> dicKeywords, int PageSize, int PageIndex, out int totalCount)
        {
            List<CustomerEntity> list = new List<CustomerEntity>();
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexDic), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            Sort sort = new Sort(new SortField("CreateDate", SortField.DOC, true));
            searcher.Search(bQuery, null, collector);
            totalCount = collector.GetTotalHits();//返回总条数
            TopDocs docs = searcher.Search(bQuery, (Filter)null, PageSize * PageIndex, sort);
            if (docs != null && docs.totalHits > 0)
            {
                for (int i = 0; i < docs.totalHits; i++)
                {
                    if (i >= (PageIndex - 1) * PageSize && i < PageIndex * PageSize)
                    {
                        Document doc = searcher.Doc(docs.scoreDocs[i].doc);
                        CustomerEntity model = new CustomerEntity()
                        {
                            CustomerId = doc.Get("number").ToString(),
                            FullName = doc.Get("FullName").ToString(),
                            CreateUserName = doc.Get("CreateUserName").ToString(),
                            CreateDate = DateTime.Parse(doc.Get("CreateDate").ToString()),
                            EnCode = doc.Get("EnCode").ToString()
                        };
                        list.Add(SetHighlighter(dicKeywords, model));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 设置关键字高亮
        /// </summary>
        /// <param name="dicKeywords">关键字列表</param>
        /// <param name="model">返回的数据模型</param>
        /// <returns></returns>
        private CustomerEntity SetHighlighter(Dictionary<string, string> dicKeywords, CustomerEntity model)
        {
            SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            highlighter.FragmentSize = 250;
            string strTitle = string.Empty;
            string strContent = string.Empty;
            dicKeywords.TryGetValue("FullName", out strTitle);
            if (!string.IsNullOrEmpty(strTitle))
            {
                string title = model.FullName;
                model.FullName = highlighter.GetBestFragment(strTitle, model.FullName);
                if (string.IsNullOrEmpty(model.FullName))
                {
                    model.FullName = title;
                }
            }
            return model;
        }
        /// <summary>
        /// 处理关键字为索引格式
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        private string GetKeyWordsSplitBySpace(string keywords)
        {
            PanGuTokenizer ktTokenizer = new PanGuTokenizer();
            StringBuilder result = new StringBuilder();
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }
                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }
            return result.ToString().Trim();
        }
        #endregion


        /// <summary>
        /// 索引存放目录
        /// </summary>
        protected string IndexDic
        {
            get
            {
                return Sys.GetMapPath(Config.GetValue("IndexDicpath"));
            }
        }
        /// <summary>
        /// 盘古分词配置目录
        /// </summary>
        protected string PanGuPath
        {
            get
            {
                return Sys.GetMapPath(Config.GetValue("PanGupath"));
            }
        }
    }
}
