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
    public partial class MyBD : Form
    {
        public MyBD()
        {
            InitializeComponent();
        }

        private void калькуляторToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
           About f1 = new About();
            f1.ShowDialog();
        }

        private void калькуляторToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Calculator f2 = new Calculator();
            f2.ShowDialog();
        }

        private void MyBD_Load(object sender, EventArgs e)
        {

        }

        private void myBD_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void OpenTableOsoba_Click(object sender, EventArgs e)
        {
            frmOsoba f1 = new frmOsoba();
            f1.ShowDialog();
        }

        private void OpenTableKatalog_Click(object sender, EventArgs e)
        {
            frmKatalog f1 = new frmKatalog();
            f1.ShowDialog();
        }

        private void OpenTableFormuliar_Click(object sender, EventArgs e)
        {
            frmFormuliar f1 = new frmFormuliar();
            f1.ShowDialog();
        }
    }
}
