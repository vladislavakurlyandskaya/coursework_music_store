using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace bd
{
    public partial class Музыкальный_магазин : Form
    {
        private AlbumService albumService = new AlbumService();
        public Музыкальный_магазин()
        {
            InitializeComponent();
            albumService.DisplayAlbum(dataGridView1);
            albumService.DisplayProduct(dataGridView2);
            albumService.DisplaySinger(dataGridView4);
            albumService.DisplaySale(dataGridView3);
            fillcombo();
            label22.Visible = false;
            label23.Visible = false;
            label21.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label27.Visible = false;
            label28.Visible = false;
            label29.Visible = false;
        }
      
        private void button1_Click(object sender, EventArgs e) 
        {
            this.Close();
            Form1 ss = new Form1();
            ss.Show();
        }

        public void fillcombo()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "select * from Album";
            string sql2 = "select * from Media";
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
                    comboBox5.Items.Add(Convert.ToString(mreader["Название_альбома"]));
                }
                mreader.Close();

                mreader2 = cmd2.ExecuteReader();
                while (mreader2.Read())
                {
                    comboBox2.Items.Add(Convert.ToString(mreader2["Вид_носителя"]));
                }
                mreader2.Close();

                mreader3 = cmd3.ExecuteReader();
                while (mreader3.Read())
                {
                    comboBox3.Items.Add(Convert.ToString(mreader3["Исполнитель"]));
                    comboBox6.Items.Add(Convert.ToString(mreader3["Исполнитель"]));
                }
                mreader3.Close();

                mreader4 = cmd4.ExecuteReader();
                while (mreader4.Read())
                {
                    comboBox4.Items.Add(Convert.ToString(mreader4["Вид_жанра"]));
                }
                mreader4.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CleanData()
        {
            nameAlbum.Text = "";
            numberSongs.Text = "";
            yearOfRelease.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            nameSinger.Text = "";
            country.Text = "";
            wholesalePrice.Text = "";
            quantityProduct.Text = "";
            batchNumber.Text = "";
            comboBox1.Text = "";
            retailPrice.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = comboBox2.Text;
            label21.Text = c;
            string con = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("Select media_Id From Media Where Вид_носителя LIKE '%" + comboBox2.Text + "%' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label21.Text = dt.Rows[0][0].ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = comboBox3.Text;
            label24.Text = c;
            string con = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("Select singer_Id From Singer Where Исполнитель LIKE '%" + comboBox3.Text + "%' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label24.Text = dt.Rows[0][0].ToString();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = comboBox4.Text;
            label25.Text = c;
            string con = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("Select genre_Id From Genre Where Вид_жанра LIKE '%" + comboBox4.Text + "%' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label25.Text = dt.Rows[0][0].ToString();
        }     

        private void addAlbumButton_Click(object sender, EventArgs e)
        {
            albumService.AddAlbum(nameAlbum.Text, numberSongs.Text, yearOfRelease.Text, label25.Text, label21.Text, label24.Text, dataGridView1);
            CleanData();
        }

        private void changeAlbumButton_Click(object sender, EventArgs e)
        {
            albumService.ChangeAlbum(nameAlbum.Text, numberSongs.Text, yearOfRelease.Text, label1.Text, label21.Text, label24.Text, label25.Text, dataGridView1);
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = comboBox1.Text;
            label27.Text = c;
            string con = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("Select album_Id From Album Where Название_альбома LIKE '%" + comboBox1.Text + "%' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label27.Text = dt.Rows[0][0].ToString();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = comboBox6.Text;
            label28.Text = c;
            string con = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("Select singer_Id From Singer Where Исполнитель LIKE '%" + comboBox6.Text + "%' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label28.Text = dt.Rows[0][0].ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string a = dateTimePicker1.Value.ToShortDateString();
            label22.Text = a;
        }

        private void addProductButton_Click(object sender, EventArgs e)
        {
            albumService.AddProduct(wholesalePrice.Text, quantityProduct.Text, label22.Text, batchNumber.Text, label27.Text, label28.Text, dataGridView2);
            CleanData();
        }    

        private void changeProductButton_Click(object sender, EventArgs e)
        {
            albumService.ChangeProduct(wholesalePrice.Text, quantityProduct.Text, label22.Text, batchNumber.Text, label8.Text, label27.Text, label28.Text, dataGridView2);
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
            dateTimePicker1.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            batchNumber.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            comboBox1.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox6.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
        }


        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = comboBox5.Text;
            label29.Text = c;
            string con = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(con);
            SqlDataAdapter sda = new SqlDataAdapter("Select album_Id From Album Where Название_альбома LIKE '%" + comboBox5.Text + "%' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label29.Text = dt.Rows[0][0].ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string b = dateTimePicker2.Value.ToShortDateString();
            label23.Text = b;
        }

        private void addSale_Click(object sender, EventArgs e)
        {
            albumService.AddSale(retailPrice.Text, label23.Text, label29.Text, dataGridView3);
            CleanData();
        }

        private void changeSale_Click(object sender, EventArgs e)
        {
            albumService.ChangeSale(retailPrice.Text, label23.Text, label29.Text, label12.Text, dataGridView3);
            CleanData();
        }

        private void deleteSale_Click(object sender, EventArgs e)
        {
            albumService.DeleteSale(label12.Text, dataGridView3);
            CleanData();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label12.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
            retailPrice.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            dateTimePicker2.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox5.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Общая_выруча_магазина f = new Общая_выруча_магазина();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Доход_магазина f = new Доход_магазина();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Проданные_альбомы  f = new Проданные_альбомы();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Непроданные_товары f = new Непроданные_товары();
            f.Show();
        }        
    }
}
