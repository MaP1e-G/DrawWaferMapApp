using DrawWaferMapApp.Common;
using DrawWaferMapApp.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawWaferMapApp
{
    public partial class Form1 : Form
    {
        private static string[] RedYellowColumnList = { "POSX", "POSY", "BIN", "ORGBINNO", "VF2", "VF1", "VZ", "IR", "IF1", "LOP1", "WLD", "WLP", "HW", "VF3", "IR2", "IV3" };

        private CsvProcessTool csvProcessTool;
        private CsvTemplate csvTemplate;
        private CsvDetail csvDetail;

        private Dictionary<string, string[]> headerInfo;
        private Dictionary<Coordinate, string[]> bodyInfo;

        public Form1()
        {
            InitializeComponent();
            InitializeOther();
        }

        #region Private Function
        private void InitializeOther()
        {
            // 初始化字段
            csvProcessTool = new CsvProcessTool();

            // 允许拖放操作
            this.AllowDrop = true;

            // 处理拖放事件
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            // 为文本框设置默认值
            txtMapPath.Text = @"C:\Users\admin\Desktop\SZHC\法则\AOI\HC240706DD3D1756T-02#RF06DD3DD.csv";
            //txtMapPath.Text = @"C:\Users\67020\Desktop\SZHC\法则\AOI\HC240706DD3D1756T-01#RF06DD3DD.csv";
        }

        // 当拖放操作进入窗体时触发
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            // 确保拖放的内容是文件
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // 允许复制
            }
            else
            {
                e.Effect = DragDropEffects.None; // 不允许拖放
            }
        }

        // 当文件被放下到窗体时触发
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            // 获取拖放的文件路径
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                string filePath = files[0]; // 获取第一个文件路径
                txtMapPath.Text = filePath;
            }
        }

        // 创建并配置 OpenFileDialog
        private void SelectFile(object sender)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "请选择一个文件";  // 可选: 设置对话框标题
                openFileDialog.InitialDirectory = @"C:\";  // 可选: 设置默认文件路径
                openFileDialog.Filter = "csv 文件 (*.csv)|*.csv";  // 可选: 设置文件类型过滤器
                openFileDialog.DefaultExt = "csv";  // 可选: 设置默认文件扩展名
                openFileDialog.Multiselect = false;  // 可选: 允许多选, 设置为 true 允许选择多个文件
                // 显示对话框并检查用户是否选择了文件
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取用户选择的文件路径
                    string selectedFilePath = openFileDialog.FileName;
                    if (sender is Infragistics.Win.UltraWinEditors.UltraTextEditor txtTemp)
                    {
                        txtTemp.Text = selectedFilePath;
                    }
                }
            }
        }

        #endregion

        #region Events
        private void btnShowMap_Click(object sender, EventArgs e)
        {
            if (csvDetail is null || csvDetail.BodyInfo is null)
            {
                return;
            }
            int xMax, xMin, yMax, yMin;
            xMax = GetXMax();
            xMin = GetXMin();
            yMax = GetYMax();
            yMin = GetYMin();
            WaferMapDisplayForm waferMapDisplayForm = new WaferMapDisplayForm()
            {
                XMax = xMax,
                XMin = xMin,
                YMax = yMax,
                YMin = yMin,
                Detail = csvDetail,
            };
            waferMapDisplayForm.Show();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectFile(txtMapPath);
        }

        private void btnCsvReadTest_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                //List<string[]> records = csvProcessTool.ReadCsvFile(txtMapPath.Text);
                //Console.WriteLine(records.Count.ToString() + Environment.NewLine + stopwatch.Elapsed);
                //headerInfo = csvProcessTool.GetHeaderInfo(records, csvTemplate);
                //bodyInfo = csvProcessTool.GetBodyInfo(records, csvTemplate);
                //stopwatch.Stop();
                AOICsvTemplate csvTemplate = new AOICsvTemplate();
                csvDetail = new AOICsvDetail();
                csvProcessTool.ReadCsvFile(txtMapPath.Text, csvTemplate, csvDetail);
                Console.WriteLine("Completed." + Environment.NewLine + stopwatch.Elapsed);
                MessageBox.Show("Completed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnQuickRead_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.WriteLine("Now Thread: " + Environment.CurrentManagedThreadId);
                List<string[]> records = await csvProcessTool.ReadCsvFileAsync(txtMapPath.Text);
                Console.WriteLine("await return: " + stopwatch.Elapsed);
                Console.WriteLine("Now Thread: " + Environment.CurrentManagedThreadId);
                Console.WriteLine(records.Count.ToString() + Environment.NewLine + stopwatch.Elapsed);
                CsvTemplate csvTemplate = new CsvTemplate()
                {
                    DataRowStartNumber = 10,
                    HeaderKeyColumnNumber = 1,
                    HeaderValueColumnNumber = 3,
                    HeaderRowStartNumber = 1,
                    HeaderRowEndNumber = 7,
                    XCoordinateColumnNumber = 1,
                    YCoordinateColumnNumber = 2,
                    ColumnNames = RedYellowColumnList,
                };
                headerInfo = await Task.Run(() => csvProcessTool.GetHeaderInfo(records, csvTemplate));
                bodyInfo = await Task.Run(() => csvProcessTool.GetBodyInfoParallel(records, csvTemplate));
                Console.WriteLine("Completed." + Environment.NewLine + stopwatch.Elapsed);
                stopwatch.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            AOICsvDetail aoiDetail = new AOICsvDetail();
            CsvDetail csvDetail = aoiDetail;
            PropertyInfo[] infos = csvDetail.GetType().GetProperties();
            foreach (var propertyInfo in infos)
            {
                if (typeof(string).IsAssignableFrom(propertyInfo.PropertyType) || typeof(string[]).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    Console.WriteLine($"{propertyInfo.Name}:");
                    Console.WriteLine($"  DeclaringType: {propertyInfo.DeclaringType}");
                    Console.WriteLine($"  ReflectedType: {propertyInfo.ReflectedType}");
                    Console.WriteLine($"  MemberType: {propertyInfo.MemberType}");
                    Console.WriteLine($"  PropertyType: {propertyInfo.PropertyType}");
                }
            }
        }

        private int GetXMax()
        {
            int result = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //result = bodyInfo.Max(kvp => kvp.Key.X);
            result = csvDetail.BodyInfo.Max(kvp => kvp.Key.X);
            stopwatch.Stop();
            Console.WriteLine($"X MAX: {result}" + Environment.NewLine + stopwatch.Elapsed);
            return result;
        }

        private int GetXMin()
        {
            int result = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //result = bodyInfo.Min(kvp => kvp.Key.X);
            result = csvDetail.BodyInfo.Min(kvp => kvp.Key.X);
            stopwatch.Stop();
            Console.WriteLine($"X MIN: {result}" + Environment.NewLine + stopwatch.Elapsed);
            return result;
        }

        private int GetYMax()
        {
            int result = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //result = bodyInfo.Max(kvp => kvp.Key.Y);
            result = csvDetail.BodyInfo.Max(kvp => kvp.Key.Y);
            stopwatch.Stop();
            Console.WriteLine($"Y MAX: {result}" + Environment.NewLine + stopwatch.Elapsed);
            return result;
        }

        private int GetYMin()
        {
            int result = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //result = bodyInfo.Min(kvp => kvp.Key.Y);
            result = csvDetail.BodyInfo.Min(kvp => kvp.Key.Y);
            stopwatch.Stop();
            Console.WriteLine($"Y MIN: {result}" + Environment.NewLine + stopwatch.Elapsed);
            return result;
        }
        #endregion
    }
}
