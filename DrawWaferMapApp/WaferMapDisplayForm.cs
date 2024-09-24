using DrawWaferMapApp.Common;
using DrawWaferMapApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawWaferMapApp
{
    public partial class WaferMapDisplayForm : Form
    {
        public Dictionary<string, string[]> HeaderInfo { set; get; } = null;
        public Dictionary<Coordinate, string[]> BodyInfo { set; get; } = null;
        public int XMax { set; get; } = 0;
        public int XMin { set; get; } = 0;
        public int YMax { set; get; } = 0;
        public int YMin { set; get; } = 0;
        public CsvDetail CsvDetail { set; get; }

        private WaferMap wfmMain;

        public WaferMapDisplayForm()
        {
            InitializeComponent();
            InitializeOther();
        }

        #region Controls Events
        private void btnDrawWaferMap_Click(object sender, EventArgs e)
        {
            DrawWaferMap();
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
                    BodyInfo = this.BodyInfo,
                    XMax = this.XMax,
                    XMin = this.XMin,
                    YMax = this.YMax,
                    YMin = this.YMin,
                };
                // 订阅事件，即时更新显示坐标
                wfmMain.WaferMapMouseMove += (s, args) =>
                {
                    txtXCoordinate.Text = args.WaferX;
                    txtYCoordinate.Text = args.WaferY;
                    try
                    {
                        Coordinate coordinate = new Coordinate(Convert.ToInt32(args.WaferX), Convert.ToInt32(args.WaferY));
                        txtBinNo.Text = BodyInfo[coordinate][2];
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
    }
}
