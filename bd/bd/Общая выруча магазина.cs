using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bd
{
    public partial class Общая_выруча_магазина : Form
    {
        private AlbumService albumService = new AlbumService();
        public Общая_выруча_магазина()
        {
            InitializeComponent();
            albumService.DisplayIncomeStatement(dataGridView1);
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
