
namespace DrawWaferMapApp
{
    partial class WaferMapDisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
            this.ptbNeighborPoint = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this.btnDrawWaferMap = new Infragistics.Win.Misc.UltraButton();
            this.btnGetElectricalValue = new Infragistics.Win.Misc.UltraButton();
            this.btnGoPosition = new Infragistics.Win.Misc.UltraButton();
            this.txtYCoordinate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtXCoordinate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblYCoordinate = new Infragistics.Win.Misc.UltraLabel();
            this.lblXCoordinate = new Infragistics.Win.Misc.UltraLabel();
            this.utpWaferMap = new Infragistics.Win.Misc.UltraPanel();
            this.lblBinNo = new Infragistics.Win.Misc.UltraLabel();
            this.txtBinNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYCoordinate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXCoordinate)).BeginInit();
            this.utpWaferMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBinNo)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.txtBinNo);
            this.ultraGroupBox1.Controls.Add(this.lblBinNo);
            this.ultraGroupBox1.Controls.Add(this.ultraButton1);
            this.ultraGroupBox1.Controls.Add(this.ptbNeighborPoint);
            this.ultraGroupBox1.Controls.Add(this.btnDrawWaferMap);
            this.ultraGroupBox1.Controls.Add(this.btnGetElectricalValue);
            this.ultraGroupBox1.Controls.Add(this.btnGoPosition);
            this.ultraGroupBox1.Controls.Add(this.txtYCoordinate);
            this.ultraGroupBox1.Controls.Add(this.txtXCoordinate);
            this.ultraGroupBox1.Controls.Add(this.lblYCoordinate);
            this.ultraGroupBox1.Controls.Add(this.lblXCoordinate);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ultraGroupBox1.Location = new System.Drawing.Point(849, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(335, 861);
            this.ultraGroupBox1.TabIndex = 1;
            this.ultraGroupBox1.Text = "ultraGroupBox1";
            // 
            // ultraButton1
            // 
            this.ultraButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraButton1.Location = new System.Drawing.Point(202, 103);
            this.ultraButton1.Name = "ultraButton1";
            this.ultraButton1.Size = new System.Drawing.Size(127, 23);
            this.ultraButton1.TabIndex = 8;
            this.ultraButton1.Text = "Draw Wafer Map";
            // 
            // ptbNeighborPoint
            // 
            this.ptbNeighborPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ptbNeighborPoint.BorderShadowColor = System.Drawing.Color.Empty;
            this.ptbNeighborPoint.Location = new System.Drawing.Point(6, 532);
            this.ptbNeighborPoint.Name = "ptbNeighborPoint";
            this.ptbNeighborPoint.Size = new System.Drawing.Size(323, 323);
            this.ptbNeighborPoint.TabIndex = 7;
            // 
            // btnDrawWaferMap
            // 
            this.btnDrawWaferMap.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawWaferMap.Location = new System.Drawing.Point(202, 74);
            this.btnDrawWaferMap.Name = "btnDrawWaferMap";
            this.btnDrawWaferMap.Size = new System.Drawing.Size(127, 23);
            this.btnDrawWaferMap.TabIndex = 6;
            this.btnDrawWaferMap.Text = "Draw Wafer Map";
            this.btnDrawWaferMap.Click += new System.EventHandler(this.btnDrawWaferMap_Click);
            // 
            // btnGetElectricalValue
            // 
            this.btnGetElectricalValue.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGetElectricalValue.Location = new System.Drawing.Point(202, 45);
            this.btnGetElectricalValue.Name = "btnGetElectricalValue";
            this.btnGetElectricalValue.Size = new System.Drawing.Size(127, 23);
            this.btnGetElectricalValue.TabIndex = 5;
            this.btnGetElectricalValue.Text = "Get Electrical";
            this.btnGetElectricalValue.Click += new System.EventHandler(this.btnGetElectricalValue_Click);
            // 
            // btnGoPosition
            // 
            this.btnGoPosition.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGoPosition.Location = new System.Drawing.Point(202, 16);
            this.btnGoPosition.Name = "btnGoPosition";
            this.btnGoPosition.Size = new System.Drawing.Size(127, 23);
            this.btnGoPosition.TabIndex = 4;
            this.btnGoPosition.Text = "Go Position";
            this.btnGoPosition.Click += new System.EventHandler(this.btnGoPosition_Click);
            // 
            // txtYCoordinate
            // 
            this.txtYCoordinate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtYCoordinate.Location = new System.Drawing.Point(44, 51);
            this.txtYCoordinate.Name = "txtYCoordinate";
            this.txtYCoordinate.Size = new System.Drawing.Size(70, 25);
            this.txtYCoordinate.TabIndex = 3;
            // 
            // txtXCoordinate
            // 
            this.txtXCoordinate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtXCoordinate.Location = new System.Drawing.Point(44, 20);
            this.txtXCoordinate.Name = "txtXCoordinate";
            this.txtXCoordinate.Size = new System.Drawing.Size(70, 25);
            this.txtXCoordinate.TabIndex = 2;
            // 
            // lblYCoordinate
            // 
            appearance2.TextHAlignAsString = "Center";
            appearance2.TextVAlignAsString = "Middle";
            this.lblYCoordinate.Appearance = appearance2;
            this.lblYCoordinate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYCoordinate.Location = new System.Drawing.Point(6, 53);
            this.lblYCoordinate.Name = "lblYCoordinate";
            this.lblYCoordinate.Size = new System.Drawing.Size(32, 23);
            this.lblYCoordinate.TabIndex = 1;
            this.lblYCoordinate.Text = "Y:";
            // 
            // lblXCoordinate
            // 
            appearance3.TextHAlignAsString = "Center";
            appearance3.TextVAlignAsString = "Middle";
            this.lblXCoordinate.Appearance = appearance3;
            this.lblXCoordinate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblXCoordinate.Location = new System.Drawing.Point(6, 22);
            this.lblXCoordinate.Name = "lblXCoordinate";
            this.lblXCoordinate.Size = new System.Drawing.Size(32, 23);
            this.lblXCoordinate.TabIndex = 0;
            this.lblXCoordinate.Text = "X:";
            // 
            // utpWaferMap
            // 
            this.utpWaferMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.utpWaferMap.Location = new System.Drawing.Point(12, 12);
            this.utpWaferMap.Name = "utpWaferMap";
            this.utpWaferMap.Size = new System.Drawing.Size(831, 837);
            this.utpWaferMap.TabIndex = 2;
            // 
            // lblBinNo
            // 
            appearance1.TextHAlignAsString = "Center";
            appearance1.TextVAlignAsString = "Middle";
            this.lblBinNo.Appearance = appearance1;
            this.lblBinNo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBinNo.Location = new System.Drawing.Point(6, 84);
            this.lblBinNo.Name = "lblBinNo";
            this.lblBinNo.Size = new System.Drawing.Size(32, 23);
            this.lblBinNo.TabIndex = 9;
            this.lblBinNo.Text = "BIN:";
            // 
            // txtBinNo
            // 
            this.txtBinNo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBinNo.Location = new System.Drawing.Point(44, 82);
            this.txtBinNo.Name = "txtBinNo";
            this.txtBinNo.Size = new System.Drawing.Size(70, 25);
            this.txtBinNo.TabIndex = 10;
            // 
            // WaferMapDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.utpWaferMap);
            this.Controls.Add(this.ultraGroupBox1);
            this.Name = "WaferMapDisplayForm";
            this.Text = "txtBinNo";
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYCoordinate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXCoordinate)).EndInit();
            this.utpWaferMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBinNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraButton btnGetElectricalValue;
        private Infragistics.Win.Misc.UltraButton btnGoPosition;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtYCoordinate;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtXCoordinate;
        private Infragistics.Win.Misc.UltraLabel lblYCoordinate;
        private Infragistics.Win.Misc.UltraLabel lblXCoordinate;
        private Infragistics.Win.Misc.UltraButton btnDrawWaferMap;
        private Infragistics.Win.UltraWinEditors.UltraPictureBox ptbNeighborPoint;
        private Infragistics.Win.Misc.UltraPanel utpWaferMap;
        private Infragistics.Win.Misc.UltraButton ultraButton1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtBinNo;
        private Infragistics.Win.Misc.UltraLabel lblBinNo;
    }
}