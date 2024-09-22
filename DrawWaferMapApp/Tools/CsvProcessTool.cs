using DrawWaferMapApp.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Tools
{
    class CsvProcessTool
    {
        public string Pattern { get; set; } = @",(?=(?:[^""]*""[^""]*"")*[^""]*$)";  // 默认匹配模式
        private Regex csvSplitRegex;

        public CsvProcessTool()
        {
            csvSplitRegex = new Regex(Pattern, RegexOptions.Compiled);
        }

        public CsvProcessTool(string pattern)
        {
            Pattern = pattern;
            csvSplitRegex = new Regex(Pattern, RegexOptions.Compiled);
        }

        /// <summary>
        /// 读取 CSV 文件, 每一行为一个字符串数组
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<string[]> ReadCsvFile(string filePath)
        {
            List<string[]> result = new List<string[]>();

            try
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    result.Add(ParseCsvLine(line));
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<List<string[]>> ReadCsvFileAsync(string filePath)
        {
            List<string[]> result = new List<string[]>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        result.Add(ParseCsvLine(line));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 利用正则表达式解析 CSV 文件中的一行数据
        /// </summary>
        /// <param name="line"></param>
        /// <returns>解析后的字符串数组</returns>
        private string[] ParseCsvLine(string line)
        {
            string[] result;
            try
            {
                result = csvSplitRegex.Split(line).Select(field => field.Trim('"')).ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 提取已解析的 CSV 文件中的表头信息
        /// </summary>
        /// <param name="csvData"></param>
        /// <param name="csvTemplate"></param>
        /// <returns></returns>
        public Dictionary<string, string[]> GetHeaderInfo(List<string[]> csvData, CsvTemplate csvTemplate)
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();

            try
            {
                if (csvTemplate is null)
                {
                    return null;
                }

                // 提前存储模板中的值，避免多次属性访问
                int headerRowStart = csvTemplate.HeaderRowStartNumber - 1;
                int headerRowEnd = csvTemplate.HeaderRowEndNumber;
                int keyColumnIndex = csvTemplate.HeaderKeyColumnNumber - 1;
                int valueColumnIndex = csvTemplate.HeaderValueColumnNumber - 1;

                for (int i = headerRowStart; i < headerRowEnd; i++)
                {
                    string[] currentRow = csvData[i];
                    int valueColumnsLength = currentRow.Length - valueColumnIndex;
                    string[] valueColumns = new string[valueColumnsLength];
                    Array.Copy(currentRow, valueColumnIndex, valueColumns, 0, valueColumnsLength);  // 使用数组切片代替 LINQ 的 Skip
                    result.Add(currentRow[keyColumnIndex], valueColumns);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Dictionary<string, string[]> GetHeaderInfoParallel(List<string[]> csvData, CsvTemplate csvTemplate)
        {
            var result = new ConcurrentDictionary<string, string[]>();

            try
            {
                if (csvTemplate == null)
                {
                    return null;
                }

                // 提前存储模板中的值，避免多次属性访问
                int headerRowStart = csvTemplate.HeaderRowStartNumber - 1;
                int headerRowEnd = csvTemplate.HeaderRowEndNumber;
                int keyColumnIndex = csvTemplate.HeaderKeyColumnNumber - 1;
                int valueColumnIndex = csvTemplate.HeaderValueColumnNumber - 1;

                // 使用 Parallel.For 来并行处理 CSV 数据
                Parallel.For(headerRowStart, headerRowEnd, i =>
                {
                    string[] currentRow = csvData[i];

                    int valueColumnsLength = currentRow.Length - valueColumnIndex;
                    string[] valueColumns = new string[valueColumnsLength];
                    Array.Copy(currentRow, valueColumnIndex, valueColumns, 0, valueColumnsLength);// 使用数组切片代替 LINQ 的 Skip

                    result.TryAdd(currentRow[keyColumnIndex], valueColumns);
                });

                return new Dictionary<string, string[]>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 提取已解析的 CSV 文件中的表身(单身)信息
        /// </summary>
        /// <param name="csvData"></param>
        /// <param name="csvTemplate"></param>
        /// <returns></returns>
        public Dictionary<Coordinate, string[]> GetBodyInfo(List<string[]> csvData, CsvTemplate csvTemplate)
        {
            Dictionary<Coordinate, string[]> result = new Dictionary<Coordinate, string[]>();

            try
            {
                if (csvTemplate is null)
                {
                    return null;
                }

                // 提前存储模板中的值，避免多次属性访问
                int dataRowStart = csvTemplate.DataRowStartNumber - 1;
                int xColumnIndex = csvTemplate.XCoordinateColumnNumber - 1;
                int yColumnIndex = csvTemplate.YCoordinateColumnNumber - 1;

                for (int i = dataRowStart; i < csvData.Count; i++)
                {
                    string[] currentRow = csvData[i];
                    Coordinate currentCoordinate = new Coordinate { X = Convert.ToInt32(currentRow[xColumnIndex]), Y = Convert.ToInt32(currentRow[yColumnIndex]) };
                    result.Add(currentCoordinate, currentRow);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Dictionary<Coordinate, string[]> GetBodyInfoParallel(List<string[]> csvData, CsvTemplate csvTemplate)
        {
            var result = new ConcurrentDictionary<Coordinate, string[]>();

            try
            {
                if (csvTemplate is null)
                {
                    return null;
                }

                // 提前存储模板中的值，避免多次属性访问
                int dataRowStart = csvTemplate.DataRowStartNumber - 1;
                int xColumnIndex = csvTemplate.XCoordinateColumnNumber - 1;
                int yColumnIndex = csvTemplate.YCoordinateColumnNumber - 1;

                Parallel.For(dataRowStart, csvData.Count, i => 
                {
                    string[] currentRow = csvData[i];
                    Coordinate currentCoordinate = new Coordinate { X = Convert.ToInt32(currentRow[xColumnIndex]), Y = Convert.ToInt32(currentRow[yColumnIndex]) };
                    result.TryAdd(currentCoordinate, currentRow);
                });

                return new Dictionary<Coordinate, string[]>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
