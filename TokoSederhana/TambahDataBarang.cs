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
        }

        // Add Row selected
        int rowSelectedForEdit;
        //DataTable sourceDataGrid = new DataTable();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonTambahDataBarang_Click(object sender, EventArgs e)
        {
            int idx;           
            string kode = textBoxKodeBarang.Text;
            string namaBarang = textBoxNamaBarang.Text;
            int hargaBarang, keuntungan;
            double hargaJual;
            bool resHB = int.TryParse(textBoxHargaBeli.Text, out hargaBarang);
            bool resLaba = int.TryParse(textBoxKeuntungan.Text, out keuntungan);

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
                    idx = dataGridView1.Rows.Add();
                }
                else
                {
                    idx = this.rowSelectedForEdit;
                }

                hargaJual = (100 + keuntungan) / 100.0 * hargaBarang;
                TambahDataKeGrid(idx, kode, namaBarang, hargaBarang, hargaJual);
            }
            
            
        }

        private void TambahDataKeGrid(int idx_, string kode_, string nama_barang_, int hargaBarang_, double hargaJual_)
        {
            dataGridView1.Rows[idx_].Cells[0].Value = idx_ + 1;
            dataGridView1.Rows[idx_].Cells[1].Value = kode_.ToUpper();
            dataGridView1.Rows[idx_].Cells[2].Value = nama_barang_.ToUpper();
            dataGridView1.Rows[idx_].Cells[3].Value = hargaBarang_;
            dataGridView1.Rows[idx_].Cells[4].Value = hargaJual_;

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
                this.dataGridView1.Rows.RemoveAt(this.rowSelectedForEdit);
            }
        }
    }
}
