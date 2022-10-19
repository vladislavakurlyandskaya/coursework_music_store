using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bd
{
    public partial class Музыкальный_магазин : Form
    {
        private AlbumService albumService = new AlbumService();
        private string connectionString = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.MusicConnectionString"].ConnectionString;

        public Музыкальный_магазин()
        {
            InitializeComponent();
            albumService.DisplayAlbum(dataGridView1);
            albumService.DisplayProduct(dataGridView2);
            albumService.DisplaySinger(dataGridView4);
            fillcombo();
           // combo();
        }
        private void combo()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter s = new SqlDataAdapter("SELECT * FROM Media", connectionString);
                DataTable table = new DataTable();
                s.Fill(table);
                comboBox2.DataSource = table.AsDataView();
                comboBox2.DisplayMember = "Носитель";
                comboBox2.ValueMember = "media_Id";
                int IDA = (int)comboBox2.SelectedValue;
            }
        }

        private void button1_Click(object sender, EventArgs e) //кнопка выйти
        {
            this.Close();
            Form1 form = new Form1();
            form.Show();
        }


        public void fillcombo() //комбобоксы
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.MusicConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select * from Album";
            string sql2 = "select media_Id, Вид_носителя from Media";
            string sql3 = "select * from Singer";
            string sql4 = "select * from Genre";

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlCommand cmd2 = new SqlCommand(sql2, connection);
            SqlCommand cmd3 = new SqlCommand(sql3, connection);
            SqlCommand cmd4 = new SqlCommand(sql4, connection);
            SqlDataReader mreader;
            SqlDataReader mreader2;
            SqlDataReader mreader3;
            SqlDataReader mreader4;
            try
            {
                connection.Open();
                mreader = cmd.ExecuteReader();
                while (mreader.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(mreader["Название_альбома"]));
                }
                mreader.Close();

                mreader2 = cmd2.ExecuteReader();
                while (mreader2.Read())
                {
                    comboBox2.Items.Add (Convert.ToString(mreader2["media_Id"]));
                    
                }
                mreader2.Close();

                mreader3 = cmd3.ExecuteReader();
                while (mreader3.Read())
                {
                    comboBox3.Items.Add(Convert.ToString(mreader3["singer_Id"]));
                }
                mreader3.Close();

                mreader4 = cmd4.ExecuteReader();
                while (mreader4.Read())
                {
                    comboBox4.Items.Add(Convert.ToString(mreader4["genre_Id"]));
                }
                mreader4.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CleanData() //функция стирания данных в текстбоксах
        {
            nameAlbum.Text = "";
            numberSongs.Text = "";
            yearOfRelease.Text = "";
            dateOfReceipt.Text = "";
            quantityProduct.Text = "";
            wholesalePrice.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            country.Text = "";
            nameSinger.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
        }

        private void addAlbumButton_Click(object sender, EventArgs e)
        {
            albumService.AddAlbum(nameAlbum.Text, numberSongs.Text, yearOfRelease.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, dataGridView1);
            CleanData();
        }
        private void changeAlbumButton_Click(object sender, EventArgs e)
        {
            albumService.ChangeAlbum(nameAlbum.Text, numberSongs.Text, yearOfRelease.Text, label1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, dataGridView1);
            CleanData();
        }
        private void deleteAlbumButton_Click(object sender, EventArgs e)
        {
            albumService.DeleteAlbum(label1.Text, dataGridView1);
            CleanData();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            nameAlbum.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            numberSongs.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            yearOfRelease.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }


        private void addProductButton_Click(object sender, EventArgs e)
        {
            albumService.AddProduct(wholesalePrice.Text, quantityProduct.Text, dateOfReceipt.Text, batchNumber.Text, dataGridView2);
            CleanData();
        }
        private void changeProductButton_Click(object sender, EventArgs e)
        {
            albumService.ChangeProduct(wholesalePrice.Text, quantityProduct.Text, dateOfReceipt.Text, batchNumber.Text, label8.Text, dataGridView2);
            CleanData();
        }
        private void deleteProductButton_Click(object sender, EventArgs e)
        {
            albumService.DeleteProduct(label8.Text, dataGridView2);
            CleanData();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label8.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            wholesalePrice.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            quantityProduct.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateOfReceipt.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            batchNumber.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void addSinger_Click(object sender, EventArgs e)
        {
            albumService.AddSinger(nameSinger.Text, country.Text, dataGridView4);
            CleanData();
        }

        private void changeSinger_Click(object sender, EventArgs e)
        {
            albumService.ChangeSinger(nameSinger.Text, country.Text, label15.Text, dataGridView4);
            CleanData();
        }

        private void deleteSinger_Click(object sender, EventArgs e)
        {
            albumService.DeleteSinger(label15.Text, dataGridView4);
            CleanData();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label15.Text = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
            nameSinger.Text = dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString();
            country.Text = dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
