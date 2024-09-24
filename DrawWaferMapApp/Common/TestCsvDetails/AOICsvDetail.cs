using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Common
{
    public class AOICsvDetail : CsvDetail
    {
        public string DataFileName { get; set; }
        public string LotNumber { get; set; }
        public string DeviceNumber { get; set; }
        public string TestTime { get; set; }
        public string MapFileName { get; set; }
        public string TransferTime { get; set; }
        public string[] SortBINFileName { get; set; }
    }
}
