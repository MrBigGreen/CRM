using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Common
{   
    /// <summary>
    /// 验证错误
    /// </summary>
    public class ValidationError
    {
        public ValidationError() { }

        public string ErrorMessage { get; set; }
    }
 
    /// <summary>
    /// 多个验证错误的集合
    /// </summary>
    public class ValidationErrors : List<ValidationError>
    {
        public void Add(string errorMessage)
        {
            base.Add(new ValidationError { ErrorMessage = errorMessage });
        }
    }
  
}
