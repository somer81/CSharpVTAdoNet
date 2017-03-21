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

namespace VeriTabani
{
    public partial class Dersler : Form
    {
        public Dersler()
        {
            InitializeComponent();
        }

        AnaForm af = new AnaForm();

        private void Dersler_Load(object sender, EventArgs e)
        {

            listele();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void listele()
        {
            listView1.Items.Clear();  

            af.bag = new OleDbConnection(af.bcum);
            af.komut = new OleDbCommand();
            af.komut.CommandText = "SELECT * FROM Dersler";
            af.komut.Connection = af.bag;
            af.bag.Open();



            af.dr = af.komut.ExecuteReader();  // Veri Okunurken SELECT

            while (af.dr.Read())  // Her Seferinde bir satır oku
            {
                ListViewItem list = new ListViewItem(af.dr["DersID"].ToString());  // Bir liste satırının ilk değeri oluştu
                //list.Tag = af.dr["ID"].ToString();
                list.SubItems.Add(af.dr["DersAdi"].ToString());  // Devam eden sütun b,lgileri sırasıyla eklendi
                list.SubItems.Add(af.dr["DersKodu"].ToString());
                list.SubItems.Add(af.dr["AKTS"].ToString());
                list.SubItems.Add(af.dr["Kredi"].ToString());  // list elemanı tamamlandı
                listView1.Items.Add(list);  // Listview üzerine satır olarak ekle

            }
            af.dr.Close();
            af.bag.Close();

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            textBox1.Focus();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            af.bag = new OleDbConnection(af.bcum);  // Bağlantı Açılır
            af.komut = new OleDbCommand();  /// Komut nesnesi oluştur

            af.komut.CommandText = "INSERT INTO Dersler(DersAdi,DersKodu,AKTS,Kredi) VALUES(@da,@dk,@akts,@krd)";

            // Parametre olarak ekleyerek SQL injectionın önüne geçilir

            af.komut.Parameters.AddWithValue("da", textBox1.Text.ToString());
            af.komut.Parameters.AddWithValue("dk", textBox2.Text.ToString());
            af.komut.Parameters.AddWithValue("akts", textBox3.Text.ToString());
            af.komut.Parameters.AddWithValue("krd", textBox4.Text.ToString());


            af.komut.Connection = af.bag;  // Komutu hangi bağlantı üzeirnden çalışacak
            try  // Hata Yakala
            {
                af.bag.Open(); // Bağlantı aç
                af.komut.ExecuteNonQuery();  // Komutu çalıştır
                MessageBox.Show("Kayıt Eklendi!!"); // Başarılı ise mesaj ver

                
               
                af.bag.Close();  // baglantiyi kapat
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt Başarısız" + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            af.bag = new OleDbConnection(af.bcum);
            af.komut = new OleDbCommand();


             string sil = listView1.SelectedItems[0].Text;
             MessageBox.Show(sil);


            af.komut.CommandText = "DELETE FROM Dersler WHERE DersID = @sil";


            af.komut.Parameters.AddWithValue("sil", sil);
           


            af.komut.Connection = af.bag;
            try
            {
                af.bag.Open();
                af.komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi!!");

                af.dr.Close();
                af.bag.Close();
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt Başarısız" + ex.Message);
            }

        }

        ListViewItem item;

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                item = listView1.SelectedItems[0];
                textBox1.Text = item.SubItems[1].Text;
                textBox2.Text = item.SubItems[2].Text;
                textBox3.Text = item.SubItems[3].Text;
                textBox4.Text = item.SubItems[4].Text;
                textBox5.Text = item.SubItems[0].Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            af.bag = new OleDbConnection(af.bcum);
            af.komut = new OleDbCommand();


           // string gunc = listView1.SelectedItems[0].Text;
           // string a = item.SubItems[0].Text;
           // MessageBox.Show(a);
             af.komut.CommandText = "UPDATE Dersler set DersAdi = @da, DersKodu = @dk, AKTS = @akts , Kredi = @krd WHERE DersID = @id";

            // af.komut.Parameters.AddWithValue("id", gunc.ToString());
            af.komut.Parameters.AddWithValue("da", textBox1.Text.ToString());
            af.komut.Parameters.AddWithValue("dk", textBox2.Text.ToString());
            af.komut.Parameters.AddWithValue("akts", textBox3.Text.ToString());
            af.komut.Parameters.AddWithValue("krd", textBox4.Text.ToString());
            af.komut.Parameters.AddWithValue("id", textBox5.Text.ToString());




            af.komut.Connection = af.bag;
            try
            {
                af.bag.Open(); // Bağlantıyı Aç
                af.komut.ExecuteNonQuery();  // Komutu Çalıştır
                MessageBox.Show("Kayıt Güncellendi!!");

                af.dr.Close();
                af.bag.Close();  // Bağlantıyı Kapat
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt Başarısız" + ex.Message);
            }
        }
    }
}
