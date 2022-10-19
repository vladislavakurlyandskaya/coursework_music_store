using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bd
{
    public partial class Проданные_альбомы : Form
    {
        private AlbumService albumService = new AlbumService();
        public Проданные_альбомы()
        {
            InitializeComponent();
            label2.Visible = false;
            label3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date > dateTimePicker1.Value.Date)
            {
                string a = dateTimePicker1.Value.ToShortDateString();
                label2.Text = a;
                string b = dateTimePicker2.Value.ToShortDateString();
                label3.Text = b;
                albumService.DisplaySoldAlbums(dataGridView1, label2.Text, label3.Text);
            }
            else
            {
                MessageBox.Show("Ошибка в выборе интервала дат");
            }
        }

        private void button1_Click(object sender, EventArgs e) => albumService.ExportWord(dataGridView1);

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Музыкальный_магазин f = new Музыкальный_магазин();
            f.Show();
        }
    }
}
