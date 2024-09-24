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
        // Event
        public event EventHandler<WaferMapMouseMoveEventArgs> WaferMapMouseMove;

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
        public CsvDetail CsvDetail { set; get; }
        /// <summary>
        /// Bin 颜色
        /// </summary>
        public Color[] Colors { set; get; }

        // Private Fields
        private int offsetX;
        private int offsetY;
        private int waferWidth;
        private int waferHeight;
        private float zoomTranslationX = 0;
        private float zoomTranslationY = 0;
        //private float xScale;
        //private float yScale;

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
            SetStyle(ControlStyles.ResizeRedraw, true);  // 在调整窗口大小时重新绘制
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
            // TranslateTransform 可以理解为：新的原点 = (0, 0) - (TranslationX, TranslationY)
            e.Graphics.TranslateTransform(TranslationX + zoomTranslationX, TranslationY + zoomTranslationY);  // Set additional translation
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  // Use antialias

            // 计算图像的绘制比例
            float xScale = (float)Width / waferWidth * Zoom;
            float yScale = (float)Height / waferHeight * Zoom;

            // 记录偏移后的坐标系上限
            float translationWidth = Width - (TranslationX + zoomTranslationX);
            float translationHeight = Height - (TranslationY + zoomTranslationY);

            // 遍历 bodyInfo，绘制每个数据点
            foreach (var entry in BodyInfo)
            {
                Coordinate coord = entry.Key;
                string[] data = entry.Value;

                // 根据 x 和 y 计算位置
                float xPos = (coord.X - XMin) * xScale;
                float yPos = (coord.Y - YMin) * yScale;

                // 不需要绘制控件外的点
                if (xPos > translationWidth || yPos > translationHeight)
                {
                    continue;
                }

                // 绘制每个点(椭圆)
                e.Graphics.FillEllipse(new SolidBrush(GetBinColor(data[2])), xPos, yPos, DieSize.Width * xScale, DieSize.Height * yScale);
            }
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
            this.MouseMove += WaferMap_MouseMove;
            this.MouseDown += WaferMap_MouseDown;
            this.MouseWheel += WaferMap_MouseWheel;
            this.MouseUp += WaferMap_MouseUp;
            this.MouseClick += WaferMap_MouseClick;
        }

        #region Functions use to draw sth.

        #endregion

        #region Mouse Events.Overwrite they if you need.
        private bool isDragging = false;
        private Point dragStart;
        private Point lastMousePosition;
        private Point lastZoomPosition;
        private int curX;
        private int curY;

        public virtual void WaferMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = true;
                dragStart = e.Location;
            }
        }

        public virtual void WaferMap_MouseMove(object sender, MouseEventArgs e)
        {
            // 检查鼠标是否在移动，如果位置没有变化，直接返回
            if (e.Location == lastMousePosition)
            {
                return;
            }

            // 更新鼠标上次位置
            lastMousePosition = e.Location;

            // 根据鼠标当前坐标来计算当前 Die 的实际坐标
            float xScale = (float)this.Width / waferWidth * Zoom;
            float yScale = (float)this.Height / waferHeight * Zoom;
            int waferX = (int)((e.X - (TranslationX + zoomTranslationX)) / xScale) + XMin;
            int waferY = (int)((e.Y - (TranslationY + zoomTranslationY)) / yScale) + YMin;
            curX = waferX;
            curY = waferY;

            if (!BodyInfo.ContainsKey(new Coordinate() { X = waferX, Y = waferY }))
            {
                WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs("N/A", "N/A"));
            }
            else
            {
                WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs(waferX.ToString(), waferY.ToString()));
            }
            Console.WriteLine($"Mouse X: {e.X}, Mouse Y: {e.Y}.");

            if (isDragging)
            {
                TranslationX += e.X - dragStart.X;
                TranslationY += e.Y - dragStart.Y;

                dragStart = e.Location;  // 更新拖动起点
                this.Invalidate();  // 重新绘制
            }
        }

        public virtual void WaferMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = false;
            }
        }

        //public virtual void WaferMap_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    float oldZoom = Zoom;

        //    if (e.Delta > 0)
        //    {
        //        Zoom += 0.1f; 
        //    }
        //    else if (e.Delta < 0)
        //    {
        //        Zoom = Math.Max(0.1f, Zoom - 0.1f);
        //    }

        //    // 根据鼠标当前坐标来计算当前 Die 的实际坐标
        //    float xScale = (float)this.Width / waferWidth * Zoom;
        //    float yScale = (float)this.Height / waferHeight * Zoom;
        //    int waferX = (int)((e.X - (TranslationX + zoomTranslationX)) / xScale) + XMin;
        //    int waferY = (int)((e.Y - (TranslationY + zoomTranslationY)) / yScale) + YMin;

        //    // 保持鼠标当前所在的点相对不变。如果鼠标位置没有发生改变，就累加偏移量；否则重置偏移量
        //    //TranslationX = oldXPos - newXPos
        //    //TranslationX += e.X * (oldZoom - Zoom);
        //    //TranslationY += e.Y * (oldZoom - Zoom);
        //    if (e.Location == lastZoomPosition)
        //    {
        //        zoomTranslationX += e.X * (oldZoom - Zoom);
        //        zoomTranslationY += e.Y * (oldZoom - Zoom);
        //    }
        //    else
        //    {
        //        // 根据 x 和 y 计算位置
        //        float xPos = (curX - XMin) * xScale;
        //        float yPos = (curY - YMin) * yScale;
        //        zoomTranslationX = xPos - e.X;
        //        zoomTranslationY = yPos - e.Y;
        //    }
        //    lastZoomPosition = e.Location;

        //    this.Invalidate();

        //    if (!BodyInfo.ContainsKey(new Coordinate() { X = waferX, Y = waferY }))
        //    {
        //        WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs("N/A", "N/A"));
        //    }
        //    else
        //    {
        //        WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs(waferX.ToString(), waferY.ToString()));
        //    }
        //    Console.WriteLine($"wafer X: {waferX}, wafer Y: {waferY}.");
        //    Console.WriteLine($"Mouse X: {e.X}, Mouse Y: {e.Y}.");
        //}
        public virtual void WaferMap_MouseWheel(object sender, MouseEventArgs e)
        {
            // 记录旧的缩放比例
            float oldZoom = Zoom;

            // 更新 Zoom
            if (e.Delta > 0)
            {
                Zoom += 0.1f;  // 放大
            }
            else if (e.Delta < 0)
            {
                Zoom = Math.Max(0.1f, Zoom - 0.1f);  // 缩小，确保缩放比例不小于0.1
            }

            // 计算缩放前的鼠标相对于整个wafer图的坐标
            float oldXPos = (e.X - TranslationX) / (oldZoom * ((float)Width / waferWidth));
            float oldYPos = (e.Y - TranslationY) / (oldZoom * ((float)Height / waferHeight));

            // 更新 xScale 和 yScale
            float xScale = (float)Width / waferWidth * Zoom;
            float yScale = (float)Height / waferHeight * Zoom;

            // 计算缩放后的鼠标相对于整个wafer图的坐标
            float newXPos = (e.X - TranslationX) / (Zoom * ((float)Width / waferWidth));
            float newYPos = (e.Y - TranslationY) / (Zoom * ((float)Height / waferHeight));

            // 通过平移调整，使鼠标相对位置不变
            TranslationX += (newXPos - oldXPos) * Zoom * ((float)Width / waferWidth);
            TranslationY += (newYPos - oldYPos) * Zoom * ((float)Height / waferHeight);

            // 请求重绘
            this.Invalidate();

            // 获取当前wafer的X和Y位置
            int waferX = (int)((e.X - TranslationX) / (Zoom * ((float)Width / waferWidth)) + XMin);
            int waferY = (int)((e.Y - TranslationY) / (Zoom * ((float)Height / waferHeight)) + YMin);

            // 触发鼠标移动事件
            if (!BodyInfo.ContainsKey(new Coordinate() { X = waferX, Y = waferY }))
            {
                WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs("N/A", "N/A"));
            }
            else
            {
                WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs(waferX.ToString(), waferY.ToString()));
            }

            // Debug 输出
            Console.WriteLine($"wafer X: {waferX}, wafer Y: {waferY}.");
            Console.WriteLine($"Mouse X: {e.X}, Mouse Y: {e.Y}.");
        }


        public virtual void WaferMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MessageBox.Show("Left Click!");
            }
        }
        #endregion

        #region Public Methods
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

        /// <summary>
        /// 定位 Wafer 上的某个点，将该点移至控件的中心
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            // 计算图像的绘制比例
            float xScale = (float)this.Width / waferWidth * Zoom;
            float yScale = (float)this.Height / waferHeight * Zoom;
            // 该点相对于中心点的位置。中心点：((XMax + XMin) / 2, (YMax + YMin) / 2)
            int xCenter = (XMax + XMin) / 2;
            int yCenter = (YMax + YMin) / 2;
            // 计算偏移值
            // 若该点位于中心点左侧，则 xDiff 为正值，坐标系应该向右移动；反之，xDiff 为负值，坐标系应该向左移动
            // 若该点位于中心点上方，则 yDiff 为正值，坐标系应该向下移动；反之，yDiff 为负值，坐标系应该向上移动
            int xDiff = xCenter - x;
            int yDiff = yCenter - y;
            // 真正偏移值应算上比例
            TranslationX = xDiff * xScale;
            TranslationY = yDiff * yScale;
            RedrawWaferMap();
        }
        #endregion
    }

    public class WaferMapMouseMoveEventArgs : EventArgs
    {
        public string WaferX { get; }
        public string WaferY { get; }

        public WaferMapMouseMoveEventArgs(string waferX, string waferY)
        {
            WaferX = waferX;
            WaferY = waferY;
        }
    }
}
