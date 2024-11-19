using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Common
{
    public enum DataStorageType
    {
        Dictionary,
        Matrix,
        Unknow,
    }

    public class CsvDetail
    {
        /// <summary>
        /// 测试档中的数据，以Dictionary形式存储
        /// </summary>
        public Dictionary<Coordinate, string[]> BodyInfo { get; set; }
        /// <summary>
        /// 测试档中的数据，以矩阵形式存储
        /// </summary>
        public string[,][] BodyInfo_Matrix { get; set; }
        /// <summary>
        /// 数据存储的形式
        /// </summary>
        public DataStorageType DataType { get; set; } = DataStorageType.Dictionary;
        public int XMax { get; set; }
        public int XMin { get; set; }
        public int YMax { get; set; }
        public int YMin { get; set; }
    }
}
