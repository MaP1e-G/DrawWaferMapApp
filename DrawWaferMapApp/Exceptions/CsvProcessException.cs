using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Exceptions
{
    class CsvProcessException : Exception
    {
        // 默认构造函数
        public CsvProcessException() : base() { }

        // 接受自定义错误消息的构造函数
        public CsvProcessException(string message) : base(message) { }

        // 接受自定义错误消息和内部异常的构造函数
        public CsvProcessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
