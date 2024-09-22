using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Common
{
    /// <summary>
    /// Csv 模板类, 帮助解析 CSV 文件用
    /// </summary>
    public class CsvTemplate
    {
        /// <summary>
        /// 表头行起始行号, 行号从 1 开始计数
        /// </summary>
        public int HeaderRowStartNumber { set; get; } = 0;
        /// <summary>
        /// 表头行结束行号, 行号从 1 开始计数
        /// </summary>
        public int HeaderRowEndNumber { set; get; } = 0;
        /// <summary>
        /// 表头信息列号, 列号从 1 开始计数, 默认为1
        /// </summary>
        public int HeaderKeyColumnNumber { set; get; } = 1;
        /// <summary>
        /// 表头信息值起始列号, 列号从 1 开始计数, 默认为2
        /// </summary>
        public int HeaderValueColumnNumber { set; get; } = 2;
        /// <summary>
        /// 数据列列名行号, 行号从 1 开始计数
        /// </summary>
        public int ColumnNameRowNumber { set; get; } = 0;
        /// <summary>
        /// 数据行起始行号, 行号从 1 开始计数
        /// </summary>
        public int DataRowStartNumber { set; get; } = 0;
        /// <summary>
        /// X坐标列号
        /// </summary>
        public int XCoordinateColumnNumber { set; get; } = 0;
        /// <summary>
        /// Y坐标列号
        /// </summary>
        public int YCoordinateColumnNumber { set; get; } = 0;
        public string[] ColumnNames { set; get; } = null;
    }
}
