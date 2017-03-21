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
    public partial class Ogrenciler : Form
    {
        public Ogrenciler()
        {
            InitializeComponent();
        }


        public void Dersal()
        {

        }

        AnaForm af; 

        private void Ogrenciler_Load(object sender, EventArgs e)
        {
            af = new AnaForm();
            af.bag = new OleDbConnection(af.bcum);
            af.adap = new OleDbDataAdapter();
            string sorgu = "SELECT * FROM Ogrenciler";
            OleDbDataAdapter data = new OleDbDataAdapter(sorgu, af.bag);
            af.dt = new DataTable();
            data.Fill(af.dt);  // Adaptör deki tabla bilgilerinden istenilen tablo datatable nesnesi üzerine aktarılır

            dataGridView1.DataSource = af.dt;


        }

    }


}
