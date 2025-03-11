using DrawWaferMapApp.Common;
using DrawWaferMapApp.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Tools
{
    public class AOICsvProcessTool : CsvProcessTool
    {
        public AOICsvProcessTool() : base() { }
        public AOICsvProcessTool(string pattern) : base(pattern) { }

        public override void ReadCsvFileToDictionary(string filePath, CsvTemplate csvTemplate, CsvDetail csvDetail)
        {
            if (csvTemplate is null || csvDetail is null)
                throw new CsvProcessException("Read csv fail!There is a null input parameter.Please check the input parameters.");

            // 当前读取行的行号，从 1 开始。
            int currentRowNumber = 1;

            try
            {
                // 提前存储模板中的值，避免多次属性访问
                int xColumnIndex = csvTemplate.XCoordinateColumnNumber - 1;
                int yColumnIndex = csvTemplate.YCoordinateColumnNumber - 1;
                int dataRowStartNumber = csvTemplate.DataRowStartNumber;
                csvDetail.BodyInfo = new Dictionary<Coordinate, string[]>();
                string[] currentRow;

                foreach (var line in File.ReadLines(filePath))
                {
                    if (currentRowNumber >= csvTemplate.DataRowStartNumber)
                    {
                        // 数据行用逗号进行分隔
                        currentRow = ParseCsvLine(line);
                        Coordinate currentCoordinate = new Coordinate { X = Convert.ToInt32(currentRow[xColumnIndex]), Y = Convert.ToInt32(currentRow[yColumnIndex]) };
                        csvDetail.BodyInfo.Add(currentCoordinate, currentRow);
                    }
                    currentRowNumber++;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
