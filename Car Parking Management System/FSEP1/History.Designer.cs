namespace FSEP1
{
    partial class History
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(History));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gradientRectangle1 = new FSEP1.GradientRectangle();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ResultBox = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.gradientRectangle1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.gradientRectangle1);
            this.panel1.Controls.Add(this.ResultBox);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(870, 718);
            this.panel1.TabIndex = 0;
            // 
            // gradientRectangle1
            // 
            this.gradientRectangle1.BackColor = System.Drawing.SystemColors.Window;
            this.gradientRectangle1.BroderRadius = 30;
            this.gradientRectangle1.Controls.Add(this.button2);
            this.gradientRectangle1.Controls.Add(this.button1);
            this.gradientRectangle1.Controls.Add(this.label1);
            this.gradientRectangle1.ForeColor = System.Drawing.Color.Black;
            this.gradientRectangle1.GradientAngle = 90F;
            this.gradientRectangle1.GradientBottomColor = System.Drawing.Color.DarkRed;
            this.gradientRectangle1.GradientTopColor = System.Drawing.Color.Gray;
            this.gradientRectangle1.Location = new System.Drawing.Point(37, 12);
            this.gradientRectangle1.Name = "gradientRectangle1";
            this.gradientRectangle1.Size = new System.Drawing.Size(803, 75);
            this.gradientRectangle1.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(683, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 53);
            this.button1.TabIndex = 1;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "History";
            // 
            // ResultBox
            // 
            this.ResultBox.BackgroundColor = System.Drawing.Color.DimGray;
            this.ResultBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultBox.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ResultBox.Location = new System.Drawing.Point(37, 104);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.RowHeadersWidth = 51;
            this.ResultBox.RowTemplate.Height = 24;
            this.ResultBox.Size = new System.Drawing.Size(803, 598);
            this.ResultBox.TabIndex = 15;
            this.ResultBox.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultBox_CellContentClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(552, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 53);
            this.button2.TabIndex = 2;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(868, 714);
            this.Controls.Add(this.panel1);
            this.Name = "History";
            this.Text = "History";
            this.Load += new System.EventHandler(this.History_Load);
            this.panel1.ResumeLayout(false);
            this.gradientRectangle1.ResumeLayout(false);
            this.gradientRectangle1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView ResultBox;
        private GradientRectangle gradientRectangle1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}