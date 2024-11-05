using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Common
{
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
        public int XMax { get; set; }
        public int XMin { get; set; }
        public int YMax { get; set; }
        public int YMin { get; set; }
    }
}
