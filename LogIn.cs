using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace laboranorna_1_bd
{
    public partial class LogIn : Form
    {
        public string[,] matrix;
        DataTable dt;
        public LogIn()
        {
            InitializeComponent();
            h.ConStr = "server = 194.44.236.9; database = sqlkn24_3_mayu; " +
                "user = sqlkn24_3_mayu; password = kn24_mayu; charset = cp1251";
            dt = h.myfunDt("SELECT * FROM userName");

            int kilkz = dt.Rows.Count;
            matrix = new string[kilkz, 4];

            for(int i = 0; i < kilkz; i++)
            {
                matrix[i, 0] = dt.Rows[i].Field<int>("Id").ToString();
                matrix[i, 1] = dt.Rows[i].Field<string>("UserName");
                matrix[i, 2] = dt.Rows[i].Field<int>("Type").ToString();
                matrix[i, 3] = dt.Rows[i].Field<string>("Password");
                comboBox1.Items.Add(matrix[i, 1]);
            }
            comboBox1.Text = matrix[0, 1];
            textBox1.UseSystemPasswordChar = true;
            comboBox1.Focus();

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Authorization();
        }

        private void Authorization()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (String.Equals(comboBox1.Text.ToUpper(), matrix[i, 1].ToUpper()))
                {
                    if (String.Equals(h.EncriptedPassword_SHA256(textBox1.Text), matrix[i, 3]))
                    {
                        this.Hide();
                        MyBD f1 = new MyBD();
                        f1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Введіть правильний пароль!", "Помилка авторизації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

    }

    static class h
    {
        public static string ConStr { get; set; }
        public static int typeUser { get; set; }
        public static string nameUser { get; set; }
        public static BindingSource bs1 { get; set; }

        public static string EncriptedPassword_SHA256(string s)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(s));
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }

        public static DataTable myfunDt(string commandString)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(ConStr))
            using (MySqlCommand cmd = new MySqlCommand(commandString, con))
            {
                try
                {
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Неможливо з'єднатися з SQL-сервером! \nПеревірте наявність Інтернету...",
                                    ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }
    }
}
