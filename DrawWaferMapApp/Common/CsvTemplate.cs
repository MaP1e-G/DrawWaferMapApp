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
        public virtual int HeaderRowStartNumber { set; get; } = 0;
        /// <summary>
        /// 表头行结束行号, 行号从 1 开始计数
        /// </summary>
        public virtual int HeaderRowEndNumber { set; get; } = 0;
        /// <summary>
        /// 表头信息列号, 列号从 1 开始计数, 默认为1
        /// </summary>
        public virtual int HeaderKeyColumnNumber { set; get; } = 1;
        /// <summary>
        /// 表头信息值起始列号, 列号从 1 开始计数, 默认为2
        /// </summary>
        public virtual int HeaderValueColumnNumber { set; get; } = 2;
        /// <summary>
        /// 数据列列名行号, 行号从 1 开始计数
        /// </summary>
        public virtual int ColumnNameRowNumber { set; get; } = 0;
        /// <summary>
        /// 数据行起始行号, 行号从 1 开始计数
        /// </summary>
        public virtual int DataRowStartNumber { set; get; } = 0;
        /// <summary>
        /// X 坐标列号, 列号从 1 开始计数
        /// </summary>
        public virtual int XCoordinateColumnNumber { set; get; } = 0;
        /// <summary>
        /// Y 坐标列号, 列号从 1 开始计数
        /// </summary>
        public virtual int YCoordinateColumnNumber { set; get; } = 0;
        /// <summary>
        /// 文件列名
        /// </summary>
        public virtual string[] ColumnNames { set; get; } = null;
        /// <summary>
        /// 列名映射，用于将列名映射到该列的索引
        /// </summary>
        public virtual Dictionary<string, int> ColumnsMap { set; get; } = null;
    }
}
