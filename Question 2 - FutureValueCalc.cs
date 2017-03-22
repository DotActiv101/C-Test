using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FVCalc
{
    public partial class FutureValueCalc : Form
    {
        int CompoundFreq = 0; // Initial variables
        double Balance = 0;
        double IntPerPeriod = 0;
        double OpenBal = 0;
        double CloseBal = 0;
        double CumInterest = 0;
        public FutureValueCalc()
        {
            InitializeComponent();

            cmbCompoundFreq.DisplayMember = "Text";
            cmbCompoundFreq.ValueMember = "Value";

            var items = new[] {
    new { Text = "Monthly", Value = "12" }, // Fill ComboBox with different items and values for each item.
    new { Text = "Querterly", Value = "4" },
    new { Text = "Half-yearly", Value = "2" },
    new { Text = "Annually", Value = "1" },
        };
            cmbCompoundFreq.DataSource = items;
        }

        // Calculate and display Future Value and Interest for the Investment Period.

        private void btnCalculate_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();

            double PrincipleAmount = double.Parse(txtPrinciple.Text); // Convert text to numerical values.
            double InterestRate = double.Parse(txtInterestRate.Text);
            int numYears = (int.Parse(txtInvTerm.Text));
            int numPeriods = numYears * CompoundFreq;

            string[] arr = new string[4];
            ListViewItem itm;

            Balance = PrincipleAmount * Math.Pow(1 + InterestRate / 100 / CompoundFreq, numYears * CompoundFreq);
            txtFutureValue.Text = Balance.ToString("n2");
            CumInterest = 0;

            for (int i = 1; i <= numPeriods; i++)
            {
                if (i == 1)
                {
                    OpenBal = PrincipleAmount;
                }
                else
                {
                    OpenBal = CloseBal;
                }
                IntPerPeriod = (OpenBal * (InterestRate / 100 / CompoundFreq));
                CumInterest = CumInterest + IntPerPeriod;
                CloseBal = OpenBal + IntPerPeriod;


                arr[0] = OpenBal.ToString("n2");
                arr[1] = IntPerPeriod.ToString("n2");
                arr[2] = CloseBal.ToString("n2");
                arr[3] = CumInterest.ToString("n2");
                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);

            }
            lblResults.Text = "Investment Value after " + txtInvTerm.Text + " Years";
            txtFutureValue.Text = Balance.ToString("n2");
            txtTotalInterest.Text = CumInterest.ToString("n2");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Clear();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Opening Bal", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Interest", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Closing Bal", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Cum Int", 70, HorizontalAlignment.Center);

        }

        private void cmbCompoundFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompoundFreq = Convert.ToInt32(cmbCompoundFreq.SelectedValue); // Everytime the combobox selection changes, the form resets and clears all previous values and then readds the columns.
            txtFutureValue.Text = "";
            txtTotalInterest.Text = "";
            listView1.Clear();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Opening Bal", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Interest", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Closing Bal", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Cum Int", 70, HorizontalAlignment.Center);
        }

        private void btnReset_Click(object sender, EventArgs e) // Clear all fields and reset calculations.
        {
            txtPrinciple.Text = "";
            txtInterestRate.Text = "";
            txtInvTerm.Text = "";
            cmbCompoundFreq.SelectedIndex = 0;
            txtFutureValue.Text = "";
            txtTotalInterest.Text = "";
            listView1.Clear(); listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Opening Bal", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Interest", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Closing Bal", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Cum Int", 70, HorizontalAlignment.Center);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
