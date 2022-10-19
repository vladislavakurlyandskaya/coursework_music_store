using System;
using System.Windows.Forms;

namespace bd
{
    public partial class Непроданные_товары : Form
    {
        private AlbumService albumService = new AlbumService();
        public Непроданные_товары()
        {
            InitializeComponent();
            albumService.DisplayUnsoldAlbums(dataGridView1);
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
