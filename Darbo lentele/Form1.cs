using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Darbo_lentele
{
    public partial class Form1 : Form
    {
        private List<Gaminys> gam = new List<Gaminys>();
        static public string IDOFselected;
        static public string Data;
        static public string Suma;
        static public string selected;
        private string selected1 = null;
        static public string pavadinimas;
        static public string pappavadinimas;
        static public string nr;
        static public string vienetai;
        static public string ikainis;
        static public double b;
        //static public string comboboxst="";
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //belekas();
            comboBox3.SelectedText = "-";
            UpdateTable( "SELECT * FROM `Gaminiai`");
            prideti();
            prideti2();
            UpdateTable( "SELECT * FROM `Gaminiai` WHERE `Data` = '-' ");
            selected = "-";
            Data = "-";
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
  
        private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            comboBox2.Items.Clear();
            selected = comboBox1.SelectedItem.ToString();
            if(Data != "-")
            Data = comboBox3.SelectedItem.ToString();
            comboBox2.Visible = true;
            //hjh();
            UpdateTable("SELECT * FROM `Gaminiai` WHERE `Pavadinimas`='" + selected + "' AND `Data` = '"+Data+"';");
            prideti1();
           
        }
        
        void prideti()
        {
            comboBox1.Items.Clear();

            int h = dataGridView1.RowCount;
            if(!comboBox1.Items.Contains("-"))
            comboBox1.Items.Add("-");
            for (int i = 0; i < h; i++)
            {
                
                DataGridViewRow selectedRow = dataGridView1.Rows[i];
                string a = Convert.ToString(selectedRow.Cells[1].Value);
                if (!comboBox1.Items.Contains(a))
                    comboBox1.Items.Add(a);
            }
        }
        void prideti2()
        {
            int h = dataGridView1.RowCount;
            for (int i = 0; i < h; i++)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[i];
                string a = Convert.ToString(selectedRow.Cells["Data"].Value);
                if (!comboBox3.Items.Contains(a))
                    comboBox3.Items.Add(a);
            }

        }
        void prideti1()
        {
            if (!comboBox2.Items.Contains("-"))
                comboBox2.Items.Add("-");
            int h = dataGridView1.RowCount;
            for (int i = 0; i < h; i++)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[i];
                string a = Convert.ToString(selectedRow.Cells[2].Value);
                if (!comboBox2.Items.Contains(a))
                    comboBox2.Items.Add(a);
            }
            
        }
        void rodyti()
        {
             List<Gaminys> Tgam = new List<Gaminys>();
            dataGridView1.DataSource = null;
            if (comboBox1.SelectedIndex != -1)
            {
                    foreach (var a in gam)
                    {
                        if (a.Pavadinimas == selected)
                        {
                            Gaminys gaminys = new Gaminys(a.Pavadinimas, a.Papildomas_Pavadinimas, a.Detales_nr, a.Vienetai, a.Ikainis,a.Suma);
                            Tgam.Add(gaminys);
                            dataGridView1.DataSource = a;
                        }
                    }

            }
            dataGridView1.DataSource = Tgam;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected1 = comboBox2.SelectedItem.ToString();
            if(selected1!="-")
                UpdateTable("SELECT * FROM `Gaminiai` WHERE `Papildomas_Pavadinimas`='" + selected1 + "' AND `Pavadinimas`='" + selected + "' AND `Data`= '"+Data+"';");
            else
                UpdateTable("SELECT * FROM `Gaminiai` WHERE `Pavadinimas`='" + selected + "' AND `Data` = '"+Data+"';");
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            add.vnt = -1;
            add f1 = new add();
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
            pavadinimas = Convert.ToString(selectedRow.Cells[1].Value);
            pappavadinimas = Convert.ToString(selectedRow.Cells[2].Value);
            nr = Convert.ToString(selectedRow.Cells[3].Value);
            vienetai = Convert.ToString(selectedRow.Cells[4].Value);
            ikainis = Convert.ToString(selectedRow.Cells[5].Value);
            IDOFselected = Convert.ToString(selectedRow.Cells["ID"].Value);
            Data = Convert.ToString(selectedRow.Cells["Data"].Value);
            Suma = Convert.ToString(selectedRow.Cells["Suma"].Value);
            f1.ShowDialog();

            if(add.vnt >= 0)
            {
                UpdateTable("SELECT * FROM `Gaminiai` WHERE `Data`='" + comboBox3.SelectedItem.ToString() + "'");
            }
            

        }
        

        private  void Button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            DateTime data = new DateTime();
            data = DateTime.Now;
            string datastring = data.Year + "-" + data.Month;
            progressBar1.Maximum = dataGridView1.RowCount;
            progressBar1.Step = 1; 
            string mmm = "";
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                progressBar1.PerformStep();
                DataGridViewRow selectedRow = dataGridView1.Rows[i];
                pavadinimas = Convert.ToString(selectedRow.Cells[1].Value);
                pappavadinimas = Convert.ToString(selectedRow.Cells[2].Value);
                nr = Convert.ToString(selectedRow.Cells[3].Value);
                vienetai = Convert.ToString(selectedRow.Cells[4].Value);
                ikainis = Convert.ToString(selectedRow.Cells[5].Value);
                Suma = Convert.ToString(selectedRow.Cells["Suma"].Value);
                //mmm += "INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES(NULL, '" + pavadinimas + "', '" + pappavadinimas + "', '" + nr + "', '" + vienetai + "', '" + ikainis + "', '" + Suma + "', '" + datastring + "')WHERE NOT EXISTS SELECT *FROM `Gaminiai` WHERE `Pavadinimas`= '" + pavadinimas + "' AND `Papildomas_Pavadinimas`= '" + pappavadinimas + "' AND `Detales_nr`= '" + nr + "' AND `Vienetai` = '" + vienetai + "' AND `Ikainis`= '" + ikainis + "' AND `Suma`= '" + Suma + "' AND `Data`= '" + datastring + "' ;                ";
                if (int.Parse(SQLGet("SELECT COUNT(*) FROM `Gaminiai` WHERE `Pavadinimas`='" + pavadinimas + "' AND `Papildomas_Pavadinimas`='" + pappavadinimas + "' AND `Detales_nr`='" + nr + "' AND `Vienetai` ='" + vienetai + "' AND `Ikainis`='" + ikainis + "' AND `Suma`='" + Suma + "' AND `Data`='" + datastring + "'")) < 1)
                    mmm += "INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES(NULL, '" + pavadinimas + "', '" + pappavadinimas + "', '" + nr + "', '" + vienetai + "', '" + ikainis + "', '" + Suma + "', '" + datastring + "'); ";
                //if(int.Parse(SQLGet("SELECT COUNT(*) FROM `Gaminiai` WHERE `Pavadinimas`='"+ pavadinimas + "' AND `Papildomas_Pavadinimas`='"+ pappavadinimas + "' AND `Detales_nr`='"+ nr + "' AND `Vienetai` ='"+ vienetai + "' AND `Ikainis`='"+ ikainis + "' AND `Suma`='"+ Suma + "' AND `Data`='"+datastring+"'")) < 1) 
                //SQLExecute1("INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES(NULL, '"+ pavadinimas + "', '"+ pappavadinimas + "', '"+ nr+ "', '"+ vienetai + "', '"+ ikainis + "', '"+ Suma + "', '"+ datastring + "');");
            }
            SQLExecute(mmm);
            //SQLExecute1("INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES(NULL, '" + pavadinimas + "', '" + pappavadinimas + "', '" + nr + "', '" + vienetai + "', '" + ikainis + "', '" + Suma + "', '" + datastring + "');");
            UpdateTable("SELECT * FROM `Gaminiai`");
            prideti();
            prideti2();
            UpdateTable("SELECT * FROM `Gaminiai` WHERE `Data` = '-' ");
            progressBar1.Dispose();

        }
        void belekas()
        {
            using (StreamReader sr = new StreamReader("C:/Users/Lukas/Desktop/aaaa.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] l = line.Split('!');
                    SQLExecute("INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES(NULL, '" + l[0] + "', '" + l[1] + "', '" + l[2] + "', '" + l[3] + "', '" + l[4] + "', '" + l[5] + "', '" +'-' + "');");

                }
            }

        }
             
        
        

        private void ComboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateTable("SELECT * FROM `Gaminiai` WHERE `Data`='" + comboBox3.SelectedItem.ToString() + "'");
            comboBox1.Text = "";
            comboBox2.Text = "";
            Data = comboBox3.SelectedItem.ToString();

            prideti();
            
        }


        public void hjh()
        {
            string query = "SELECT * FROM `Gaminiai`";
            if (dataGridView1.Rows.Count == 0 || selected == "-")
            {
                query = "SELECT * FROM `Gaminiai`";
            }
            else
            {
                query = "SELECT * FROM `Gaminiai` WHERE `Data`='" + comboBox3.SelectedItem.ToString() + "'";
            }
            string dbConnectionString = string.Format("server=remotemysql.com;uid=R3WQl1cVvX;pwd=ar8Q7T0MWg;database=R3WQl1cVvX;port=3306;");
           

            var conn = new MySqlConnection(dbConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            //string pavadinimas = reader.GetString(0);
            conn.Close();
            label1.Text = pavadinimas;
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, dbConnectionString);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
            //dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["Data"].Visible = false;
        }
        public void UpdateTable(string query)
        {
            string dbConnectionString = string.Format("server=remotemysql.com;uid=R3WQl1cVvX;pwd=ar8Q7T0MWg;database=R3WQl1cVvX;port=3306;");
            var conn = new MySqlConnection(dbConnectionString);
            conn.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, dbConnectionString);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
        }
        static async public Task SQLExecute(string query)
        {
            string dbConnectionString = string.Format("server=remotemysql.com;uid=R3WQl1cVvX;pwd=ar8Q7T0MWg;database=R3WQl1cVvX;port=3306;");          
            var conn = new MySqlConnection(dbConnectionString);
            var cmd = new MySqlCommand(query, conn);
            conn.Open();
            var reader = cmd.ExecuteReader();
            conn.Close();
        }
        static public string SQLGet(string query)
        {
            string dbConnectionString = string.Format("server=remotemysql.com;uid=R3WQl1cVvX;pwd=ar8Q7T0MWg;database=R3WQl1cVvX;port=3306;");
            var conn = new MySqlConnection(dbConnectionString);
            var cmd = new MySqlCommand(query, conn);
            conn.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();
            int p = reader.GetInt32(0);
            conn.Close();
            return p.ToString();
        }

        async Task SQLExecute1(string query)
        {
            string dbConnectionString = string.Format("server=remotemysql.com;uid=R3WQl1cVvX;pwd=ar8Q7T0MWg;database=R3WQl1cVvX;port=3306;");
            var conn = new MySqlConnection(dbConnectionString);
            var cmd = new MySqlCommand(query, conn);
            conn.Open();
            var reader = cmd.ExecuteReader();
            conn.Close();
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            UpdateTable("SELECT * FROM `Gaminiai` WHERE `Data`='" + comboBox3.SelectedItem.ToString() + "'");

        }
    }
    
}
