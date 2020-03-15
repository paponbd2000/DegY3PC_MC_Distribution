namespace BBAAbsentList
{
    partial class DegY3PCMCHome
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.numericUpDownExamYear = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxExamCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExamYear)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(88, 163);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(44, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Start";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // buttonFinish
            // 
            this.buttonFinish.Enabled = false;
            this.buttonFinish.Location = new System.Drawing.Point(161, 163);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(54, 23);
            this.buttonFinish.TabIndex = 5;
            this.buttonFinish.Text = "Finish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // numericUpDownExamYear
            // 
            this.numericUpDownExamYear.Location = new System.Drawing.Point(161, 85);
            this.numericUpDownExamYear.Maximum = new decimal(new int[] {
            2024,
            0,
            0,
            0});
            this.numericUpDownExamYear.Minimum = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.numericUpDownExamYear.Name = "numericUpDownExamYear";
            this.numericUpDownExamYear.ReadOnly = true;
            this.numericUpDownExamYear.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownExamYear.TabIndex = 3;
            this.numericUpDownExamYear.Value = new decimal(new int[] {
            2018,
            0,
            0,
            0});
            this.numericUpDownExamYear.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Exam Year:";
            this.label2.Visible = false;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBoxExamCode
            // 
            this.comboBoxExamCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExamCode.FormattingEnabled = true;
            this.comboBoxExamCode.Items.AddRange(new object[] {
            "601",
            "602",
            "603",
            "604",
            "605",
            "606",
            "607",
            "608"});
            this.comboBoxExamCode.Location = new System.Drawing.Point(160, 45);
            this.comboBoxExamCode.Name = "comboBoxExamCode";
            this.comboBoxExamCode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxExamCode.TabIndex = 2;
            this.comboBoxExamCode.Visible = false;
            this.comboBoxExamCode.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Exam Code:";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // DegY3AbsentListHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 267);
            this.Controls.Add(this.buttonFinish);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.numericUpDownExamYear);
            this.Controls.Add(this.comboBoxExamCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DegY3AbsentListHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Degree Absent List Generation";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExamYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.NumericUpDown numericUpDownExamYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxExamCode;
        private System.Windows.Forms.Label label1;
    }
}