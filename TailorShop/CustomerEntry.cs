using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TailorShop
{
    public partial class CustomerEntry : Form
    {
        public CustomerEntry()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string gender = "";

                if (radioButton1.Checked)
                {
                    gender = "પુરુષ";
                }
                else if(radioButton2.Checked)
                {
                    gender = "સ્ત્રી";
                }
                else
                {
                    gender = "";
                }
                SqlConnection con = new SqlConnection("Data Source=MEET-LP;Initial Catalog=TailorShop;Integrated Security=SSPI");
                SqlCommand cmd;
                con.Open();
                string s = "insert into tblCustomer values(@CustomerName,@CustomerMobileNo,@CustomerAddress,@EmailId,@Gender,@Status)";
                cmd = new SqlCommand(s, con);
                cmd.Parameters.AddWithValue("@CustomerName", textBox1.Text);
                cmd.Parameters.AddWithValue("@CustomerMobileNo", textBox2.Text);
                cmd.Parameters.AddWithValue("@CustomerAddress", textBox3.Text);
                cmd.Parameters.AddWithValue("@EmailId", textBox4.Text);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.CommandType = CommandType.Text;
                int i = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(i + " Row(s) Inserted ");
                gridbind();
                clrtextbox();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public void gridbind()
        {
            SqlConnection con = new SqlConnection("Data Source=MEET-LP;Initial Catalog=TailorShop;Integrated Security=SSPI");
            SqlCommand cmd;
            string s = "select CustomerID,CustomerName,CustomerMobileNo,CustomerAddress,EmailId,Gender from tblCustomer where Status='1'";
            cmd = new SqlCommand(s, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string gender = "";

                if (radioButton1.Checked)
                {
                    gender = "પુરુષ";
                }
                else if (radioButton2.Checked)
                {
                    gender = "સ્ત્રી";
                }
                else
                {
                    gender = "";
                }
                SqlConnection con = new SqlConnection("Data Source=MEET-LP;Initial Catalog=TailorShop;Integrated Security=SSPI");
                SqlCommand cmd;
                con.Open();
                string s = "UPDATE tblCustomer SET CustomerName=@CustomerName,CustomerMobileNo=@CustomerMobileNo,CustomerAddress=@CustomerAddress,EmailId=@EmailId,Gender=@Gender  WHERE CustomerID=" + textBox5.Text+"";
                cmd = new SqlCommand(s, con);
                cmd.Parameters.AddWithValue("@CustomerName", textBox1.Text);
                cmd.Parameters.AddWithValue("@CustomerMobileNo", textBox2.Text);
                cmd.Parameters.AddWithValue("@CustomerAddress", textBox3.Text);
                cmd.Parameters.AddWithValue("@EmailId", textBox4.Text);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.CommandType = CommandType.Text;
                int i = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(i + " Row(s) Updated ");
                gridbind();
                clrtextbox();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CustomerEntry_Load(object sender, EventArgs e)
        {
            gridbind();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;

            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            textBox5.Text = selectedRow.Cells[0].Value.ToString();
            textBox1.Text = selectedRow.Cells[1].Value.ToString();
            textBox2.Text = selectedRow.Cells[2].Value.ToString();
            textBox3.Text = selectedRow.Cells[3].Value.ToString();
            textBox4.Text = selectedRow.Cells[4].Value.ToString();

            string gender = selectedRow.Cells[5].Value.ToString();

            if (gender == "પુરુષ")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            enabletextbox();
            button1.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;

        }

        public void clrtextbox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clrtextbox();
            enabletextbox();

            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Customer ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=MEET-LP;Initial Catalog=TailorShop;Integrated Security=SSPI");
                    SqlCommand cmd;
                    con.Open();
                    string s = "UPDATE tblCustomer SET Status=@Status WHERE CustomerID=" + textBox5.Text + "";
                    cmd = new SqlCommand(s, con);
                    cmd.Parameters.AddWithValue("@Status", "0");
                    cmd.CommandType = CommandType.Text;
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show(i + " Row(s) Deleted ");
                    gridbind();
                    clrtextbox();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                // If 'No', do something here.
            }
        }

        public void enabletextbox()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
        }

    }
}
