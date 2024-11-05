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
                    DataType = DataStorageType.Dictionary,
                };
                // 订阅事件，即时更新显示坐标
                wfmMain.WaferMapMouseMove += (s, args) =>
                {
                    txtXCoordinate.Text = args.WaferX;
                    txtYCoordinate.Text = args.WaferY;
                    try
                    {
                        Coordinate coordinate = new Coordinate(Convert.ToInt32(args.WaferX), Convert.ToInt32(args.WaferY));
                        txtBinNo.Text = Detail.BodyInfo[coordinate][2];
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
                    DataType = DataStorageType.Matrix,
                    IsDrawCross = true,
                };
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
            }
            else
            {
                wfmMain.RedrawWaferMap();
            }
        }
    }
}
