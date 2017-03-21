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
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dersler d = new Dersler();
            d.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ogrenciler o = new Ogrenciler();
            o.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hocalar h = new Hocalar();
            h.Show();
        }

       public OleDbConnection bag;
       public OleDbCommand komut;
       public OleDbDataReader dr;
       public OleDbDataAdapter adap;
       public DataTable dt;
       public DataSet ds;
       public string bcum = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=okul.accdb";


        private void Form1_Load(object sender, EventArgs e)
        {

            bag = new OleDbConnection(bcum);

            try { 
                bag.Open();
                MessageBox.Show("VeriTabanına Bağlanıldı");
                // bag.Close();
            
            }
            catch(Exception ex)
            {
                MessageBox.Show("Bağlantı HATASI !!" + ex.Message);
            }


        }
    }
}
