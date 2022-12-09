using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoSederhana
{
    public partial class TambahDataBarang : Form
    {
        public TambahDataBarang()
        {
            InitializeComponent();
            initColumnTable();
            textBoxNamaBarang.CharacterCasing = CharacterCasing.Upper;
            textBoxKodeBarang.CharacterCasing = CharacterCasing.Upper;
        }

        // Add Row selected
        int rowSelectedForEdit;
        DataTable sourceDataGrid = new DataTable();

        // Add header for data table
        private void initColumnTable()
        {
            this.sourceDataGrid.Clear();
            this.sourceDataGrid.Columns.Add("NO", typeof(int));
            this.sourceDataGrid.Columns.Add("KODE", typeof(string));
            this.sourceDataGrid.Columns.Add("NAMA BARANG", typeof(string));
            this.sourceDataGrid.Columns.Add("HARGA BELI", typeof(double));
            this.sourceDataGrid.Columns.Add("HARGA JUAL", typeof(double));
            this.sourceDataGrid.Columns.Add("TANGGAL", typeof(DateTime));

            dataGridView1.DataSource = this.sourceDataGrid;
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 15;
            dataGridView1.Columns[2].FillWeight = 35;
            dataGridView1.Columns[3].FillWeight = 13;
            dataGridView1.Columns[4].FillWeight = 13;
            dataGridView1.Columns[5].FillWeight = 14;
            //dataGridView1.RowHeadersVisible = false;

            ForDebuging();
        }

        private void ForDebuging()
        {
            TambahDataKeGrid(1, "C540", "MASAKO", 2500, 3000);
            TambahDataKeGrid(2, "C541", "MASAKO SAPI BESAR", 2500, 3000);
            TambahDataKeGrid(3, "C542", "MASAKO SAPI KECIL", 2500, 3000);
            TambahDataKeGrid(4, "C543", "MASAKO AYAM BESAR", 2500, 3000);
            TambahDataKeGrid(5, "C544", "MASAKO AYAM KAMPUNG", 2500, 3000);
        }

        private void label2_Click(object sender, EventArgs e) { }

        private void buttonTambahDataBarang_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.sourceDataGrid;
            int idx;           
            string kode = textBoxKodeBarang.Text;
            string namaBarang = textBoxNamaBarang.Text;
            double hargaBarang, keuntungan;
            double hargaJual;
            bool resHB = double.TryParse(textBoxHargaBeli.Text, out hargaBarang);
            bool resLaba = double.TryParse(textBoxKeuntungan.Text, out keuntungan);

            if (kode == string.Empty | namaBarang == string.Empty)
            {
                MessageBox.Show("ISI DATA DENGAN BENAR", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!resHB)
            {
                MessageBox.Show("HARGA BELI HARUS BERUPA ANGKA", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (kode == string.Empty)
            {
                MessageBox.Show("KODE HARUS DIISI!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (namaBarang == string.Empty)
            {
                MessageBox.Show("NAMA BARANG HARUS DIISI!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!resLaba | keuntungan <= 0)
            {
                MessageBox.Show("KEUNTUNGAN HARUS BERUPA ANGKA DAN TIDAK BOLEH NEGATIF", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (buttonTambahDataBarang.Text == "TAMBAH DATA")
                {
                    idx = this.sourceDataGrid.Rows.Count;
                    hargaJual = (100 + keuntungan) / 100.0 * hargaBarang;
                    TambahDataKeGrid(idx, kode.ToUpper(), namaBarang.ToUpper(), hargaBarang, hargaJual);
                }
                else
                {
                    idx = this.rowSelectedForEdit;
                    hargaJual = (100 + keuntungan) / 100.0 * hargaBarang;
                    UpdateDataTable(idx, kode.ToUpper(), namaBarang.ToUpper(), hargaBarang, hargaJual);
                }            
            }
            
            
        }

        private void TambahDataKeGrid(int idx_, string kode_, string nama_barang_, double hargaBarang_, double hargaJual_)
        {
            /*Ini langsung tambah ke datagridview
            
            dataGridView1.Rows[idx_].Cells[0].Value = idx_ + 1;
            dataGridView1.Rows[idx_].Cells[1].Value = kode_.ToUpper();
            dataGridView1.Rows[idx_].Cells[2].Value = nama_barang_.ToUpper();
            dataGridView1.Rows[idx_].Cells[3].Value = hargaBarang_;
            dataGridView1.Rows[idx_].Cells[4].Value = hargaJual_;
            
             */

            DataRow drow = this.sourceDataGrid.NewRow();
            drow["NO"] = idx_ + 1;
            drow["KODE"] = kode_;
            drow["NAMA BARANG"] = nama_barang_;
            drow["HARGA BELI"] = hargaBarang_;
            drow["HARGA JUAL"] = hargaJual_;
            drow["TANGGAL"] = DateTime.UtcNow.ToShortDateString();
            this.sourceDataGrid.Rows.Add(drow);

            HapusIsianTambahData();
        }

        private void UpdateDataTable(int idx_, string kode_, string nama_barang_, double hargaBarang_, double hargaJual_)
        {
            this.sourceDataGrid.Rows[idx_]["KODE"] = kode_;
            this.sourceDataGrid.Rows[idx_]["NAMA BARANG"] = nama_barang_;
            this.sourceDataGrid.Rows[idx_]["HARGA BELI"] = hargaBarang_;
            this.sourceDataGrid.Rows[idx_]["HARGA JUAL"] = hargaJual_;
            this.sourceDataGrid.Rows[idx_]["TANGGAL"] = DateTime.UtcNow.ToShortDateString();

            HapusIsianTambahData();
        }

        private void HapusIsianTambahData()
        {
            textBoxHargaBeli.Text = string.Empty;
            textBoxKodeBarang.Text = string.Empty;
            textBoxNamaBarang.Text = string.Empty;
            textBoxKeuntungan.Text = "25";

            buttonTambahDataBarang.Text = "TAMBAH DATA";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //var dataIndexNo = dataGridView1.Rows[e.RowIndex].Index.ToString();
            //string cellValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            //MessageBox.Show("The row index = " + dataIndexNo.ToString() + " and the row data in second column is: "
            //    + cellValue.ToString());
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //var dataIndexNo = dataGridView1.Rows[e.RowIndex].Index.ToString();
            //string cellValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            string kodeBarang = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string namaBarang = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string hargaBeli = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            string hargaJual = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            double laba = (Convert.ToDouble(hargaJual) - Convert.ToDouble(hargaBeli)) / Convert.ToDouble(hargaBeli) * 100;

            textBoxNamaBarang.Text = namaBarang;
            textBoxKodeBarang.Text = kodeBarang;
            textBoxHargaBeli.Text = hargaBeli;
            textBoxKeuntungan.Text = Convert.ToString(laba);

            buttonTambahDataBarang.Text = "SIMPAN";
            this.rowSelectedForEdit = e.RowIndex;

            /*MessageBox.Show("The row index = " + dataIndexNo.ToString() + " and the row data in second column is: "
                + cellValue.ToString());*/
        }

        private void TambahDataBarang_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //this.dataGridView1.Rows[e.RowIndex].Selected = true;

                    //MessageBox.Show($"row-{e.RowIndex}, col-{e.ColumnIndex}", "kam", MessageBoxButtons.OK);
                    //this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                    this.rowSelectedForEdit = e.RowIndex;
                    this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                    contextMenuStrip1.Show(Cursor.Position);
                }            
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!this.dataGridView1.Rows[this.rowSelectedForEdit].IsNewRow)
            {
                //this.dataGridView1.Rows.RemoveAt(this.rowSelectedForEdit);
                this.sourceDataGrid.Rows.RemoveAt(this.rowSelectedForEdit);

            }
        }

        private DataRow[] SearchValue(string keyWord)
        {
            DataRow[] results = this.sourceDataGrid.Select($"NAMA BARANG Like '%{keyWord}%' or KODE Like '%{keyWord}%' or HARGA BELI Like '%{keyWord}%' or HARGA JUAL Like '%{keyWord}%'");
            return results;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DataTable tempDataView = SelectSearchData(textBoxSearch.Text, this.sourceDataGrid);
            dataGridView1.DataSource = tempDataView;
        }

        private DataTable SelectSearchData(string keyWord, DataTable dt)
        {
            DataView dataView = new DataView(dt);
            //dataView.RowFilter = $"'NAMA BARANG' Like '%{keyWord}%' or KODE Like '%{keyWord}%' or 'HARGA BELI' Like '%{keyWord}%' or 'HARGA JUAL' Like '%{keyWord}%'";
            dataView.RowFilter = "[NAMA BARANG] LIKE '%" + keyWord + "%'";
            return dataView.ToTable();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // High light and searching apply over selective fields of grid.  
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && this.sourceDataGrid.Columns[e.ColumnIndex].ColumnName != "NO")
            {
                // Check data for search  
                if (!String.IsNullOrWhiteSpace(textBoxSearch.Text.Trim()))
                {
                    String gridCellValue = e.FormattedValue.ToString();
                    // check the index of search text into grid cell.  
                    int startIndexInCellValue = gridCellValue.ToLower().IndexOf(textBoxSearch.Text.Trim().ToLower());
                    // IF search text is exists inside grid cell then startIndexInCellValue value will be greater then 0 or equal to 0  
                    if (startIndexInCellValue >= 0)
                    {
                        e.Handled = true;
                        e.PaintBackground(e.CellBounds, true);
                        //the highlite rectangle  
                        Rectangle hl_rect = new Rectangle();
                        hl_rect.Y = e.CellBounds.Y + 2;
                        hl_rect.Height = e.CellBounds.Height - 5;
                        //find the size of the text before the search word in grid cell data.  
                        String sBeforeSearchword = gridCellValue.Substring(0, startIndexInCellValue);
                        //size of the search word in the grid cell data  
                        String sSearchWord = gridCellValue.Substring(startIndexInCellValue, textBoxSearch.Text.Trim().Length);
                        Size s1 = TextRenderer.MeasureText(e.Graphics, sBeforeSearchword, e.CellStyle.Font, e.CellBounds.Size);
                        Size s2 = TextRenderer.MeasureText(e.Graphics, sSearchWord, e.CellStyle.Font, e.CellBounds.Size);
                        if (s1.Width > 5)
                        {
                            hl_rect.X = e.CellBounds.X + s1.Width - 5;
                            hl_rect.Width = s2.Width - 6;
                        }
                        else
                        {
                            hl_rect.X = e.CellBounds.X + 2;
                            hl_rect.Width = s2.Width - 6;
                        }
                        //color for showing highlighted text in grid cell  
                        SolidBrush hl_brush;
                        hl_brush = new SolidBrush(Color.Yellow);
                        //paint the background behind the search word  
                        e.Graphics.FillRectangle(hl_brush, hl_rect);
                        hl_brush.Dispose();
                        e.PaintContent(e.CellBounds);
                    }
                }
            }
        }
    }
}
