using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace TallyBuddy
{
    public partial class Form2 : Form
    {
        SqlConnection Tallyserver;
        SqlDataReader rdr;
        SqlCommand Retrivecmd;
        String city_id, connectionString, Tallyserviceaddress, CompanyName;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (File.Exists("ServerConfig.txt"))
            {
                this.Hide();
                Form1 home = new Form1();
                home.ShowDialog();
                this.Close();
            }
            else
            {
                Tallyserver = new SqlConnection(ConfigurationManager.ConnectionStrings["Tallyserver"].ConnectionString);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Tallyserver.Close();
            Tallyserver.Open();
            city_id = comboBox1.SelectedItem.ToString();
            Retrivecmd = new SqlCommand("select * from ConfigureMyTallyBuddy where city_id=" + city_id, Tallyserver);
            rdr = Retrivecmd.ExecuteReader();
            if (rdr.Read())
            {
                connectionString = (string)rdr["connectionString"];
                Tallyserviceaddress = (string)rdr["Tallyserviceaddress"];
                CompanyName = (string)rdr["CompanyName"];
                


                using (System.IO.StreamWriter file = new System.IO.StreamWriter("ServerConfig.txt", true))
                {

                    file.WriteLine("connectionString=" + connectionString);
                    file.WriteLine("serviceaddress=" + Tallyserviceaddress);
                    file.WriteLine("CompanyName=" + CompanyName);
                    file.WriteLine("city_id=" + city_id);
                    file.Close();


                    this.Hide();
                    Form1 home = new Form1();
                    home.ShowDialog();
                    this.Close();
                }
            }

        }
        void IsFileExist()
        {

        }

    }
}
