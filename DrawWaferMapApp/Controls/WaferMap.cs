using DrawWaferMapApp.Common;
using DrawWaferMapApp.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawWaferMapApp.Controls
{
    public enum DataStorageType
    {
        Dictionary,
        Matrix,
    }

    public partial class WaferMap : UserControl
    {
        // Event
        public event EventHandler<WaferMapMouseMoveEventArgs> WaferMapMouseMove;

        // Properties
        public int XMax { get; set; } = 0;
        public int XMin { get; set; } = 0;
        public int YMax { get; set; } = 0;
        public int YMin { get; set; } = 0;
        public float TranslationX { get; private set; } = 0f;
        public float TranslationY { get; private set; } = 0f;
        public float Zoom { get; private set; } = 1f;
        public float ScaleFactor { get; set; } = 0.5f;  // 缩放系数
        public Size DieSize { get; set; } = new Size(1, 1);  // 绘制 Die 的尺寸
        public bool RedrawWhenResize { get; set; } = true;
        public CsvDetail Detail { get; set; }
        public Color[] Colors { get; set; }  // Bin 颜色
        public DataStorageType DataType { get; set; } = DataStorageType.Dictionary;
        public bool IsDrawCross { get; set; } = false;  // 是否绘制十字线

        // Private Fields
        private int waferWidth;
        private int waferHeight;
        private static readonly object paintLock = new object();

        public WaferMap()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);  // 在调整窗口大小时重新绘制
            DoubleBuffered = true;  // 双缓冲
            SetBinColor();
            RegisterEvents();
        }

        public WaferMap(CsvDetail detail, int xMax, int xMin, int yMax, int yMin)
        {
            // 因通过对象初始化器来对属性赋值是在构造函数执行完毕后进行的，故以下赋值需要在其他操作执行前完成
            Detail = detail;
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
            // Calculate wafer size
            waferWidth = XMax - XMin;
            waferHeight = YMax - YMin;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Detail is null || (Detail.BodyInfo is null && Detail.BodyInfo_Matrix is null))
            {
                return;
            }

            e.Graphics.Clear(Color.White);  // Clear BackColor
            // TranslateTransform 可以理解为：新的原点 = (0, 0) - (TranslationX, TranslationY)
            e.Graphics.TranslateTransform(TranslationX, TranslationY);  // Set additional translation
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  // Use antialias

            // 创建一个包含原点 (0, 0) 的点数组
            PointF[] points = new PointF[] { new PointF(0, 0) };

            // 使用TransformPoints将所有点进行转换
            e.Graphics.Transform.TransformPoints(points);

            // 获取变换后的原点位置
            PointF transformedOrigin = points[0];

            // 输出变换后的原点
            Console.WriteLine($"Transformed Origin: X = {transformedOrigin.X}, Y = {transformedOrigin.Y}");

            // 计算图像的绘制比例
            float xScale = (float)Width / waferWidth * Zoom;
            float yScale = (float)Height / waferHeight * Zoom;

            // 记录偏移后的坐标系上限
            float translationWidth = Width - (TranslationX);
            float translationHeight = Height - (TranslationY);

            // 遍历 bodyInfo，绘制每个数据点
            if (DataType == DataStorageType.Dictionary)
            {
                foreach (var entry in Detail.BodyInfo)
                {
                    Coordinate coord = entry.Key;
                    string[] data = entry.Value;

                    // 根据 x 和 y 计算位置
                    float xPos = (coord.X - XMin) * xScale;
                    float yPos = (coord.Y - YMin) * yScale;

                    // 不需要绘制控件外的点
                    if (xPos < 0 - TranslationX || yPos < 0 - TranslationY || xPos > translationWidth || yPos > translationHeight)
                    {
                        continue;
                    }

                    // 绘制每个点(椭圆)
                    e.Graphics.FillEllipse(new SolidBrush(GetBinColor(data[2])), xPos, yPos, DieSize.Width * xScale, DieSize.Height * yScale);
                }
            }
            else
            {
                for (int x = 0; x < Detail.BodyInfo_Matrix.GetLength(0); x++)
                {
                    for (int y = 0; y < Detail.BodyInfo_Matrix.GetLength(1); y++)
                    {
                        string[] data = Detail.BodyInfo_Matrix[x, y];
                        if (data is null)
                        {
                            continue;
                        }

                        // 根据 x 和 y 计算位置
                        float xPos = ((x + XMin) - XMin) * xScale;
                        float yPos = ((y + YMin) - YMin) * yScale;

                        // 不需要绘制控件外的点
                        if (xPos < 0 - TranslationX || yPos < 0 - TranslationY || xPos > translationWidth || yPos > translationHeight)
                        {
                            continue;
                        }

                        // 绘制每个点(椭圆)
                        e.Graphics.FillEllipse(new SolidBrush(GetBinColor(data[2])), xPos, yPos, DieSize.Width * xScale, DieSize.Height * yScale);
                    }
                }
            }

            // 将控件平分为四个区域，绘制十字线
            if (IsDrawCross)
            {
                e.Graphics.FillRectangle(Brushes.Black, 0 - TranslationX, (translationHeight - TranslationY) / 2 - 1, Width, 2);
                e.Graphics.FillRectangle(Brushes.Black, (translationWidth - TranslationX) / 2 - 1, 0 - TranslationY, 2, Height);
                //e.Graphics.FillRectangle(Brushes.Black, (translationWidth - TranslationY) / 2 - 1, 0, 2, translationHeight);
                Console.WriteLine("translationWidth: " + translationWidth);
                Console.WriteLine("translationHeight: " + translationHeight);
                Console.WriteLine("TranslationX: " + TranslationX);
                Console.WriteLine("TranslationY: " + TranslationY);
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
            MouseMove += WaferMap_MouseMove;
            MouseDown += WaferMap_MouseDown;
            MouseWheel += WaferMap_MouseWheel;
            MouseUp += WaferMap_MouseUp;
            MouseClick += WaferMap_MouseClick;
        }

        #region Functions use to draw sth.

        #endregion

        #region Mouse Events.Overwrite they if you need.
        private bool isDragging = false;
        private Point dragStart;
        private Point lastMousePosition;

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
            float xScale = (float)Width / waferWidth * Zoom;
            float yScale = (float)Height / waferHeight * Zoom;
            int waferX = (int)((e.X - (TranslationX)) / xScale) + XMin;
            int waferY = (int)((e.Y - (TranslationY)) / yScale) + YMin;

            WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs(waferX.ToString(), waferY.ToString()));
            //Debug.WriteLine($"Mouse X: {e.X}, Mouse Y: {e.Y}.");

            if (isDragging)
            {
                TranslationX += e.X - dragStart.X;
                TranslationY += e.Y - dragStart.Y;

                dragStart = e.Location;  // 更新拖动起点
                Invalidate();  // 重新绘制
            }
        }

        public virtual void WaferMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = false;
            }
        }
        public virtual void WaferMap_MouseWheel(object sender, MouseEventArgs e)
        {
            // 记录旧的缩放比例
            float oldZoom = Zoom;

            // 更新 Zoom
            if (e.Delta > 0)
            {
                Zoom += ScaleFactor;  // 放大
            }
            else if (e.Delta < 0)
            {
                Zoom = Math.Max(1.0f, Zoom - ScaleFactor);  // 缩小，确保缩放比例不小于1.0
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
            Invalidate();

            // 获取当前wafer的X和Y位置
            int waferX = (int)((e.X - TranslationX) / (Zoom * ((float)Width / waferWidth)) + XMin);
            int waferY = (int)((e.Y - TranslationY) / (Zoom * ((float)Height / waferHeight)) + YMin);

            // 触发鼠标移动事件

            WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs(waferX.ToString(), waferY.ToString()));

            // Debug 输出
            //Debug.WriteLine($"wafer X: {waferX}, wafer Y: {waferY}.");
            //Debug.WriteLine($"Mouse X: {e.X}, Mouse Y: {e.Y}.");
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
            Invalidate();
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
        /// <param name="x">目标点的 X 坐标</param>
        /// <param name="y">目标点的 Y 坐标</param>
        public void SetPosition(int x, int y)
        {
            // 计算当前图像的绘制比例，结合 Zoom 缩放
            float xScale = (float)Width / waferWidth * Zoom;
            float yScale = (float)Height / waferHeight * Zoom;

            // 获取控件的中心点（像素坐标）
            float controlCenterX = Width / 2f;
            float controlCenterY = Height / 2f;

            // 计算目标点在当前缩放下的实际绘制位置
            float targetXPos = (x - XMin) * xScale;
            float targetYPos = (y - YMin) * yScale;

            // 计算偏移值
            // 若该点位于中心点左侧，则 xDiff 为正值，坐标系应该向右移动；反之，xDiff 为负值，坐标系应该向左移动
            // 若该点位于中心点上方，则 yDiff 为正值，坐标系应该向下移动；反之，yDiff 为负值，坐标系应该向上移动
            TranslationX = controlCenterX - targetXPos;
            TranslationY = controlCenterY - targetYPos;

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
