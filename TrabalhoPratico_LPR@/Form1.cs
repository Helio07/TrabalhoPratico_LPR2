using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace TrabalhoPratico_LPR_
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Helio Sette\Documents\BDsupermercado.mdf"";Integrated Security=True;Connect Timeout=30");

        public Form1()
        {
            InitializeComponent();
        }

        private void usuarioBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'bDsupermercadoDataSet.Usuario'. Você pode movê-la ou removê-la conforme necessário.
            

        }

        private void loginTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String login, senha;
            bool achou = false;

            con.Open();
            String query = " SELECT * FROM Usuario";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                login = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                senha = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                if (login == loginTextBox.Text && senha == senhaTextBox.Text) 
                {
                    achou = true;
                    Form2 f2 = new Form2(this); 
                    f2.Show();
                    this.Hide();
                    break;
                }
            }
            if (achou == false) { MessageBox.Show("Usuario não encontrado!!"); }
            con.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
