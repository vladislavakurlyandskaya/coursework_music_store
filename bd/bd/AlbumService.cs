using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace bd
{
    internal class AlbumService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["bd.Properties.Settings.Music_storeConnectionString"].ConnectionString;
        SqlDataAdapter adapter;
        SqlCommand cmd;

        public void DisplayAlbum(DataGridView dataGridView1) //показать все альбомы
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select * from album", connection);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        public void DisplayProduct(DataGridView dataGridView2) //показать все товары
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select reception_Id, Оптовая_цена, Количество_склад, Дата_поступления, Номер_партии, album_Id, singer_Id from reception", connection);
                adapter.Fill(dt);
                dataGridView2.DataSource = dt;
                connection.Close();
            }
        }

        public void DisplaySinger(DataGridView dataGridView4) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select * from singer", connection);
                adapter.Fill(dt);
                dataGridView4.DataSource = dt;
                connection.Close();
            }
        }

        public void DisplaySale(DataGridView dataGridView3)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select sale_Id, Розничная_цена, Дата_продажи, album_Id from sale", connection);
                adapter.Fill(dt);
                dataGridView3.DataSource = dt;
                connection.Close();
            }
        }


        public void AddAlbum(string nameAlbum, string numberSongs, string yearOfRelease, string genre, string media, string singer, DataGridView dataGridView1)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("insert into Album (Название_альбома, Количество_песен_в_альбоме, Год_выпуска, genre_Id, media_Id, singer_Id) values (@название_альбома, @количество_песен_в_альбоме, @год_выпуска, @жанр, @носитель, @исполнитель)", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@название_альбома", nameAlbum);
                cmd.Parameters.AddWithValue("@количество_песен_в_альбоме", numberSongs);
                cmd.Parameters.AddWithValue("@год_выпуска", yearOfRelease);
                cmd.Parameters.AddWithValue("@жанр", genre);
                cmd.Parameters.AddWithValue("@носитель", media);
                cmd.Parameters.AddWithValue("@исполнитель", singer);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Альбом добавлен");
                DisplayAlbum(dataGridView1);               
            }
        }

        public void ChangeAlbum(string nameAlbum, string numberSongs, string yearOfRelease, string id, string media, string singer, string genre, DataGridView dataGridView1)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("update album set Название_альбома=@название_альбома, Количество_песен_в_альбоме=@количество_песен_в_альбоме, Год_выпуска=@год_выпуска, media_Id=@носитель, singer_Id=@исполнитель, genre_Id=@жанр where @id= album_Id", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@название_альбома", nameAlbum);
                cmd.Parameters.AddWithValue("@количество_песен_в_альбоме", numberSongs);
                cmd.Parameters.AddWithValue("@год_выпуска", yearOfRelease);
                cmd.Parameters.AddWithValue("@носитель", media);
                cmd.Parameters.AddWithValue("@исполнитель", singer);
                cmd.Parameters.AddWithValue("@жанр", genre);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Альбом обновлен");
                DisplayAlbum(dataGridView1);
            }
        }
                
        public void DeleteAlbum(string id, DataGridView dataGridView1)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (id != "")
                {
                    cmd = new SqlCommand("delete album where album_Id=@id", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Альбом удален");
                    DisplayAlbum(dataGridView1);
                }
                else
                {
                    MessageBox.Show("Не выбран альбом");
                }
            }
        }


        public void AddProduct(string wholesalePrice, string quantityProduct, string dateOfReceipt, string batchNumber, string album, string singer, DataGridView dataGridView2)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("insert into reception (Оптовая_цена, Количество_склад, Дата_поступления, Номер_партии, album_Id, singer_Id) values (@оптовая_цена, @количество_склад, @дата_поступления, @номер_партии, @альбом, @исполнитель)", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@оптовая_цена", wholesalePrice);
                cmd.Parameters.AddWithValue("@количество_склад", quantityProduct);
                cmd.Parameters.AddWithValue("@дата_поступления", dateOfReceipt);
                cmd.Parameters.AddWithValue("@номер_партии", batchNumber);
                cmd.Parameters.AddWithValue("@альбом", album);
                cmd.Parameters.AddWithValue("@исполнитель", singer);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Товар добавлен");
                DisplayProduct(dataGridView2);
            }
        }

        public void ChangeProduct(string wholesalePrice, string quantityProduct, string dateOfReceipt, string batchNumber, string id, string album, string singer, DataGridView dataGridView2)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("update reception set Оптовая_цена=@оптовая_цена, Количество_склад=@количество_склад, Дата_поступления=@дата_поступления, Номер_партии=@номер_партии, album_Id=@альбом, singer_Id=@исполнитель where @id= reception_Id", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@оптовая_цена", wholesalePrice);
                cmd.Parameters.AddWithValue("@количество_склад", quantityProduct);
                cmd.Parameters.AddWithValue("@дата_поступления", dateOfReceipt);
                cmd.Parameters.AddWithValue("@номер_партии", batchNumber);
                cmd.Parameters.AddWithValue("@альбом", album);
                cmd.Parameters.AddWithValue("@исполнитель", singer);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Данные о товаре изменены");
                DisplayProduct(dataGridView2);

            }
        }

        public void DeleteProduct(string id, DataGridView dataGridView2)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (id != "")
                {
                    cmd = new SqlCommand("delete reception where reception_Id=@id", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Товар удален");
                    DisplayAlbum(dataGridView2);
                }
                else
                {
                    MessageBox.Show("Не выбран товар");
                }
            }
        }


        public void AddSinger(string nameSinger, string country, DataGridView dataGridView4)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("insert into singer (Исполнитель, Страна) values (@исполнитель, @страна)", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@исполнитель", nameSinger);
                cmd.Parameters.AddWithValue("@страна", country);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Исполнитель добавлен");
                DisplaySinger(dataGridView4);
            }
        }

        public void ChangeSinger(string nameSinger, string country, string id, DataGridView dataGridView4)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("update singer set Исполнитель=@исполнитель, Страна=@страна where @id= singer_Id", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@исполнитель", nameSinger);
                cmd.Parameters.AddWithValue("@страна", country);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Исполнитель изменен");
                DisplaySinger(dataGridView4);
            }
        }

        public void DeleteSinger(string id, DataGridView dataGridView4)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (id != "")
                {
                    cmd = new SqlCommand("delete singer where singer_Id=@id", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Исполнитель удален");
                    DisplaySinger(dataGridView4);
                }
                else
                {
                    MessageBox.Show("Не выбран исполнитель");
                }
            }
        }


        public void AddSale(string retailPrice, string dateOfSale, string album, DataGridView dataGridView3)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("insert into sale (Розничная_цена, Дата_продажи, album_Id) values (@цена, @дата_продажи, @альбом)", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@цена", retailPrice);
                cmd.Parameters.AddWithValue("@дата_продажи", dateOfSale);
                cmd.Parameters.AddWithValue("@альбом", album);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Товар добавлен");
                DisplaySale(dataGridView3);
            }
        }

        public void ChangeSale(string retailPrice, string dateOfSale, string album, string id, DataGridView dataGridView3)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("update sale set Розничная_цена=@цена, Дата_продажи=@дата_продажи, album_Id=@альбом where @id = sale_Id", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@цена", retailPrice);
                cmd.Parameters.AddWithValue("@дата_продажи", dateOfSale);
                cmd.Parameters.AddWithValue("@альбом", album);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Товар изменен");
                DisplaySale(dataGridView3);
            }
        }

        public void DeleteSale(string id, DataGridView dataGridView3)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (id != "")
                {
                    cmd = new SqlCommand("delete sale where sale_Id=@id", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Товар удален");
                    DisplaySale(dataGridView3);
                }
                else
                {
                    MessageBox.Show("Не выбран товар");
                }
            }
        }


        public void DisplayIncomeStatement(DataGridView dataGrid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select Sale.Дата_продажи as Дата_продажи, COUNT(Sale.album_Id) as Количество_альбомов, " +
                    "SUM(Sale.Розничная_цена) as Доход " +
                    "from Sale group by Sale.Дата_продажи ORDER BY Доход  ASC ", connection);
                adapter.Fill(dt);

                dataGrid.DataSource = dt;
                connection.Close();
            }
        }

        public void DisplayIncomeStatementForPeriod(DataGridView dataGrid, string a, string b)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select Sale.Дата_продажи as Дата_продажи, COUNT(Sale.album_Id) as Количество_альбомов, " +
                    "SUM(Sale.Розничная_цена) as Доход " +
                    "from Sale group by Sale.Дата_продажи " +
                    "HAVING Sale.Дата_продажи > @a AND Sale.Дата_продажи < @b " +
                    "ORDER BY Доход  ASC ", connection);
                adapter.SelectCommand.Parameters.AddWithValue("@a", a);
                adapter.SelectCommand.Parameters.AddWithValue("@b", b);

                adapter.Fill(dt);
                dataGrid.DataSource = dt;
                connection.Close();
            }
        }

        public void DisplaySoldAlbums(DataGridView dataGrid, string a, string b)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select a.Название_альбома, a.Количество_песен_в_альбоме, a.Год_выпуска, s.Дата_продажи " +
                    "from Sale s " +
                    "join Album a ON a.album_Id = s.album_Id " +
                    "group by a.Название_альбома, a.Количество_песен_в_альбоме, a.Год_выпуска, s.Дата_продажи " +
                    "HAVING s.Дата_продажи > @a AND s.Дата_продажи < @b", connection);

                adapter.SelectCommand.Parameters.AddWithValue("@a", a);
                adapter.SelectCommand.Parameters.AddWithValue("@b", b);

                adapter.Fill(dt);
                dataGrid.DataSource = dt;
                connection.Close();
            }
        }

        public void DisplayUnsoldAlbums(DataGridView dataGrid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("SELECT Название_альбома, Количество_песен_в_альбоме, Год_выпуска FROM Album WHERE not exists (SELECT Розничная_цена, Дата_продажи FROM Sale WHERE (Sale.album_Id = Album.album_Id))", connection);

                adapter.Fill(dt);
                dataGrid.DataSource = dt;
                connection.Close();
            }
        }

        public void ExportWord(DataGridView DGV)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //добавим поля и колонки
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    }
                }

                Microsoft.Office.Interop.Word.Document oDoc = new Microsoft.Office.Interop.Word.Document();
                oDoc.Application.Visible = true;

                //страницы
                oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;

                dynamic oRange = oDoc.Content.Application.Selection.Range; string oTemp = ""; for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }
                //формат таблиц
                oRange.Text = oTemp;

                object Separator = Microsoft.Office.Interop.Word.WdTableFieldSeparator.wdSeparateByTabs; object ApplyBorders = true; object AutoFit = true; object AutoFitBehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount, Type.Missing, Type.Missing, ref ApplyBorders, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select(); oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0; oDoc.Application.Selection.Tables[1].Rows.Alignment = 0; oDoc.Application.Selection.Tables[1].Rows[1].Select(); oDoc.Application.Selection.InsertRowsAbove(1); oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //Стили заголовков
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;


                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //Текст заголовка
                foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Доход магазина";
                    headerRange.Font.Size = 25;
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }
            }
        }
    }
}
