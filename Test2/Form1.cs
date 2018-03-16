using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Numerics;


namespace Test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CreditCalculator creditCalculator;

        private void fillingTableButton_Click(object sender, EventArgs e)
        {
            creditDataTable.Rows.Clear();

            double creditSumm = (double)creditSummBox.Value;
            int mounthsCount = (int)mounthsCountBox.Value;
            creditCalculator = new CreditCalculator(creditSumm, mounthsCount);

            fillTable(creditCalculator);
        }

        private void fillTableRow(CreditCalculator cc, int index)
        {
            if(cc == null)
            {
                MessageBox.Show("Таблица не создана", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            creditDataTable.Rows.Add();
            creditDataTable.Rows[index].Cells[0].Value = index + 1;
            creditDataTable.Rows[index].Cells[1].Value = cc.getDatas()[index].debtPayment.ToString("0.00");
            creditDataTable.Rows[index].Cells[2].Value = cc.getDatas()[index].creditProcent.ToString("0.00");
            creditDataTable.Rows[index].Cells[3].Value = cc.getMountPayment().ToString("0.00");
            creditDataTable.Rows[index].Cells[4].Value = cc.getDatas()[index].debtRemainder.ToString("0.00");
            creditDataTable.Rows[index].Cells[5].Value = cc.getDatas()[index].prepayment.ToString();            
        }

        private void fillTable(CreditCalculator cc)
        {
            for (int i = 0; i < creditCalculator.getTopIndex(); i++)
            {                
                if (double.IsNaN(creditCalculator.getDatas()[i].debtRemainder))
                    break;
                fillTableRow(creditCalculator, i);
            }
                
        }

        private void creditDataTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (creditCalculator == null)
                return;
            if (e.ColumnIndex == 5)
            {
                double newPrepayment = openInputForm();
                if(newPrepayment > 0)
                {
                    creditCalculator.changeDatas(e.RowIndex, newPrepayment);
                    creditDataTable.Rows.Clear();
                    fillTable(creditCalculator);
                }
               
            }
        }

        private double openInputForm()
        {
            InputForm inputForm = new InputForm();
            inputForm.ShowDialog(this);
            return inputForm.value;
        }
    }
}
