using DrawWaferMapApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawWaferMapApp.Controls
{
    public partial class MiniWaferMap : UserControl
    {
        public Color[] Colors { get; set; }  // Bin 颜色
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public CsvDetail Detail { get; set; }
        public int HalfOfTheSide { get; set; } = 10;

        private PictureBox waferMap;
        private Bitmap bitmap;
        private Graphics g;

        public MiniWaferMap()
        {
            InitializeComponent();
        }

        #region Event
        /// <summary>
        /// 组件加载时初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiniWaferMap_Load(object sender, EventArgs e)
        {
            InitializeOther();
            SetBinColor(null);
        }

        /// <summary>
        /// 重载绘制事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Detail == null)
            {
                DrawWhitePicture(e.Graphics);
                return;
            }

            DrawMiniMap(e.Graphics);
        }
        #endregion

        #region Private methods
        private void InitializeOther()
        {
            // pictureBox
            //waferMap = new PictureBox();
            //waferMap.Location = new Point(0, 0);
            //waferMap.Size = new Size(Math.Min(Width, Height), Math.Min(Width, Height));
            //waferMap.SizeMode = PictureBoxSizeMode.AutoSize;
            //this.Controls.Add(waferMap);
            //DrawWhitePicture();
        }

        /// <summary>
        /// 绘制白布
        /// </summary>
        private void DrawWhitePicture()
        {
            try
            {
                //if (bitmap != null)
                //{
                //    bitmap.Dispose();
                //}
                //if (g != null)
                //{
                //    g.Dispose();
                //}

                //bitmap = new Bitmap(waferMap.Width, waferMap.Height);
                //g = Graphics.FromImage(bitmap);
                //g.Clear(Color.White);
                //waferMap.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DrawWhitePicture(Graphics g)
        {
            try
            {
                g.Clear(Color.White);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 绘制迷你晶圆图
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="csvDetail"></param>
        /// <param name="halfOfTheSide"></param>
        private void DrawMiniMap(int x, int y, CsvDetail csvDetail, int halfOfTheSide = 10)
        {
            try
            {
                if (halfOfTheSide <= 0)
                {
                    halfOfTheSide = 10;
                }
                //if (bitmap != null)
                //{
                //    bitmap.Dispose();
                //}
                //if (g != null)
                //{
                //    g.Dispose();
                //}

                waferMap.Size = new Size(Math.Min(Width, Height), Math.Min(Width, Height));
                int rectangeSide = (int)Math.Round((double)waferMap.Width / (2 * halfOfTheSide + (2 * halfOfTheSide) / 5 + 1));
                int gap = rectangeSide / 5;
                bitmap = new Bitmap(waferMap.Width, waferMap.Height);
                g = Graphics.FromImage(bitmap);
                g.Clear(Color.White);

                for (int i = x - halfOfTheSide; i <= x + halfOfTheSide; i++)
                {
                    for (int j = y - halfOfTheSide; j <= y + halfOfTheSide; j++)
                    {
                        //if (csvDetail.DataType == DataStorageType.Dictionary)
                        //{
                        //    if (csvDetail.BodyInfo.ContainsKey(new Coordinate(i, j)))
                        //    {
                        //        string[] binInfo = csvDetail.BodyInfo[new Coordinate(i, j)];
                        //        if (binInfo.Length > 0)
                        //        {
                        //            Rectangle rect = new Rectangle((i - x + halfOfTheSide) * (rectangeSide + gap) + gap, (j - y + halfOfTheSide) * (rectangeSide + gap) + gap, rectangeSide, rectangeSide);
                        //            g.FillRectangle(new SolidBrush(GetBinColor(binInfo[0])), rect);
                        //        }
                        //    }
                        //}
                        //else if (csvDetail.DataType == DataStorageType.Matrix)
                        //{
                        //    if (i >= csvDetail.XMin && i <= csvDetail.XMax && j >= csvDetail.YMin && j <= csvDetail.YMax)
                        //    {
                        //        string[] binInfo = csvDetail.BodyInfo_Matrix[i - csvDetail.XMin, j - csvDetail.YMin];
                        //        if (binInfo.Length > 0)
                        //        {
                        //            Rectangle rect = new Rectangle((i - x + halfOfTheSide) * (rectangeSide + gap) + gap, (j - y + halfOfTheSide) * (rectangeSide + gap) + gap, rectangeSide, rectangeSide);
                        //            g.FillRectangle(new SolidBrush(GetBinColor(binInfo[0])), rect);
                        //        }
                        //    }
                        //}
                        if (i >= csvDetail.XMin && i <= csvDetail.XMax && j >= csvDetail.YMin && j <= csvDetail.YMax)
                        {
                            string[] binInfo = csvDetail.BodyInfo_Matrix[i - csvDetail.XMin, j - csvDetail.YMin];
                            if (binInfo != null && binInfo.Length > 0)
                            {
                                Rectangle rect = new Rectangle((i - x + halfOfTheSide) * (rectangeSide + gap), (j - y + halfOfTheSide) * (rectangeSide + gap), rectangeSide, rectangeSide);
                                g.FillRectangle(new SolidBrush(GetBinColor(binInfo[2])), rect);
                            }
                        }
                    }
                }

                waferMap.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DrawMiniMap(Graphics g)
        {
            try
            {
                if (HalfOfTheSide <= 0)
                {
                    HalfOfTheSide = 10;
                }

                int rectangeSide = (int)Math.Round((double)Width / (2 * HalfOfTheSide + (2 * HalfOfTheSide) / 5 + 1));
                int gap = rectangeSide / 5;
                //g.Clear(Color.White);

                for (int i = X - HalfOfTheSide; i <= X + HalfOfTheSide; i++)
                {
                    for (int j = Y - HalfOfTheSide; j <= Y + HalfOfTheSide; j++)
                    {
                        if (i >= Detail.XMin && i <= Detail.XMax && j >= Detail.YMin && j <= Detail.YMax)
                        {
                            string[] binInfo = Detail.BodyInfo_Matrix[i - Detail.XMin, j - Detail.YMin];
                            if (binInfo != null && binInfo.Length > 0)
                            {
                                Rectangle rect = new Rectangle((i - X + HalfOfTheSide) * (rectangeSide + gap), (j - Y + HalfOfTheSide) * (rectangeSide + gap), rectangeSide, rectangeSide);
                                g.FillRectangle(new SolidBrush(GetBinColor(binInfo[2])), rect);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private Color GetBinColor(int binNo)
        {
            return Colors[binNo];
        }

        private Color GetBinColor(string binNo)
        {
            return Colors[Convert.ToInt32(binNo)];
        }
        #endregion

        #region Public methods
        public void Redraw(int x, int y, CsvDetail csvDetail, int halfOfTheSide = 10) => DrawMiniMap(x, y, csvDetail, halfOfTheSide);
        public void Redraw() => Invalidate();
        #endregion
    }
}
