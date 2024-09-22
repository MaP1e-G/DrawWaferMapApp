using DrawWaferMapApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawWaferMapApp.Controls
{
    public partial class WaferMap : UserControl
    {
        // Public Properties
        public Dictionary<Coordinate, string[]> BodyInfo { set; get; } = null;
        public int XMax { set; get; } = 0;
        public int XMin { set; get; } = 0;
        public int YMax { set; get; } = 0;
        public int YMin { set; get; } = 0;
        public float TranslationX { set; get; } = 0f;
        public float TranslationY { set; get; } = 0f;
        public float Zoom { set; get; } = 1f;
        public Size DieSize { set; get; } = new Size(1, 1);  // 绘制 Die 的尺寸
        public bool RedrawWhenResize { set; get; } = true;

        /// <summary>
        /// Bin 颜色
        /// </summary>
        public Color[] Colors { set; get; }

        // Private Fields
        private int offsetX;
        private int offsetY;
        private int waferWidth;
        private int waferHeight;

        public WaferMap()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);  // 在调整窗口大小时重新绘制
            DoubleBuffered = true;  // 双缓冲
            SetBinColor();
            RegisterEvents();
        }

        public WaferMap(Dictionary<Coordinate, string[]> bodyInfo, int xMax, int xMin, int yMax, int yMin)
        {
            // 因通过对象初始化器来对属性赋值是在构造函数执行完毕后进行的，故以下赋值需要在其他操作执行前完成
            BodyInfo = bodyInfo;
            XMax = xMax;
            XMin = xMin;
            YMax = yMax;
            YMin = yMin;

            InitializeComponent();
            //SetStyle(ControlStyles.ResizeRedraw, true);  // 在调整窗口大小时重新绘制
            DoubleBuffered = true;  // 双缓冲
            SetBinColor();
            RegisterEvents();
        }

        private void WaferMap_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;  // 设置控件填充整个父容器
            // Set offset
            offsetX = XMin;
            offsetY = YMin;
            // Calculate wafer size
            waferWidth = XMax - XMin;
            waferHeight = YMax - YMin;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BodyInfo is null)
            {
                return;
            }

            e.Graphics.Clear(Color.White);  // Clear BackColor
            e.Graphics.TranslateTransform(TranslationX, TranslationY);  // Set additional translation
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  // Use antialias

            // 计算图像的绘制比例
            float xScale = (float)this.Width / waferWidth;
            float yScale = (float)this.Height / waferHeight;

            // 遍历 bodyInfo，绘制每个数据点
            foreach (var entry in BodyInfo)
            {
                Coordinate coord = entry.Key;
                string[] data = entry.Value;

                // 根据 x 和 y 计算位置
                float xPos = (coord.X - XMin) * xScale;
                float yPos = (coord.Y - YMin) * yScale;

                // 绘制每个点(椭圆)
                e.Graphics.FillEllipse(new SolidBrush(GetBinColor(data[2])), xPos, yPos, DieSize.Width * xScale, DieSize.Height * yScale);
            }
        }

        public void RedrawWaferMap()
        {
            this.Invalidate();
        }

        /// <summary>
        /// 设置各个 Bin 等级的颜色
        /// </summary>
        public void SetBinColor()
        {
            Color[] colors = new Color[126];
            colors[0] = Color.Black;
            colors[1] = Color.FromArgb(255, 195, 0);
            colors[2] = Color.FromArgb(155, 0, 156);
            colors[3] = Color.FromArgb(255, 130, 1);
            colors[4] = Color.FromArgb(0, 153, 0);
            colors[5] = Color.FromArgb(0, 154, 254);
            colors[6] = Color.FromArgb(206, 0, 205);
            colors[7] = Color.FromArgb(0, 101, 207);
            colors[8] = Color.FromArgb(254, 154, 206);
            colors[9] = Color.FromArgb(255, 154, 0);
            colors[10] = Color.FromArgb(0, 181, 74);
            colors[122] = Color.Green;
            colors[123] = Color.Red;
            colors[124] = Color.Blue;
            colors[125] = Color.Black;
            for (int i = 11; i < 122; i++)
            {
                colors[i] = Color.Silver;
            }
            SetBinColor(colors);
        }

        public void SetBinColor(Color[] colors)
        {
            if (colors is null || colors.Length == 0)
            {
                SetBinColor();  // 如果传入的数组为空或长度为 0, 就调用默认颜色
            }
            Colors = colors;
        }

        private Color GetBinColor(int binNo)
        {
            return Colors[binNo];
        }

        private Color GetBinColor(string binNo)
        {
            return Colors[Convert.ToInt32(binNo)];
        }

        /// <summary>
        /// 为控件注册事件
        /// </summary>
        private void RegisterEvents()
        {

        }

        #region Functions use to draw sth.
        
        #endregion
    }
}
