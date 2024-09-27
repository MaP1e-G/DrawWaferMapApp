using DrawWaferMapApp.Common;
using DrawWaferMapApp.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrawWaferMapApp.Tools
{
    public class CsvProcessTool
    {
        // Property
        public string Pattern { get; set; } = @",(?=(?:[^""]*""[^""]*"")*[^""]*$)";  // 默认匹配模式
        public char SplitChar { get; set; } = ',';  // 默认分隔符

        // Field
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
        public List<string[]> ReadCsvFile(string filePath) => ReadCsvFile(filePath, false);

        public List<string[]> ReadCsvFile(string filePath, bool useRegex)
        {
            List<string[]> result = new List<string[]>();
            try
            {
                if (useRegex)
                {
                    foreach (var line in File.ReadLines(filePath))
                    {
                        result.Add(ParseCsvLineByRegex(line));
                    }
                }
                else
                {
                    foreach (var line in File.ReadLines(filePath))
                    {
                        result.Add(ParseCsvLine(line, SplitChar));
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
        /// 读取 CSV 文件, 并将信息存入到 csvDetail 中
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="csvTemplate"></param>
        /// <returns></returns>
        public virtual void ReadCsvFile(string filePath, CsvTemplate csvTemplate, CsvDetail csvDetail)
        {
            if (csvTemplate is null || csvDetail is null)
                throw new CsvProcessException("Read csv fail!There is a null input parameter.Please check the input parameters.");

            // 当前读取行的行号，从 1 开始。
            int currentRowNumber = 1;
            // 获取公有的、仅在当前类声明的实例属性
            PropertyInfo[] propertyInfos = csvDetail.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            try
            {
                // 提前存储模板中的值，避免多次属性访问
                int keyColumnIndex = csvTemplate.HeaderKeyColumnNumber - 1;
                int valueColumnIndex = csvTemplate.HeaderValueColumnNumber - 1;
                int xColumnIndex = csvTemplate.XCoordinateColumnNumber - 1;
                int yColumnIndex = csvTemplate.YCoordinateColumnNumber - 1;
                // 创建新的 BodyInfo
                csvDetail.BodyInfo = new Dictionary<Coordinate, string[]>();
                string[] currentRow;

                foreach (var line in File.ReadLines(filePath))
                {
                    if (IsInRange(currentRowNumber, csvTemplate.HeaderRowStartNumber, csvTemplate.HeaderRowEndNumber))
                    {
                        // 表头行用正则进行分隔
                        currentRow = ParseCsvLineByRegex(line);
                        // 利用反射为 csvDetail 中的属性进行赋值，区分大小写。
                        // 查找匹配的属性
                        var propertyInfo = propertyInfos.FirstOrDefault(info => info.Name.Equals(currentRow[keyColumnIndex], StringComparison.Ordinal));
                        if (propertyInfo != null)
                        {
                            // 获取值并赋值
                            if (propertyInfo.PropertyType.IsValueType || typeof(string).IsAssignableFrom(propertyInfo.PropertyType))
                            {
                                string valueToSet = currentRow[valueColumnIndex];
                                object convertedValue = Convert.ChangeType(valueToSet, propertyInfo.PropertyType);
                                propertyInfo.SetValue(csvDetail, convertedValue);
                            }
                            else if (propertyInfo.PropertyType.IsArray)
                            {
                                Type elementType = propertyInfo.PropertyType.GetElementType();
                                string[] valueToSet = currentRow.Skip(valueColumnIndex).ToArray();
                                Array convertedArray = Array.CreateInstance(elementType, valueToSet.Length);
                                for (int i = 0; i < valueToSet.Length; i++)
                                {
                                    convertedArray.SetValue(Convert.ChangeType(valueToSet[i], elementType), i);
                                }
                                propertyInfo.SetValue(csvDetail, convertedArray);
                            }
                        }
                    }
                    else if (currentRowNumber >= csvTemplate.DataRowStartNumber)
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

        // 异步不会阻塞线程，但是需要频繁切换上下文以及确认状态(虽然设置了 ConfigureAwait 但还是很慢)，总体性能会慢
        public async Task<List<string[]>> ReadCsvFileAsync(string filePath)
        {
            List<string[]> result = new List<string[]>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
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
        /// 提取已解析的 CSV 文件中的表头信息
        /// </summary>
        /// <param name="csvData"></param>
        /// <param name="csvTemplate"></param>
        /// <returns></returns>
        public Dictionary<string, string[]> GetHeaderInfo(List<string[]> csvData, CsvTemplate csvTemplate)
        {
            if (csvTemplate is null)
                throw new CsvProcessException("Get header info fail!There is a null input parameter.Please check the input parameters.");

            Dictionary<string, string[]> result = new Dictionary<string, string[]>();

            try
            {
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

        public void GetHeaderInfo(List<string[]> csvData, CsvTemplate csvTemplate, Dictionary<string, string[]> result)
        {
            if (csvTemplate is null || result is null)
                throw new CsvProcessException("Get header info fail!There is a null input parameter.Please check the input parameters.");

            try
            {
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Dictionary<string, string[]> GetHeaderInfoParallel(List<string[]> csvData, CsvTemplate csvTemplate)
        {
            if (csvTemplate == null)
                throw new CsvProcessException("Get header info fail!There is a null input parameter.Please check the input parameters.");

            var result = new ConcurrentDictionary<string, string[]>();

            try
            {
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
            if (csvTemplate is null)
                throw new CsvProcessException("Get body info fail!There is a null input parameter.Please check the input parameters.");

            Dictionary<Coordinate, string[]> result = new Dictionary<Coordinate, string[]>();

            try
            {
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

        public void GetBodyInfo(List<string[]> csvData, CsvTemplate csvTemplate, Dictionary<Coordinate, string[]> result)
        {
            if (csvTemplate is null || result is null)
                throw new CsvProcessException("Get body info fail!There is a null input parameter.Please check the input parameters.");

            try
            {
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Dictionary<Coordinate, string[]> GetBodyInfoParallel(List<string[]> csvData, CsvTemplate csvTemplate)
        {
            if (csvTemplate is null)
                throw new CsvProcessException("Get body info fail!There is a null input parameter.Please check the input parameters.");

            var result = new ConcurrentDictionary<Coordinate, string[]>();

            try
            {
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

        #region Methods
        /// <summary>
        /// 判断 num 是否在范围内，lower为下限，upper为上限，开区间
        /// </summary>
        protected bool IsInRange<T>(T num, T lower, T upper) where T : IComparable<T>
        {
            return num.CompareTo(lower) >= 0 && num.CompareTo(upper) <= 0;
        }

        /// <summary>
        /// 判断 num 是否不在范围内，lower为下限，upper为上限，开区间
        /// </summary>
        protected bool IsOutOfRange<T>(T num, T lower, T upper) where T : IComparable<T>
        {
            return num.CompareTo(lower) < 0 || num.CompareTo(upper) > 0;
        }

        /// <summary>
        /// 通过 String.Split 解析 CSV 文件中的一行数据
        /// </summary>
        /// <param name="line"></param>
        /// <param name="splitChar">分隔符，默认为逗号</param>
        /// <returns>解析后的字符串数组</returns>
        protected string[] ParseCsvLine(string line, char splitChar = ',')
        {
            string[] result;
            try
            {
                result = line.Split(splitChar);
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
        protected string[] ParseCsvLineByRegex(string line)
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
        #endregion
    }
}
