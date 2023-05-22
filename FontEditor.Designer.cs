
namespace wdfe
{
    partial class FontEditor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontEditor));
            Button_OpenFont = new System.Windows.Forms.Button();
            Button_ExportBMP = new System.Windows.Forms.Button();
            Button_ImportBMP = new System.Windows.Forms.Button();
            Button_SaveFont = new System.Windows.Forms.Button();
            CharSelect = new System.Windows.Forms.NumericUpDown();
            ViewCharBox = new System.Windows.Forms.PictureBox();
            CharWidthSetting = new System.Windows.Forms.TrackBar();
            Label_CharWidthSetting = new System.Windows.Forms.Label();
            Label_Version = new System.Windows.Forms.Label();
            Label_Credit = new System.Windows.Forms.Label();
            Log = new System.Windows.Forms.TextBox();
            Button_SaveSymbol = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)CharSelect).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ViewCharBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CharWidthSetting).BeginInit();
            SuspendLayout();
            // 
            // Button_OpenFont
            // 
            Button_OpenFont.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Button_OpenFont.Location = new System.Drawing.Point(77, 0);
            Button_OpenFont.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            Button_OpenFont.Name = "Button_OpenFont";
            Button_OpenFont.Size = new System.Drawing.Size(113, 22);
            Button_OpenFont.TabIndex = 0;
            Button_OpenFont.Text = "Open Font";
            Button_OpenFont.UseVisualStyleBackColor = true;
            Button_OpenFont.Click += Button_OpenFont_Click;
            // 
            // Button_ExportBMP
            // 
            Button_ExportBMP.Enabled = false;
            Button_ExportBMP.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Button_ExportBMP.Location = new System.Drawing.Point(95, 259);
            Button_ExportBMP.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            Button_ExportBMP.Name = "Button_ExportBMP";
            Button_ExportBMP.Size = new System.Drawing.Size(95, 22);
            Button_ExportBMP.TabIndex = 5;
            Button_ExportBMP.Text = "Export Symbol";
            Button_ExportBMP.UseVisualStyleBackColor = true;
            Button_ExportBMP.Click += Button_ExportBMP_Click;
            // 
            // Button_ImportBMP
            // 
            Button_ImportBMP.Enabled = false;
            Button_ImportBMP.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Button_ImportBMP.Location = new System.Drawing.Point(1, 259);
            Button_ImportBMP.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            Button_ImportBMP.Name = "Button_ImportBMP";
            Button_ImportBMP.Size = new System.Drawing.Size(95, 22);
            Button_ImportBMP.TabIndex = 4;
            Button_ImportBMP.Text = "Import Symbol";
            Button_ImportBMP.UseVisualStyleBackColor = true;
            Button_ImportBMP.Click += Button_ImportBMP_Click;
            // 
            // Button_SaveFont
            // 
            Button_SaveFont.Enabled = false;
            Button_SaveFont.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Button_SaveFont.ForeColor = System.Drawing.Color.Green;
            Button_SaveFont.Location = new System.Drawing.Point(77, 22);
            Button_SaveFont.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            Button_SaveFont.Name = "Button_SaveFont";
            Button_SaveFont.Size = new System.Drawing.Size(113, 22);
            Button_SaveFont.TabIndex = 2;
            Button_SaveFont.Text = "Export Font";
            Button_SaveFont.UseVisualStyleBackColor = true;
            Button_SaveFont.Click += Button_SaveFont_Click;
            // 
            // CharSelect
            // 
            CharSelect.BackColor = System.Drawing.Color.Black;
            CharSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            CharSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            CharSelect.Enabled = false;
            CharSelect.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            CharSelect.ForeColor = System.Drawing.Color.White;
            CharSelect.Location = new System.Drawing.Point(1, 24);
            CharSelect.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            CharSelect.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            CharSelect.Name = "CharSelect";
            CharSelect.Size = new System.Drawing.Size(74, 19);
            CharSelect.TabIndex = 1;
            CharSelect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            CharSelect.Value = new decimal(new int[] { 1, 0, 0, 0 });
            CharSelect.ValueChanged += CharSelect_ValueChanged;
            // 
            // ViewCharBox
            // 
            ViewCharBox.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            ViewCharBox.Location = new System.Drawing.Point(1, 45);
            ViewCharBox.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            ViewCharBox.Name = "ViewCharBox";
            ViewCharBox.Size = new System.Drawing.Size(189, 183);
            ViewCharBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            ViewCharBox.TabIndex = 5;
            ViewCharBox.TabStop = false;
            // 
            // CharWidthSetting
            // 
            CharWidthSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            CharWidthSetting.Enabled = false;
            CharWidthSetting.Location = new System.Drawing.Point(-3, 240);
            CharWidthSetting.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            CharWidthSetting.Maximum = 35;
            CharWidthSetting.Name = "CharWidthSetting";
            CharWidthSetting.Size = new System.Drawing.Size(169, 45);
            CharWidthSetting.TabIndex = 3;
            CharWidthSetting.TickStyle = System.Windows.Forms.TickStyle.None;
            CharWidthSetting.Value = 1;
            CharWidthSetting.ValueChanged += CharWidthSetting_ValueChanged;
            // 
            // Label_CharWidthSetting
            // 
            Label_CharWidthSetting.AutoSize = true;
            Label_CharWidthSetting.BackColor = System.Drawing.Color.Transparent;
            Label_CharWidthSetting.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_CharWidthSetting.ForeColor = System.Drawing.Color.White;
            Label_CharWidthSetting.Location = new System.Drawing.Point(164, 244);
            Label_CharWidthSetting.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            Label_CharWidthSetting.Name = "Label_CharWidthSetting";
            Label_CharWidthSetting.Size = new System.Drawing.Size(14, 13);
            Label_CharWidthSetting.TabIndex = 7;
            Label_CharWidthSetting.Text = "1";
            // 
            // Label_Version
            // 
            Label_Version.AutoSize = true;
            Label_Version.Cursor = System.Windows.Forms.Cursors.Hand;
            Label_Version.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Version.ForeColor = System.Drawing.Color.White;
            Label_Version.Location = new System.Drawing.Point(5, 5);
            Label_Version.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            Label_Version.Name = "Label_Version";
            Label_Version.Size = new System.Drawing.Size(60, 13);
            Label_Version.TabIndex = 9;
            Label_Version.Text = "WDFE 0.0";
            Label_Version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            Label_Version.Click += LabelVersion_Click;
            // 
            // Label_Credit
            // 
            Label_Credit.AutoSize = true;
            Label_Credit.Cursor = System.Windows.Forms.Cursors.Hand;
            Label_Credit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Label_Credit.ForeColor = System.Drawing.Color.White;
            Label_Credit.Location = new System.Drawing.Point(102, 304);
            Label_Credit.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            Label_Credit.Name = "Label_Credit";
            Label_Credit.Size = new System.Drawing.Size(88, 12);
            Label_Credit.TabIndex = 10;
            Label_Credit.Text = "made by emuyia";
            Label_Credit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            Label_Credit.Click += LabelCredit_Click;
            // 
            // Log
            // 
            Log.BackColor = System.Drawing.Color.Black;
            Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            Log.Cursor = System.Windows.Forms.Cursors.Hand;
            Log.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Log.ForeColor = System.Drawing.Color.DarkGray;
            Log.Location = new System.Drawing.Point(1, 228);
            Log.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            Log.Name = "Log";
            Log.PlaceholderText = "...";
            Log.ReadOnly = true;
            Log.RightToLeft = System.Windows.Forms.RightToLeft.No;
            Log.Size = new System.Drawing.Size(189, 12);
            Log.TabIndex = 11;
            Log.TabStop = false;
            Log.Text = "Open a font to start...";
            Log.WordWrap = false;
            Log.Click += Log_Click;
            // 
            // Button_SaveSymbol
            // 
            Button_SaveSymbol.Enabled = false;
            Button_SaveSymbol.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Button_SaveSymbol.ForeColor = System.Drawing.Color.Green;
            Button_SaveSymbol.Location = new System.Drawing.Point(1, 280);
            Button_SaveSymbol.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            Button_SaveSymbol.Name = "Button_SaveSymbol";
            Button_SaveSymbol.Size = new System.Drawing.Size(189, 22);
            Button_SaveSymbol.TabIndex = 7;
            Button_SaveSymbol.Text = "Save Symbol";
            Button_SaveSymbol.UseVisualStyleBackColor = true;
            Button_SaveSymbol.Click += Button_SaveSymbol_Click;
            // 
            // FontEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
            ClientSize = new System.Drawing.Size(191, 318);
            Controls.Add(Button_ExportBMP);
            Controls.Add(Button_ImportBMP);
            Controls.Add(ViewCharBox);
            Controls.Add(Button_SaveSymbol);
            Controls.Add(Log);
            Controls.Add(Label_Credit);
            Controls.Add(Label_Version);
            Controls.Add(Button_SaveFont);
            Controls.Add(Label_CharWidthSetting);
            Controls.Add(CharWidthSetting);
            Controls.Add(CharSelect);
            Controls.Add(Button_OpenFont);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            MaximizeBox = false;
            Name = "FontEditor";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "White Day Font Editor";
            Load += FontEditor_Load;
            KeyDown += FontEditor_KeyDown;
            ((System.ComponentModel.ISupportInitialize)CharSelect).EndInit();
            ((System.ComponentModel.ISupportInitialize)ViewCharBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)CharWidthSetting).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button Button_OpenFont;
        private System.Windows.Forms.Button Button_ExportBMP;
        private System.Windows.Forms.Button Button_ImportBMP;
        private System.Windows.Forms.Button Button_SaveFont;
        private System.Windows.Forms.NumericUpDown CharSelect;
        private System.Windows.Forms.PictureBox ViewCharBox;
        private System.Windows.Forms.TrackBar CharWidthSetting;
        private System.Windows.Forms.Label Label_CharWidthSetting;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.Label Label_Credit;
        private System.Windows.Forms.TextBox Log;
        private System.Windows.Forms.Button Button_SaveSymbol;
    }
}

