﻿
namespace DrawWaferMapApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnStreamReader = new Infragistics.Win.Misc.UltraButton();
            this.btnFileReadLines = new Infragistics.Win.Misc.UltraButton();
            this.btnFileReadAllLines = new Infragistics.Win.Misc.UltraButton();
            this.btnCsvRead_Matrix = new Infragistics.Win.Misc.UltraButton();
            this.btnAsyncRead = new Infragistics.Win.Misc.UltraButton();
            this.btnCsvRead_Dictionary = new Infragistics.Win.Misc.UltraButton();
            this.btnShowMap = new Infragistics.Win.Misc.UltraButton();
            this.btnSelect = new Infragistics.Win.Misc.UltraButton();
            this.txtMapPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraSplitter1 = new Infragistics.Win.Misc.UltraSplitter();
            this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraStatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.btnStreamReader);
            this.ultraGroupBox1.Controls.Add(this.btnFileReadLines);
            this.ultraGroupBox1.Controls.Add(this.btnFileReadAllLines);
            this.ultraGroupBox1.Controls.Add(this.btnCsvRead_Matrix);
            this.ultraGroupBox1.Controls.Add(this.btnAsyncRead);
            this.ultraGroupBox1.Controls.Add(this.btnCsvRead_Dictionary);
            this.ultraGroupBox1.Controls.Add(this.btnShowMap);
            this.ultraGroupBox1.Controls.Add(this.btnSelect);
            this.ultraGroupBox1.Controls.Add(this.txtMapPath);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel1);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(800, 200);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "ultraGroupBox1";
            // 
            // btnStreamReader
            // 
            this.btnStreamReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStreamReader.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStreamReader.Location = new System.Drawing.Point(6, 107);
            this.btnStreamReader.Name = "btnStreamReader";
            this.btnStreamReader.Size = new System.Drawing.Size(155, 25);
            this.btnStreamReader.TabIndex = 9;
            this.btnStreamReader.Text = "StreamReader";
            this.btnStreamReader.Click += new System.EventHandler(this.btnStreamReader_Click);
            // 
            // btnFileReadLines
            // 
            this.btnFileReadLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileReadLines.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFileReadLines.Location = new System.Drawing.Point(6, 76);
            this.btnFileReadLines.Name = "btnFileReadLines";
            this.btnFileReadLines.Size = new System.Drawing.Size(155, 25);
            this.btnFileReadLines.TabIndex = 8;
            this.btnFileReadLines.Text = "File.ReadLines";
            this.btnFileReadLines.Click += new System.EventHandler(this.btnFileReadLines_Click);
            // 
            // btnFileReadAllLines
            // 
            this.btnFileReadAllLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileReadAllLines.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFileReadAllLines.Location = new System.Drawing.Point(6, 45);
            this.btnFileReadAllLines.Name = "btnFileReadAllLines";
            this.btnFileReadAllLines.Size = new System.Drawing.Size(155, 25);
            this.btnFileReadAllLines.TabIndex = 7;
            this.btnFileReadAllLines.Text = "File.ReadAllLines";
            this.btnFileReadAllLines.Click += new System.EventHandler(this.btnFileReadAllLines_Click);
            // 
            // btnCsvRead_Matrix
            // 
            this.btnCsvRead_Matrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCsvRead_Matrix.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCsvRead_Matrix.Location = new System.Drawing.Point(688, 107);
            this.btnCsvRead_Matrix.Name = "btnCsvRead_Matrix";
            this.btnCsvRead_Matrix.Size = new System.Drawing.Size(106, 25);
            this.btnCsvRead_Matrix.TabIndex = 6;
            this.btnCsvRead_Matrix.Text = "CSV Read Matrix";
            this.btnCsvRead_Matrix.Click += new System.EventHandler(this.btnCsvRead_Matrix_Click);
            // 
            // btnAsyncRead
            // 
            this.btnAsyncRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAsyncRead.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAsyncRead.Location = new System.Drawing.Point(688, 138);
            this.btnAsyncRead.Name = "btnAsyncRead";
            this.btnAsyncRead.Size = new System.Drawing.Size(106, 25);
            this.btnAsyncRead.TabIndex = 5;
            this.btnAsyncRead.Text = "Async Read";
            this.btnAsyncRead.Click += new System.EventHandler(this.btnAsyncRead_Click);
            // 
            // btnCsvRead_Dictionary
            // 
            this.btnCsvRead_Dictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCsvRead_Dictionary.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCsvRead_Dictionary.Location = new System.Drawing.Point(688, 76);
            this.btnCsvRead_Dictionary.Name = "btnCsvRead_Dictionary";
            this.btnCsvRead_Dictionary.Size = new System.Drawing.Size(106, 25);
            this.btnCsvRead_Dictionary.TabIndex = 4;
            this.btnCsvRead_Dictionary.Text = "CSV Read Dictionary";
            this.btnCsvRead_Dictionary.Click += new System.EventHandler(this.btnCsvReadTest_Click);
            // 
            // btnShowMap
            // 
            this.btnShowMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowMap.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShowMap.Location = new System.Drawing.Point(688, 45);
            this.btnShowMap.Name = "btnShowMap";
            this.btnShowMap.Size = new System.Drawing.Size(106, 25);
            this.btnShowMap.TabIndex = 3;
            this.btnShowMap.Text = "Show Map";
            this.btnShowMap.Click += new System.EventHandler(this.btnShowMap_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Location = new System.Drawing.Point(688, 14);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(106, 25);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtMapPath
            // 
            this.txtMapPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMapPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMapPath.Location = new System.Drawing.Point(118, 15);
            this.txtMapPath.Name = "txtMapPath";
            this.txtMapPath.Size = new System.Drawing.Size(564, 25);
            this.txtMapPath.TabIndex = 1;
            // 
            // ultraLabel1
            // 
            appearance2.TextHAlignAsString = "Right";
            appearance2.TextVAlignAsString = "Middle";
            this.ultraLabel1.Appearance = appearance2;
            this.ultraLabel1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ultraLabel1.Location = new System.Drawing.Point(12, 19);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(100, 19);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Map Path";
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 210);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(800, 217);
            this.ultraGroupBox2.TabIndex = 1;
            this.ultraGroupBox2.Text = "ultraGroupBox2";
            // 
            // ultraSplitter1
            // 
            this.ultraSplitter1.BackColor = System.Drawing.SystemColors.Control;
            this.ultraSplitter1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
            this.ultraSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraSplitter1.Location = new System.Drawing.Point(0, 200);
            this.ultraSplitter1.Name = "ultraSplitter1";
            this.ultraSplitter1.RestoreExtent = 200;
            this.ultraSplitter1.Size = new System.Drawing.Size(800, 10);
            this.ultraSplitter1.TabIndex = 2;
            // 
            // ultraStatusBar1
            // 
            this.ultraStatusBar1.Location = new System.Drawing.Point(0, 427);
            this.ultraStatusBar1.Name = "ultraStatusBar1";
            this.ultraStatusBar1.Size = new System.Drawing.Size(800, 23);
            this.ultraStatusBar1.TabIndex = 0;
            this.ultraStatusBar1.Text = "ultraStatusBar1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ultraGroupBox2);
            this.Controls.Add(this.ultraStatusBar1);
            this.Controls.Add(this.ultraSplitter1);
            this.Controls.Add(this.ultraGroupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraStatusBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.Misc.UltraSplitter ultraSplitter1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraButton btnShowMap;
        private Infragistics.Win.Misc.UltraButton btnSelect;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtMapPath;
        private Infragistics.Win.Misc.UltraButton btnCsvRead_Dictionary;
        private Infragistics.Win.Misc.UltraButton btnAsyncRead;
        private Infragistics.Win.Misc.UltraButton btnCsvRead_Matrix;
        private Infragistics.Win.UltraWinStatusBar.UltraStatusBar ultraStatusBar1;
        private Infragistics.Win.Misc.UltraButton btnStreamReader;
        private Infragistics.Win.Misc.UltraButton btnFileReadLines;
        private Infragistics.Win.Misc.UltraButton btnFileReadAllLines;
    }
}

