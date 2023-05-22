using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplik_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Ömer\\Desktop\\Kitaplik.mdb");
        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select *from kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }
        string durum="";
        private void btnkaydet_Click(object sender, EventArgs e)
        {
           
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("insert into kitaplar (kitapad,yazar,tur,sayfa,durum) values (@a1,@a2,@a3,@a4,@a5)",baglanti);
            komut1.Parameters.AddWithValue("@a1", txtkitapad.Text);
            komut1.Parameters.AddWithValue("@a2", txtyazar.Text);
            komut1.Parameters.AddWithValue("@a3", cmbtur.Text);
            komut1.Parameters.AddWithValue("@a4", txtsayfa.Text);
            komut1.Parameters.AddWithValue("@a5", durum);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Kitap bilgileri kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void rdokullanilmis_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void rdosifir_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtkitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtkitapad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbtur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            
            if(dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                rdosifir.Checked = true;
            }
           
             else 
            {
                rdokullanilmis.Checked = true;
            }
            
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            
            
            DialogResult result= MessageBox.Show("Kayıt silinecekter onaylıyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (result == DialogResult.Yes)
            {
                OleDbCommand komut = new OleDbCommand("delete  from Kitaplar where Kitapid=@a1", baglanti);
                komut.Parameters.AddWithValue("@a1", txtkitapid.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("update kitaplar set kitapad=@a1, yazar=@a2, tur=@a3, sayfa=@a4, durum=@a5 where kitapid=@a6", baglanti);
            komut.Parameters.AddWithValue("@a1", txtkitapad.Text);
            komut.Parameters.AddWithValue("@a2", txtyazar.Text);
            komut.Parameters.AddWithValue("@a3", cmbtur.Text);
            komut.Parameters.AddWithValue("@a4", txtsayfa.Text);
            if (rdokullanilmis.Checked == true)
            {
                komut.Parameters.AddWithValue("@a5", durum);
            }
            if (rdosifir.Checked == true)
            {
                komut.Parameters.AddWithValue("@a5", durum);
            }
            komut.Parameters.AddWithValue("@a6", txtkitapid.Text);
            komut.ExecuteNonQuery();
            listele();
            baglanti.Close();
            MessageBox.Show("Kayıt güncellendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

        private void btnara_Click(object sender, EventArgs e)
        {
            
            OleDbCommand komut = new OleDbCommand("select * from Kitaplar where kitapad=@a1 or yazar=@a2 ", baglanti);
            komut.Parameters.AddWithValue("@a1", textBox1.Text);
            komut.Parameters.AddWithValue("@a2", textBox1.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtkitapad.Text = "";
            txtkitapid.Text = "";
            txtsayfa.Text = "";
            txtyazar.Text = "";
            cmbtur.SelectedIndex = -1;
            rdokullanilmis.Checked = false;
            rdosifir.Checked = false;
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("select * from Kitaplar where kitapad like '%"+textBox1.Text+"%' ", baglanti);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
