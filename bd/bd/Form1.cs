using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace bd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string con = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From UserAuthorization Where Логин= '" + textBox1.Text + "'And Пароль='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                Музыкальный_магазин ss = new Музыкальный_магазин();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
