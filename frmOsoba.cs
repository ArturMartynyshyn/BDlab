using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laboranorna_1_bd
{
    public partial class frmOsoba : Form
    {
        public frmOsoba()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmOsoba_Load(object sender, EventArgs e)
        {
            this.Height = 500;
            h.bs1 = new BindingSource();
            h.bs1.DataSource = h.myfunDt("Select * from Osoba");
            dataGridView1.DataSource = h.bs1;
            bindingNavigator1.BindingSource = h.bs1;
            dataGridView1.RowHeadersVisible = false;

            DataTable dtBorder = new DataTable();
            DataTable dtDistinct = new DataTable();
            dtBorder = h.myfunDt("SELECT min(rating), max(rating), min(DNO), max(DNO) FROM Osoba");
            dtDistinct = h.myfunDt("SELECT distinct Adress from Osoba");

            txtReitForm.Text = dtBorder.Rows[0][0].ToString();
            txtReitTo.Text = dtBorder.Rows[0][1].ToString();
            dtpDNOFrom.Value = Convert.ToDateTime(dtBorder.Rows[0][2].ToString());
            dtpDNOTo.Value = Convert.ToDateTime(dtBorder.Rows[0][3].ToString());

            cmbAdres.Items.Add("");
            for (int i = 0; i < dtDistinct.Rows.Count; i++)
                cmbAdres.Items.Add(dtDistinct.Rows[i][0].ToString());
            cmbAdres.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSex.Items.Add("");
            cmbSex.Items.Add("ч");
            cmbSex.Items.Add("ж");
            cmbSex.Text = "ч";
            cmbSex.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (btnFind.Checked)
            {
                label1.Visible = true;
                txtFind.Visible = true;
                txtFind.Focus();
            }
            else
            {
                CancelFind();
            }

        }

        private void CancelFind()
        {
            label1.Visible = false;
            txtFind.Visible = false;
            txtFind.Text = "";
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(txtFind.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void txtFind_Leave(object sender, EventArgs e)
        {
            CancelFind();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            Pen p = new Pen(Color.DarkViolet, 1);
            gfx.DrawLine(p, 0, 5, 5, 5);
            gfx.DrawLine(p, 35, 5, e.ClipRectangle.Width - 2, 5);
            gfx.DrawLine(p, 0, 5, 0, e.ClipRectangle.Height - 2);
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, 5, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2, 0, e.ClipRectangle.Height - 2);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (btnFilter.Checked)
            {
                this.Height = 600;
                groupBox1.Visible = true;
            }
            else
            {
                this.Height = 440;
                groupBox1.Visible = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnFilterOk_Click(object sender, EventArgs e)
        {
            try
            {
                var strFilter = new StringBuilder("id > 0"); // Default base condition

                // PIPO Filter
                if (!string.IsNullOrEmpty(txtPIP.Text))
                {
                    strFilter.Append($" AND PIPO LIKE '{txtPIP.Text.Replace("'", "''")}%'");
                }

                // Rating Filter
                if (decimal.TryParse(txtReitForm.Text, out var ratingFrom) &&
                    decimal.TryParse(txtReitTo.Text, out var ratingTo))
                {
                    strFilter.Append($" AND (rating >= {ratingFrom} AND rating <= {ratingTo})");
                }
                else if (decimal.TryParse(txtReitForm.Text, out ratingFrom))
                {
                    strFilter.Append($" AND rating >= {ratingFrom}");
                }
                else if (decimal.TryParse(txtReitTo.Text, out ratingTo))
                {
                    strFilter.Append($" AND rating <= {ratingTo}");
                }

                // Date Filter
                strFilter.Append($" AND DNO >= #{dtpDNOFrom.Value:yyyy-MM-dd HH:mm:ss}# AND DNO <= #{dtpDNOTo.Value:yyyy-MM-dd HH:mm:ss}#");

                // Address Filter
                if (!string.IsNullOrEmpty(cmbAdres.Text))
                {
                    strFilter.Append($" AND Adress = '{cmbAdres.Text.Replace("'", "''")}'");
                }

                // Gender Filter
                if (cmbSex.Text == "ч")
                {
                    strFilter.Append(" AND sex = 1"); // Male
                }
                else if (cmbSex.Text == "ж")
                {
                    strFilter.Append(" AND sex = 0"); // Female
                }

                // Debugging: Show the filter
                MessageBox.Show(strFilter.ToString(), "Generated Filter");

                // Apply the filter
                h.bs1.Filter = strFilter.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void btnFilterCancel_Click(object sender, EventArgs e)
        {
            h.bs1.RemoveFilter();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Table1_Insert f1add = new Table1_Insert();
            f1add.ShowDialog();
            h.bs1.DataSource = h.myfunDt("Select * from Osoba");
            dataGridView1.DataSource = h.bs1;

        }
    }

}
