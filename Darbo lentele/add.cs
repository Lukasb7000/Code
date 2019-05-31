using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Darbo_lentele
{
    public partial class add : Form
    {
        public static int vnt;
        public static bool k = false;
        public add()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Add_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.pavadinimas;
            textBox2.Text = Form1.pappavadinimas;
            textBox3.Text = Form1.nr;
            textBox4.Text = Form1.vienetai;
            textBox5.Text = Form1.ikainis;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.All(char.IsDigit)&&Convert.ToInt32(textBox4.Text)>=0)
            {
                if (Form1.Data != "-")
                {

                    vnt = Convert.ToInt32(textBox4.Text);
                    double suma = vnt * double.Parse(Form1.ikainis);
                    Form1.SQLExecute("UPDATE `Gaminiai` SET `Vienetai` = '"+vnt+"', `Suma` = '"+suma+"' WHERE `Gaminiai`.`ID` = "+Form1.IDOFselected+";");
                }
                //else
                //{
                //    DateTime data = new DateTime();
                //    data = DateTime.Now;
                //    string datastring = data.Year + "-" + data.Month;
                //    vnt = Convert.ToInt32(textBox4.Text);
                //    double suma = vnt * double.Parse(Form1.ikainis);
                //    Form1.SQLExecute("INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES (NULL, '" + Form1.pavadinimas + "', '" + Form1.pappavadinimas + "', '" + Form1.nr + "', '" + vnt + "', '" + Form1.ikainis + "', '" + suma + "', '" + datastring + "');");
                //}

            }
            this.Close();
            
        }
 
    }

}
