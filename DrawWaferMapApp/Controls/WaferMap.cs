﻿using DrawWaferMapApp.Common;
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
        public CsvDetail Detail { get; set; }
        public Color[] Colors { get; set; }  // Bin 颜色
        public bool DrawCross { get; set; } = false;  // 是否绘制十字线

        // Private Fields
        private int _waferWidth;
        private int _waferHeight;
        private bool _isDrawBin = false;  // 画 Bin 标志，画 Bin 模式下不允许除了描点以外的操作
        private bool _canModifyBin = false;  // 是否可以修改 Bin
        private Size _squareSize = new Size(25, 25);  // 绘制的半透明小正方形方框的大小
        private int _opacity = 128;  // 透明度，0-255之间
        private Point _squareCenter = new Point(0, 0);
        private bool _fixed = false;  // 固定某一个点

        public WaferMap()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);  // 在调整窗口大小时重新绘制
            DoubleBuffered = true;  // 双缓冲
        }

        private void WaferMap_Load(object sender, EventArgs e)
        {
            SetBinColor(null);
            RegisterEvents();
            this.Dock = DockStyle.Fill;  // 设置控件填充整个父容器

            // Calculate wafer size
            _waferWidth = XMax - XMin;
            _waferHeight = YMax - YMin;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Detail is null || (Detail.BodyInfo is null && Detail.BodyInfo_Matrix is null))
            {
                return;
            }

            e.Graphics.Clear(Color.White);  // 清除所有绘制内容，并用白色填充背景

            // 对于 Graphics 而言，新的原点 = (TranslationX, TranslationY)；对于控件而言，新的原点 = (0, 0) - (TranslationX, TranslationY)
            e.Graphics.TranslateTransform(TranslationX, TranslationY);  // 重设原点
            Console.WriteLine(TranslationX + " " + TranslationY);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  // 抗锯齿

            // 创建一个包含原点 (0, 0) 的点数组
            //PointF[] points = new PointF[] { new PointF(0, 0) };
            // 使用TransformPoints将所有点进行转换
            //e.Graphics.Transform.TransformPoints(points);
            // 获取变换后的原点位置
            //PointF transformedOrigin = points[0];
            // 输出变换后的原点
            //Console.WriteLine($"Transformed Origin: X = {transformedOrigin.X}, Y = {transformedOrigin.Y}");

            // 计算图像的绘制比例
            float xScale = (float)Width / _waferWidth * Zoom;
            float yScale = (float)Height / _waferHeight * Zoom;

            // 记录偏移后的坐标系上限
            float translationWidth = Width - (TranslationX);
            float translationHeight = Height - (TranslationY);

            // 遍历 bodyInfo，绘制每个数据点
            if (Detail.DataType == DataStorageType.Dictionary)
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
                    if (_canModifyBin && !IsInPolygon2(new Point((int)xPos, (int)yPos), _drawBinPoints))
                    {
                        e.Graphics.FillEllipse(new SolidBrush(Color.Pink), xPos, yPos, DieSize.Width * xScale, DieSize.Height * yScale);
                    }
                    else
                    {
                        e.Graphics.FillEllipse(new SolidBrush(GetBinColor(data[2])), xPos, yPos, DieSize.Width * xScale, DieSize.Height * yScale);
                    }
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
                        if (_canModifyBin && !IsInPolygon2(new Point((int)xPos, (int)yPos), _drawBinPoints) && data[2].Equals("122"))
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.Pink), xPos, yPos, DieSize.Width * xScale, DieSize.Height * yScale);
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(GetBinColor(data[2])), xPos, yPos, DieSize.Width * xScale, DieSize.Height * yScale);
                        }
                    }
                }
            }

            // 将控件平分为四个区域，绘制十字线
            if (DrawCross)
            {
                e.Graphics.FillRectangle(Brushes.Black, 0 - TranslationX, (translationHeight - TranslationY) / 2 - 1, Width, 2);
                e.Graphics.FillRectangle(Brushes.Black, (translationWidth - TranslationX) / 2 - 1, 0 - TranslationY, 2, Height);
            }

            if (_canModifyBin)
            {
                _canModifyBin = false;
            }
        }

        #region Mouse Events.Overwrite they if you need.
        private bool _isDragging = false;
        private Point _dragStart;
        private Point _lastMousePosition;

        protected virtual void WaferMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _isDragging = true;
                _dragStart = e.Location;
            }
        }

        protected virtual void WaferMap_MouseMove(object sender, MouseEventArgs e)
        {
            // 如果开启了“固定”选项，则不处理鼠标移动事件
            if (_fixed)
                return;

            // 检查鼠标是否在移动，如果位置没有变化，直接返回
            if (e.Location == _lastMousePosition)
                return;

            // 更新鼠标上次位置
            _lastMousePosition = e.Location;

            // 根据鼠标当前坐标来计算当前 Die 的实际坐标
            float xScale = (float)Width / _waferWidth * Zoom;
            float yScale = (float)Height / _waferHeight * Zoom;
            int waferX = (int)((e.X - (TranslationX)) / xScale) + XMin;
            int waferY = (int)((e.Y - (TranslationY)) / yScale) + YMin;

            WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs(waferX.ToString(), waferY.ToString()));
            Debug.WriteLine($"Mouse X: {e.X}, Mouse Y: {e.Y}.");

            if (_isDragging && !_isDrawBin)
            {
                TranslationX += e.X - _dragStart.X;
                TranslationY += e.Y - _dragStart.Y;

                _dragStart = e.Location;  // 更新拖动起点
                Invalidate();  // 重新绘制
            }
        }

        protected virtual void WaferMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                _isDragging = false;
        }

        protected virtual void WaferMap_MouseWheel(object sender, MouseEventArgs e)
        {
            // 如果开启了“固定”选项，则不处理鼠标滚轮事件
            if (_fixed)
                return;

            if (_isDrawBin)
                return;

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
            float oldXPos = (e.X - TranslationX) / (oldZoom * ((float)Width / _waferWidth));
            float oldYPos = (e.Y - TranslationY) / (oldZoom * ((float)Height / _waferHeight));

            // 计算缩放后的鼠标相对于整个wafer图的坐标
            float newXPos = (e.X - TranslationX) / (Zoom * ((float)Width / _waferWidth));
            float newYPos = (e.Y - TranslationY) / (Zoom * ((float)Height / _waferHeight));

            // 通过平移调整，使鼠标相对位置不变
            TranslationX += (newXPos - oldXPos) * Zoom * ((float)Width / _waferWidth);
            TranslationY += (newYPos - oldYPos) * Zoom * ((float)Height / _waferHeight);

            // 请求重绘
            Invalidate();

            // 获取当前wafer的X和Y位置
            int waferX = (int)((e.X - TranslationX) / (Zoom * ((float)Width / _waferWidth)) + XMin);
            int waferY = (int)((e.Y - TranslationY) / (Zoom * ((float)Height / _waferHeight)) + YMin);

            // 触发鼠标移动事件

            WaferMapMouseMove?.Invoke(this, new WaferMapMouseMoveEventArgs(waferX.ToString(), waferY.ToString()));

            // Debug 输出
            //Debug.WriteLine($"wafer X: {waferX}, wafer Y: {waferY}.");
            //Debug.WriteLine($"Mouse X: {e.X}, Mouse Y: {e.Y}.");
        }

        protected virtual void WaferMap_MouseClick(object sender, MouseEventArgs e)
        {
            // 如果是画 Bin 模式，点击鼠标左键时，会在鼠标点击位置绘制一个灰色的 5 * 5 的正方形
            if (_isDrawBin)
            {
                _drawBinPoints.Add(new Point(e.X, e.Y));
                _binGraphics.FillRectangle(Brushes.Gray, e.X, e.Y, 5, 5);

                // 如果不是第一个点，绘制上一个点和当前点之间的线
                if (_drawBinPoints.Count - 1 > 0)
                {
                    _binGraphics.DrawLine(_drawBinPen, _drawBinPoints[_drawBinPoints.Count - 2], _drawBinPoints[_drawBinPoints.Count - 1]);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                _squareCenter = e.Location;
                contextMenuStrip1.Show(this, e.X, e.Y);
            }
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

        private void tsmiFixed_CheckedChanged(object sender, EventArgs e)
        {
            _fixed = tsmiFixed.Checked;
            Invalidate();
            Refresh();
            if (_fixed)
            {
                // 创建半透明白色画刷并绘制
                using (Graphics g = this.CreateGraphics())
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(_opacity, Color.White)))
                {
                    g.FillRectangle(brush, _squareCenter.X - _squareSize.Width / 2, _squareCenter.Y - _squareSize.Height / 2, _squareSize.Width, _squareSize.Height);

                    // 可选：绘制边框
                    using (Pen pen = new Pen(Color.FromArgb(Math.Min(255, _opacity + 50), Color.White)))
                    {
                        g.DrawRectangle(pen, _squareCenter.X - _squareSize.Width / 2, _squareCenter.Y - _squareSize.Height / 2, _squareSize.Width, _squareSize.Height);
                    }
                }
            }
        }

        private void tsmiFixed_Click(object sender, EventArgs e)
        {
            tsmiFixed.Checked = !tsmiFixed.Checked;
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
        public void SetBinColor(Color[] colors)
        {
            if (colors is null || colors.Length == 0)
            {
                Colors = SetBinColorDefault();  // 如果传入的数组为空或长度为 0, 就调用默认颜色
            }
            else
            {
                Colors = colors;
            }
        }

        private Color[] SetBinColorDefault()
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
            return colors;
        }

        /// <summary>
        /// 定位 Wafer 上的某个点，将该点移至控件的中心
        /// </summary>
        /// <param name="x">目标点的 X 坐标</param>
        /// <param name="y">目标点的 Y 坐标</param>
        public void SetPosition(int x, int y)
        {
            // 计算当前图像的绘制比例，结合 Zoom 缩放
            float xScale = (float)Width / _waferWidth * Zoom;
            float yScale = (float)Height / _waferHeight * Zoom;

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

        #region Draw bin methods

        // 记录上次鼠标点击的位置以及各个点的位置
        private int _lastX = -1;
        private int _lastY = -1;
        // 记录画 Bin 的点
        private List<Point> _drawBinPoints;
        // 画 Bin 时使用的画笔和 Graphics 对象
        private Pen _drawBinPen;
        private Graphics _binGraphics;

        public bool DrawBin()
        {
            bool result = false;

            if (_isDrawBin)  // 如果已经在画 Bin 模式下，再次点击按钮时，退出画 Bin 模式
            {
                // 需要判断是否能够构成多边形
                if (!IsValidPolygon(_drawBinPoints))
                {
                    MessageBox.Show("无法构成多边形，请重新绘制！");
                    CleanDrawBinHistory();
                    return true;
                }
                else
                {
                    _canModifyBin = true;
                }

                // 如果已经有 3 个以上的点，绘制最后一个点和第一个点之间的线
                if (_drawBinPoints.Count > 3)
                {
                    _binGraphics.DrawLine(_drawBinPen, _drawBinPoints[_drawBinPoints.Count - 1], _drawBinPoints[0]);
                }

                // 注意：即使 Dispose 了 Graphics 对象，也不会清除控件上的绘制内容，除非控件重绘
                if (_binGraphics != null)
                {
                    _binGraphics.Dispose();
                    _binGraphics = null;
                }

                if (_drawBinPen != null)
                {
                    _drawBinPen.Dispose();
                    _drawBinPen = null;
                }

                result = _isDrawBin = false;
            }
            else  // 否则，进入画 Bin 模式
            {
                // 重置相关变量
                _drawBinPoints = new List<Point>();
                _lastX = _lastY = -1;
                _canModifyBin = false;

                // 创建一个新的 Graphics 对象
                _binGraphics = this.CreateGraphics();

                // 创建一个新的画笔
                _drawBinPen = new Pen(Color.Silver, 2);

                result = _isDrawBin = true;
            }

            return result;
        }

        /// <summary>
        /// 画 Bin 撤销
        /// </summary>
        public void DrawBinUndo()
        {
            if (_isDrawBin && _drawBinPoints.Count > 0)  // 已经在画 Bin 模式下，且已绘制的点大于1时，才能撤销
            {
                // 从 List 删除最后绘制的点
                _drawBinPoints.RemoveAt(_drawBinPoints.Count - 1);

                // 执行重绘
                Invalidate();
                Refresh();

                // 重新画点和线
                if (_drawBinPoints.Count > 0)
                {
                    _binGraphics.FillRectangle(Brushes.Gray, _drawBinPoints[0].X, _drawBinPoints[0].Y, 5, 5);
                }
                for (int i = 1; i < _drawBinPoints.Count; i++)
                {
                    _binGraphics.FillRectangle(Brushes.Gray, _drawBinPoints[i].X, _drawBinPoints[i].Y, 5, 5);
                    _binGraphics.DrawLine(_drawBinPen, _drawBinPoints[i], _drawBinPoints[i - 1]);
                }
            }
        }

        /// <summary>
        /// 清理画 Bin 记录
        /// </summary>
        public void CleanDrawBinHistory()
        {
            // 重置相关变量
            _drawBinPoints = new List<Point>();
            _lastX = _lastY = -1;
            _canModifyBin = false;

            // 重绘控件
            RedrawWaferMap();
        }

        public void ModifyBin()
        {
            if (!_canModifyBin)
            {
                MessageBox.Show("请先画 Bin！");
                return;
            }

            RedrawWaferMap();
        }

        /// <summary>
        /// 计算叉积 (P2 - P1) x (P3 - P1)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private double CrossProduct(Point p1, Point p2, Point p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
        }

        /// <summary>
        /// 判断点 P3 是否在线段 P1P2 上（注意：调用此方法判断的前提是三点共线，即它们的叉积为 0）
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private bool IsPointOnSegment(Point p1, Point p2, Point p3)
        {
            return Math.Min(p1.X, p2.X) <= p3.X && p3.X <= Math.Max(p1.X, p2.X) &&
                   Math.Min(p1.Y, p2.Y) <= p3.Y && p3.Y <= Math.Max(p1.Y, p2.Y);
        }

        /// <summary>
        /// 判断线段 P1P2 和 P3P4 是否相交
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        private bool AreSegmentsIntersecting(Point p1, Point p2, Point p3, Point p4)
        {
            double d1 = CrossProduct(p3, p4, p1);
            double d2 = CrossProduct(p3, p4, p2);
            double d3 = CrossProduct(p1, p2, p3);
            double d4 = CrossProduct(p1, p2, p4);

            // 检查相交的基本条件
            if (d1 * d2 < 0 && d3 * d4 < 0)
            {
                return true;
            }

            // 检查共线的特殊情况
            if (d1 == 0 && IsPointOnSegment(p3, p4, p1)) return true;
            if (d2 == 0 && IsPointOnSegment(p3, p4, p2)) return true;
            if (d3 == 0 && IsPointOnSegment(p1, p2, p3)) return true;
            if (d4 == 0 && IsPointOnSegment(p1, p2, p4)) return true;

            return false;
        }

        /// <summary>
        /// 判断一组点是否构成有效的多边形。原理：
        /// 1. 验证多边形是否闭合
        /// 2. 验证多边形是否有自相交：对于每一对非相邻边，检查是否相交
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private bool IsValidPolygon(List<Point> points)
        {
            int n = points.Count;

            // 需要三个点以上
            if (n <= 3)
            {
                return false;
            }

            // 检查每一对非相邻边是否相交
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 2; j < n; j++)
                {
                    // 不比较相邻边和首尾相连的边
                    if (i == 0 && j == n - 1) continue;

                    if (AreSegmentsIntersecting(points[i], points[(i + 1) % n], points[j], points[(j + 1) % n]))
                    {
                        return false; // 存在相交
                    }
                }
            }
            return true; // 无相交
        }

        /// <summary>  
        /// 判断点是否在多边形内.  
        /// ----------原理----------  
        /// 注意到如果从P作水平向左的射线的话，如果P在多边形内部，那么这条射线与多边形的交点必为奇数，  
        /// 如果P在多边形外部，则交点个数必为偶数(0也在内)。  
        /// 所以，我们可以顺序考虑多边形的每条边，求出交点的总个数。还有一些特殊情况要考虑。假如考虑边(P1,P2)，  
        /// 1)如果射线正好穿过P1或者P2,那么这个交点会被算作2次，处理办法是如果P的从坐标与P1,P2中较小的纵坐标相同，则直接忽略这种情况  
        /// 2)如果射线水平，则射线要么与其无交点，要么有无数个，这种情况也直接忽略。  
        /// 3)如果射线竖直，而P0的横坐标小于P1,P2的横坐标，则必然相交。  
        /// 4)再判断相交之前，先判断P是否在边(P1,P2)的上面，如果在，则直接得出结论：P再多边形内部。  
        /// </summary>  
        /// <param name="checkPoint">要判断的点</param>  
        /// <param name="polygonPoints">多边形的顶点</param>  
        /// <returns></returns>  
        private bool IsInPolygon2(Point checkPoint, List<Point> polygonPoints)
        {
            try
            {
                int counter = 0;

                int pointCount = polygonPoints.Count;
                Point p1 = polygonPoints[0];
                for (int i = 1; i <= pointCount; i++)
                {
                    Point p2 = polygonPoints[i % pointCount];
                    if (checkPoint.Y > Math.Min(p1.Y, p2.Y) && checkPoint.Y <= Math.Max(p1.Y, p2.Y))//校验点的Y大于线段端点的最小Y //校验点的Y小于线段端点的最大Y  
                    {
                        if (checkPoint.X <= Math.Max(p1.X, p2.X))//校验点的X小于等线段端点的最大X(使用校验点的左射线判断).  
                        {
                            if (p1.Y != p2.Y)//线段不平行于X轴  
                            {
                                double xinters = (checkPoint.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
                                if (p1.X == p2.X || checkPoint.X <= xinters)
                                    counter++;
                            }
                        }
                    }
                    p1 = p2;
                }

                if (counter % 2 == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Color GetBinColor(int binNo)
        {
            return binNo < Colors.Length ? Colors[binNo] : Color.Black;
        }

        private Color GetBinColor(string binNo)
        {
            return Convert.ToInt32(binNo) < Colors.Length ? Colors[Convert.ToInt32(binNo)] : Color.Black;
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
