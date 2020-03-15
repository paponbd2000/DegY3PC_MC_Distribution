using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBAAbsentList
{
    public partial class DegY3PCMCHome : Form
    {
        public DegY3PCMCHome()
        {
            InitializeComponent();
            comboBoxExamCode.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            String examCode = comboBoxExamCode.Text;
            String examYear = numericUpDownExamYear.Value.ToString();
            buttonOk.Enabled = false;
            // Printer absentListPrinter = new Printer(examCode, examYear);
            PrinterDegY3 absentListPrinter = new PrinterDegY3();
            buttonFinish.Enabled = true;
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
