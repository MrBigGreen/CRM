using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
     public class  SoundCommentModel
     {
         public string CommentID { get; set; }
         public int TelCode { get; set; }
         public string SoundID { get; set; }
         public string SoundURL { get; set; }
         public string SoundType { get; set; }
         public string SoundGoodRemark { get; set; }
         public string SoundBadRemark { get; set; }
         public string SoundImproveRemark { get; set; }
         public DateTime CreateTime { get; set; }
         public string CreateUserID { get; set; }
         public DateTime UpdateTime { get; set; }
         public string UpdateUserID { get; set; }
    }
}
