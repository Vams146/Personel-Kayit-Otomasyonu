using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace personel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void listele()
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=personel2;Uid=root;pwd=root");
            MySqlCommand komut = new MySqlCommand("Select*from personel", baglanti);
            DataSet ds = new DataSet();
            MySqlDataAdapter kopru = new MySqlDataAdapter(komut);
            kopru.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void arama()
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=personel2;Uid=root;pwd=root");
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            MySqlCommand ara = new MySqlCommand("Select*from personel where  like '" + textBox1.Text + "%'", baglanti);
            DataSet ds = new DataSet();
            MySqlDataAdapter kopru = new MySqlDataAdapter(ara);
            kopru.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }


        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=personel2;Uid=root;pwd=root");
            baglanti.Open();
            MySqlCommand ekle = new MySqlCommand("insert into personel(kartno,sicilno,perssoyad,departman,gorevtarihi,maas)values(@kno,@sno,@sad,@dep,@gt,@ms)", baglanti);
            ekle.Parameters.AddWithValue("@kno", txtKartNo.Text);
            ekle.Parameters.AddWithValue("@sno", txtSicil.Text);
            ekle.Parameters.AddWithValue("@sad", txtSoyad.Text);
            ekle.Parameters.AddWithValue("@dep", departman.Text);
            ekle.Parameters.AddWithValue("@gt", txtTarih.Text);
            ekle.Parameters.AddWithValue("@ms", textBox1.Text);
            ekle.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarıyla Eklendi");
            listele();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            arama();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=personel2;Uid=root;pwd=root");
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select departmanadi from departman", baglanti);
            MySqlDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                comboBox1.Items.Add(okuyucu["departmanadi"].ToString());
            }
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=personel2;Uid=root;pwd=root");
            baglanti.Open();
            MySqlCommand sil = new MySqlCommand("Delete from personel where kartno=@id", baglanti);
            sil.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["kartno"].Value.ToString());
            sil.ExecuteNonQuery();
            MessageBox.Show("Kaydınız Başarıyla Silindi");
            listele();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=personel2;Uid=root;pwd=root");
            baglanti.Open();
            MySqlCommand guncelle = new MySqlCommand("update personel set kartno=@kno,sicilno=@sno,perssoyad=@sad,departman=@dep,gorevtarihi=@gt,maas=@ms", baglanti);
            guncelle.Parameters.AddWithValue("@kno", txtKartNo.Text);
            guncelle.Parameters.AddWithValue("@sno", txtSicil.Text);
            guncelle.Parameters.AddWithValue("@sad", txtSoyad.Text);
            guncelle.Parameters.AddWithValue("@dep", comboBox1.Text);
            guncelle.Parameters.AddWithValue("@gt", txtTarih.Text);
            guncelle.Parameters.AddWithValue("@ms", textBox1.Text);
            guncelle.ExecuteNonQuery();
            MessageBox.Show("Kaydınız Güncellendi");
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=personel2;Uid=root;pwd=root");
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            int yuzde = Convert.ToInt32(txtZam.Text);
            MySqlCommand guncelle = new MySqlCommand("update personel set maas=@ms where idpersonel=@id", baglanti);
            guncelle.Parameters.AddWithValue("@ms", Convert.ToInt32(txtAylik.Text) * Convert.ToInt32(txtZam.Text) / 100);
            guncelle.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["idpersonel"].Value.ToString());
            guncelle.ExecuteNonQuery();
            MessageBox.Show("Maaşınıza Zam Yapılmıştır.");
            listele();
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtAd.Clear();
            txtKartNo.Clear();
            txtSicil.Clear();
            txtSoyad.Clear();
            txtTarih.Clear();
            departman.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtAra.Clear();
            comboBox1.SelectedIndex = 0;
        }
    } }
