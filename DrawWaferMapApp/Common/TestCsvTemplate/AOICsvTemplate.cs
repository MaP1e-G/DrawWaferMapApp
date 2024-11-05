using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Common
{
    public class AOICsvTemplate : CsvTemplate
    {
        // Modify the base class's implements if you need
        //public override int HeaderRowStartNumber { get => base.HeaderRowStartNumber; set => base.HeaderRowStartNumber = value; };
        //public override int HeaderRowEndNumber { get => base.HeaderRowEndNumber; set => base.HeaderRowEndNumber = value; }
        //public override int HeaderKeyColumnNumber { get => base.HeaderKeyColumnNumber; set => base.HeaderKeyColumnNumber = value; }
        //public override int HeaderValueColumnNumber { get => base.HeaderValueColumnNumber; set => base.HeaderValueColumnNumber = value; }
        //public override int ColumnNameRowNumber { get => base.ColumnNameRowNumber; set => base.ColumnNameRowNumber = value; }
        //public override int DataRowStartNumber { get => base.DataRowStartNumber; set => base.DataRowStartNumber = value; }
        //public override int XCoordinateColumnNumber { get => base.XCoordinateColumnNumber; set => base.XCoordinateColumnNumber = value; }
        //public override int YCoordinateColumnNumber { get => base.YCoordinateColumnNumber; set => base.YCoordinateColumnNumber = value; }
        //public override string[] ColumnNames { get => base.ColumnNames; set => base.ColumnNames = value; }

        public AOICsvTemplate()
        {
            DataRowStartNumber = 10;
            HeaderKeyColumnNumber = 1;
            HeaderValueColumnNumber = 3;
            HeaderRowStartNumber = 1;
            HeaderRowEndNumber = 7;
            XCoordinateColumnNumber = 1;
            YCoordinateColumnNumber = 2;
            ColumnNames = new string[] { "POSX", "POSY", "BIN", "ORGBINNO", "VF2", "VF1", "VZ", "IR", "IF1", "LOP1", "WLD", "WLP", "HW", "VF3", "IR2", "IV3" };
            ColumnsMap = new Dictionary<string, int> {
                { "POSX", 0 },
                { "POSY", 1 },
                { "BIN", 2 },
                { "ORGBINNO", 3 },
                { "VF2", 4 },
                { "VF1", 5 },
                { "VZ", 6 },
                { "IR", 7 },
                { "IF1", 8 },
                { "LOP1", 9 },
                { "WLD", 10 },
                { "WLP", 11 },
                { "HW", 12 },
                { "VF3", 13 },
                { "IR2", 14 },
                { "IV3", 15 }
            };
        }
    }
}
