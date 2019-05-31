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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            DateTime data = new DateTime();
            data = DateTime.Now;
            string datastring = data.Year + "-" + data.Month;
            if (textBox4.Text == "0")
                Form1.SQLExecute("INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + 0 + "', '" + textBox5.Text + "', '0', '-');");
            else if(int.Parse(textBox4.Text) > 0 && double.Parse(textBox5.Text) > 0)
            {
                Form1.SQLExecute("INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + 0 + "', '" + textBox5.Text + "', '0', '-');");
                double s = int.Parse(textBox4.Text) * double.Parse(textBox5.Text);
                Form1.SQLExecute("INSERT INTO `Gaminiai` (`ID`, `Pavadinimas`, `Papildomas_Pavadinimas`, `Detales_nr`, `Vienetai`, `Ikainis`, `Suma`, `Data`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '"+s+"', '"+datastring+"');");
            }
            this.Close();
        }
    }
}
