using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();

        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private const string ConnectionString = "Data Source =DESKTOP-MMMNIOM;Initial Catalog=db_project;Integrated Security=True";
        SqlConnection con = new SqlConnection(ConnectionString);
        SqlCommand scmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool mode = true;
        string sql;

        public void Load()
        {
            try
            {
                sql = "select* from db_table1";
                scmd = new SqlCommand(sql, con);
                con.Open();
                read = scmd.ExecuteReader();
                drr = new SqlDataAdapter(sql, con);
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getID(string id)
        {
            sql ="select *from db_table1 where id = " + id + " ";
            scmd = new SqlCommand(sql, con);
            con.Open();
            read = scmd.ExecuteReader();

            while(read.Read())
            {
                sname.Text = read[1].ToString();
                scourse.Text = read[2].ToString();
                sfees.Text = read[3].ToString();
            }
            con.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string name = sname.Text;
            string course = scourse.Text;
            string fee = sfees.Text;

            if (mode == true)
            {
                sql = "insert into db_table1(sname, scourse, fees) values(@name,@course,@fee)";
                con.Open();
                scmd = new SqlCommand(sql, con);
                scmd.Parameters.AddWithValue("@name", name);
                scmd.Parameters.AddWithValue("@course", course);
                scmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Record Added");
                scmd.ExecuteNonQuery();

                sname.Clear();
                scourse.Clear();
                sfees.Clear();
                sname.Focus();
            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update db_table1 set sname=@name, scourse=@course, fees=@fee where id=@id";
                con.Open();
                scmd = new SqlCommand(sql, con);
                scmd.Parameters.AddWithValue("@name", name);
                scmd.Parameters.AddWithValue("@course", course);
                scmd.Parameters.AddWithValue("@fee", fee);
                scmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated");
                scmd.ExecuteNonQuery();

                sname.Clear();
                scourse.Clear();
                sfees.Clear();
                sname.Focus();
                button1.Text = "Submit";
                mode = true;

            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                mode= false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";

            }
            else if(e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from db_table1 where id=@id";
                con.Open();
                scmd = new SqlCommand(sql, con);
                scmd.Parameters.AddWithValue("@id", id);
                scmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();

            }
        }

   

       

        private void button3_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sname.Clear();
            scourse.Clear();
            sfees.Clear();
            sname.Focus();
        }
    }  
}
