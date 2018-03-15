using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        public double value;

        private void okButton_Click(object sender, EventArgs e)
        {
            value = (double)numericUpDown1.Value;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
