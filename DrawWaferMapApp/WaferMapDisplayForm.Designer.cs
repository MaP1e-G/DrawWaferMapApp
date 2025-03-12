
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            this.ugb1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnDrawBinUndo = new Infragistics.Win.Misc.UltraButton();
            this.btnModifyBin = new Infragistics.Win.Misc.UltraButton();
            this.btnClean = new Infragistics.Win.Misc.UltraButton();
            this.btnDrawBin = new Infragistics.Win.Misc.UltraButton();
            this.txtBinNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblBinNo = new Infragistics.Win.Misc.UltraLabel();
            this.btnDrawMapByMatrix = new Infragistics.Win.Misc.UltraButton();
            this.btnDrawMapByDictionary = new Infragistics.Win.Misc.UltraButton();
            this.btnGetElectricalValue = new Infragistics.Win.Misc.UltraButton();
            this.btnGoPosition = new Infragistics.Win.Misc.UltraButton();
            this.txtYCoordinate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtXCoordinate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblYCoordinate = new Infragistics.Win.Misc.UltraLabel();
            this.lblXCoordinate = new Infragistics.Win.Misc.UltraLabel();
            this.utpWaferMap = new Infragistics.Win.Misc.UltraPanel();
            this.miniWaferMap1 = new DrawWaferMapApp.Controls.MiniWaferMap();
            ((System.ComponentModel.ISupportInitialize)(this.ugb1)).BeginInit();
            this.ugb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBinNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYCoordinate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXCoordinate)).BeginInit();
            this.utpWaferMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // ugb1
            // 
            this.ugb1.Controls.Add(this.btnDrawBinUndo);
            this.ugb1.Controls.Add(this.miniWaferMap1);
            this.ugb1.Controls.Add(this.btnModifyBin);
            this.ugb1.Controls.Add(this.btnClean);
            this.ugb1.Controls.Add(this.btnDrawBin);
            this.ugb1.Controls.Add(this.txtBinNo);
            this.ugb1.Controls.Add(this.lblBinNo);
            this.ugb1.Controls.Add(this.btnDrawMapByMatrix);
            this.ugb1.Controls.Add(this.btnDrawMapByDictionary);
            this.ugb1.Controls.Add(this.btnGetElectricalValue);
            this.ugb1.Controls.Add(this.btnGoPosition);
            this.ugb1.Controls.Add(this.txtYCoordinate);
            this.ugb1.Controls.Add(this.txtXCoordinate);
            this.ugb1.Controls.Add(this.lblYCoordinate);
            this.ugb1.Controls.Add(this.lblXCoordinate);
            this.ugb1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ugb1.Location = new System.Drawing.Point(849, 0);
            this.ugb1.Name = "ugb1";
            this.ugb1.Size = new System.Drawing.Size(335, 861);
            this.ugb1.TabIndex = 1;
            this.ugb1.Text = "ultraGroupBox1";
            // 
            // btnDrawBinUndo
            // 
            this.btnDrawBinUndo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawBinUndo.Location = new System.Drawing.Point(202, 219);
            this.btnDrawBinUndo.Name = "btnDrawBinUndo";
            this.btnDrawBinUndo.Size = new System.Drawing.Size(127, 23);
            this.btnDrawBinUndo.TabIndex = 15;
            this.btnDrawBinUndo.Text = "Draw Bin Undo";
            this.btnDrawBinUndo.Click += new System.EventHandler(this.btnDrawBinUndo_Click);
            // 
            // btnModifyBin
            // 
            this.btnModifyBin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModifyBin.Location = new System.Drawing.Point(202, 190);
            this.btnModifyBin.Name = "btnModifyBin";
            this.btnModifyBin.Size = new System.Drawing.Size(127, 23);
            this.btnModifyBin.TabIndex = 13;
            this.btnModifyBin.Text = "Modify Bin";
            this.btnModifyBin.Click += new System.EventHandler(this.btnModifyBin_Click);
            // 
            // btnClean
            // 
            this.btnClean.Enabled = false;
            this.btnClean.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClean.Location = new System.Drawing.Point(202, 161);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(127, 23);
            this.btnClean.TabIndex = 12;
            this.btnClean.Text = "Draw Bin Clean";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnDrawBin
            // 
            this.btnDrawBin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawBin.Location = new System.Drawing.Point(202, 132);
            this.btnDrawBin.Name = "btnDrawBin";
            this.btnDrawBin.Size = new System.Drawing.Size(127, 23);
            this.btnDrawBin.TabIndex = 11;
            this.btnDrawBin.Text = "Draw Bin(Start)";
            this.btnDrawBin.Click += new System.EventHandler(this.btnDrawBin_Click);
            // 
            // txtBinNo
            // 
            this.txtBinNo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBinNo.Location = new System.Drawing.Point(44, 82);
            this.txtBinNo.Name = "txtBinNo";
            this.txtBinNo.Size = new System.Drawing.Size(70, 25);
            this.txtBinNo.TabIndex = 10;
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
            // btnDrawMapByMatrix
            // 
            this.btnDrawMapByMatrix.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawMapByMatrix.Location = new System.Drawing.Point(202, 103);
            this.btnDrawMapByMatrix.Name = "btnDrawMapByMatrix";
            this.btnDrawMapByMatrix.Size = new System.Drawing.Size(127, 23);
            this.btnDrawMapByMatrix.TabIndex = 8;
            this.btnDrawMapByMatrix.Text = "Draw Map(Matrix)";
            this.btnDrawMapByMatrix.Click += new System.EventHandler(this.btnDrawMapByMatrix_Click);
            // 
            // btnDrawMapByDictionary
            // 
            this.btnDrawMapByDictionary.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawMapByDictionary.Location = new System.Drawing.Point(202, 74);
            this.btnDrawMapByDictionary.Name = "btnDrawMapByDictionary";
            this.btnDrawMapByDictionary.Size = new System.Drawing.Size(127, 23);
            this.btnDrawMapByDictionary.TabIndex = 6;
            this.btnDrawMapByDictionary.Text = "Draw Map(Dic)";
            this.btnDrawMapByDictionary.Click += new System.EventHandler(this.btnDrawWaferMap_Click);
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
            // miniWaferMap1
            // 
            this.miniWaferMap1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.miniWaferMap1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(0)))), ((int)(((byte)(156))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(130)))), ((int)(((byte)(1))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(154)))), ((int)(((byte)(254))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(0)))), ((int)(((byte)(205))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(101)))), ((int)(((byte)(207))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(154)))), ((int)(((byte)(206))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(154)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(181)))), ((int)(((byte)(74))))),
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Green,
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.Black};
            this.miniWaferMap1.Detail = null;
            this.miniWaferMap1.HalfOfTheSide = 10;
            this.miniWaferMap1.Location = new System.Drawing.Point(6, 526);
            this.miniWaferMap1.Name = "miniWaferMap1";
            this.miniWaferMap1.Size = new System.Drawing.Size(323, 323);
            this.miniWaferMap1.TabIndex = 14;
            this.miniWaferMap1.X = 0;
            this.miniWaferMap1.Y = 0;
            // 
            // WaferMapDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.utpWaferMap);
            this.Controls.Add(this.ugb1);
            this.Name = "WaferMapDisplayForm";
            this.Text = "txtBinNo";
            this.Load += new System.EventHandler(this.WaferMapDisplayForm_Load);
            this.Resize += new System.EventHandler(this.WaferMapDisplayForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ugb1)).EndInit();
            this.ugb1.ResumeLayout(false);
            this.ugb1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBinNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYCoordinate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXCoordinate)).EndInit();
            this.utpWaferMap.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Infragistics.Win.Misc.UltraGroupBox ugb1;
        private Infragistics.Win.Misc.UltraButton btnGetElectricalValue;
        private Infragistics.Win.Misc.UltraButton btnGoPosition;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtYCoordinate;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtXCoordinate;
        private Infragistics.Win.Misc.UltraLabel lblYCoordinate;
        private Infragistics.Win.Misc.UltraLabel lblXCoordinate;
        private Infragistics.Win.Misc.UltraButton btnDrawMapByDictionary;
        private Infragistics.Win.Misc.UltraPanel utpWaferMap;
        private Infragistics.Win.Misc.UltraButton btnDrawMapByMatrix;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtBinNo;
        private Infragistics.Win.Misc.UltraLabel lblBinNo;
        private Infragistics.Win.Misc.UltraButton btnDrawBin;
        private Infragistics.Win.Misc.UltraButton btnClean;
        private Infragistics.Win.Misc.UltraButton btnModifyBin;
        private Controls.MiniWaferMap miniWaferMap1;
        private Infragistics.Win.Misc.UltraButton btnDrawBinUndo;
    }
}