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
using System.Configuration;

namespace TrabalhoPratico_LPR_
{
    public partial class CaixaForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Helio Sette\Documents\BDsupermercado.mdf"";Integrated Security=True;Connect Timeout=30");
        public CaixaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double preco, subtotal;
            string spreco, nome;
            int quant, cod;

            con.Open();
            String query = " SELECT * FROM Estoque WHERE codigo ='"+ textBox1.Text+ "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            nome = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            spreco = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            preco = Convert.ToDouble(spreco);
            quant = Convert.ToInt32(textBox2.Text);

            subtotal = preco * quant;
            con.Close();

            //query = " INSERT INTO (Cod_produto, Nome_produto, Preco_produto, Subtotal) Values ('"+textBox1.Text+"' , '"+nome+ "', '"+preco+"', '"+subtotal+"') ";
            //sda = new SqlDataAdapter(query, con);
            //sda.SelectCommand.ExecuteNonQuery();

            con.Open();
            using (var cmd = new SqlCommand(
                @"INSERT INTO Nota (Cod_produto, Nome_produto, Preco_produto, Quantidade,  Subtotal) Values (@Cod_produto, @Nome_produto, @Preco_produto, @Quantidade, @Subtotal)", con))
            {
                cmd.Parameters.AddWithValue("@Cod_produto", textBox1.Text);
                cmd.Parameters.AddWithValue("@Nome_produto", nome);
                cmd.Parameters.AddWithValue("@Preco_produto", preco);
                cmd.Parameters.AddWithValue("@Quantidade", quant);
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.ExecuteNonQuery();
            }
            con.Close();


            con.Open();
            query = " SELECT Cod_produto, Nome_produto, Preco_produto, Quantidade,  Subtotal  FROM Nota ";
            DataTable dt = new DataTable();
            sda = new SqlDataAdapter(query, con);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            con.Open();
            query = " SELECT SUM(Subtotal)  FROM Nota ";
            sda = new SqlDataAdapter(query, con);
            ds = new DataSet();

            sda.Fill(ds);

            label3.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            con.Close();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void CaixaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            con.Open();
            String query = " DELETE FROM Nota ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            con.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
