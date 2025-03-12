using DrawWaferMapApp.Common;
using DrawWaferMapApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawWaferMapApp
{
    public partial class WaferMapDisplayForm : Form
    {
        public int XMax { get; set; } = 0;
        public int XMin { get; set; } = 0;
        public int YMax { get; set; } = 0;
        public int YMin { get; set; } = 0;
        public CsvDetail Detail { get; set; }
        public CsvTemplate Template { get; set; }

        private WaferMap wfmMain;
        private static readonly Stopwatch stopwatch = new Stopwatch();

        public WaferMapDisplayForm()
        {
            InitializeComponent();
            InitializeOther();
        }

        #region Controls Events
        private void WaferMapDisplayForm_Load(object sender, EventArgs e)
        {
            if (Detail.DataType == DataStorageType.Dictionary)
            {
                btnDrawMapByDictionary.PerformClick();
            }
            else if (Detail.DataType == DataStorageType.Matrix)
            {
                btnDrawMapByMatrix.PerformClick();
            }
        }

        private void btnDrawWaferMap_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            DrawWaferMap();
            Console.WriteLine($"Draw map time: {stopwatch.ElapsedMilliseconds}");
            stopwatch.Stop();
        }

        private void btnGoPosition_Click(object sender, EventArgs e)
        {
            if (wfmMain is null)
            {
                return;
            }

            try
            {
                int x = Convert.ToInt32(txtXCoordinate.Text);
                int y = Convert.ToInt32(txtYCoordinate.Text);
                if (x < XMin || x > XMax || y < YMin || y > YMax)
                {
                    MessageBox.Show("Please input a valid coordinate!");
                    return;
                }
                wfmMain.SetPosition(x, y);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please input a valid coordinate!");
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btnGetElectricalValue_Click(object sender, EventArgs e)
        {
            // To do...
        }
        #endregion

        #region Private Function
        private void InitializeOther()
        {
        }

        private void DrawWaferMap()
        {
            if (wfmMain is null)
            {
                wfmMain = new WaferMap()
                {
                    XMax = this.XMax,
                    XMin = this.XMin,
                    YMax = this.YMax,
                    YMin = this.YMin,
                    Detail = this.Detail,
                };

                // 设置迷你晶圆图的属性
                miniWaferMap1.Detail = Detail;
                miniWaferMap1.HalfOfTheSide = 10;

                // 订阅事件，即时更新显示坐标
                wfmMain.WaferMapMouseMove += (s, args) =>
                {
                    txtXCoordinate.Text = args.WaferX;
                    txtYCoordinate.Text = args.WaferY;
                    try
                    {
                        int x = Convert.ToInt32(args.WaferX);
                        int y = Convert.ToInt32(args.WaferY);
                        Coordinate coordinate = new Coordinate(x, y);
                        txtBinNo.Text = Detail.BodyInfo[coordinate][2];

                        // 更新（重绘）迷你晶圆图
                        miniWaferMap1.X = x;
                        miniWaferMap1.Y = y;
                        miniWaferMap1.Redraw();
                    }
                    catch (FormatException ex)
                    {
                        txtBinNo.Text = "N/A";
                        return;
                    }
                    catch (Exception ex)
                    {
                        txtBinNo.Text = "Error";
                        return;
                    }
                };
                utpWaferMap.ClientArea.Controls.Add(wfmMain);
                wfmMain.Dock = DockStyle.None;  // 需要手动设置 Dock 和 Anchor，防止窗口 Resize 时无法调整 WaferMap 控件的大小
                wfmMain.Anchor = AnchorStyles.None;
                AdjustWaferMapSize();
            }
            else
            {
                wfmMain.RedrawWaferMap();
            }
            //WaferMap wfmMain = new WaferMap(BodyInfo, XMax, XMin, YMax, YMin);
        }
        #endregion

        private void btnDrawMapByMatrix_Click(object sender, EventArgs e)
        {
            if (wfmMain is null)
            {
                wfmMain = new WaferMap()
                {
                    XMax = this.XMax,
                    XMin = this.XMin,
                    YMax = this.YMax,
                    YMin = this.YMin,
                    Detail = this.Detail,
                    //IsDrawCross = true,
                };

                // 设置迷你晶圆图的属性
                miniWaferMap1.Detail = Detail;
                miniWaferMap1.HalfOfTheSide = 10;

                // 订阅事件，即时更新显示坐标
                wfmMain.WaferMapMouseMove += (s, args) =>
                {
                    txtXCoordinate.Text = args.WaferX;
                    txtYCoordinate.Text = args.WaferY;
                    try
                    {
                        int x = Convert.ToInt32(args.WaferX);
                        int y = Convert.ToInt32(args.WaferY);
                        if (x < XMin || x > XMax || y < YMin || y > YMax)
                        {
                            txtBinNo.Text = "N/A";
                            return;
                        }
                        if (Detail.BodyInfo_Matrix[x - XMin, y - YMin] is null)
                        {
                            txtBinNo.Text = "N/A";
                            return;
                        }
                        txtBinNo.Text = Detail.BodyInfo_Matrix[x - XMin, y - YMin][Template.ColumnsMap["BIN"]];

                        // 更新（重绘）迷你晶圆图
                        //miniWaferMap1.Redraw(x, y, Detail, 10);
                        miniWaferMap1.X = x;
                        miniWaferMap1.Y = y;
                        miniWaferMap1.Redraw();
                    }
                    catch (FormatException ex)
                    {
                        txtBinNo.Text = "N/A";
                        return;
                    }
                    catch (Exception ex)
                    {
                        txtBinNo.Text = "Error";
                        return;
                    }
                };
                utpWaferMap.ClientArea.Controls.Add(wfmMain);
                wfmMain.Dock = DockStyle.None;  // 需要手动设置 Dock 和 Anchor，防止窗口 Resize 时无法调整 WaferMap 控件的大小
                wfmMain.Anchor = AnchorStyles.None;
                AdjustWaferMapSize();
            }
            else
            {
                wfmMain.RedrawWaferMap();
            }
        }

        private void btnDrawBin_Click(object sender, EventArgs e)
        {
            if (wfmMain != null)
            {
                bool result = wfmMain.DrawBin();
                if (result)
                {
                    btnClean.Enabled = true;
                    btnDrawBin.Text = "Draw Bin(End)";
                }
                else
                {
                    btnClean.Enabled = false;
                    btnDrawBin.Text = "Draw Bin(Start)";
                }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            if (wfmMain != null)
            {
                wfmMain.CleanDrawBinHistory();
            }
        }

        private void btnModifyBin_Click(object sender, EventArgs e)
        {
            wfmMain.ModifyBin();
        }

        private void btnDrawBinUndo_Click(object sender, EventArgs e)
        {
            wfmMain.DrawBinUndo();
        }

        private void WaferMapDisplayForm_ResizeEnd(object sender, EventArgs e)
        {
            AdjustWaferMapSize();
        }

        // 调整 WaferMap 控件的大小，使其保持正方形
        private void AdjustWaferMapSize()
        {
            // 获取 UltraPanel 的大小
            int panelWidth = utpWaferMap.ClientArea.Width;
            int panelHeight = utpWaferMap.ClientArea.Height;

            // 取宽高的最小值，确保保持正方形
            int squareSize = Math.Min(panelWidth, panelHeight);

            // 设置 WaferMap 的大小和位置
            wfmMain.Size = new Size(squareSize, squareSize);

            // 居中放置
            wfmMain.Location = new Point(
                (panelWidth - squareSize) / 2,
                (panelHeight - squareSize) / 2
            );
        }

        private void WaferMapDisplayForm_Resize(object sender, EventArgs e)
        {
            AdjustWaferMapSize();
        }
    }
}
