using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Offertech.Util.Ioc;
using System;

namespace Offertech.Data.Repository
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.10.10
    /// 描 述：数据库建立工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="DbType">数据库类型</param>
        /// <returns></returns>
        public static IDatabase Base(string connString, DatabaseType DbType)
        {
            DbHelper.DbType = DbType;
            return UnityIocHelper.DBInstance.GetService<IDatabase>(new ParameterOverride(
              "connString", connString), new ParameterOverride(
              "DbType", DbType.ToString()));
        }
        /// <summary>
        /// 连接基础库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Base()
        {
            DbHelper.DbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), UnityIocHelper.GetmapToByName("DBcontainer", "IDbContext"));
            return UnityIocHelper.DBInstance.GetService<IDatabase>(new ParameterOverride(
             "connString", "BaseDb"), new ParameterOverride(
              "DbType", ""));
        }
    }
}
