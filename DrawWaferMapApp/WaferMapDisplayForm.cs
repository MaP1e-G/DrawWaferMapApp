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
        public List<string[]> DataInfo { set; get; } = null;
        private WaferMap wfmMain;

        public WaferMapDisplayForm()
        {
            InitializeComponent();
            InitializeOther();
        }

        #region Events
        private void btnDrawWaferMap_Click(object sender, EventArgs e)
        {
            DrawWaferMap();
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
