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

namespace Project01
{
    public partial class Form4 : Form
    {
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        DataSet dataSet;
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;

            if (radioButton1.Checked)
            {
                sql = "SELECT * FROM 제조업체 WHERE 제조업체명=@제조업체명";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@제조업체명", textBox1.Text);
            }
            else if (radioButton2.Checked)
            {
                sql = "SELECT * FROM 제조업체 WHERE 업체번호=@업체번호";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@업체번호", textBox2.Text);
            }
            else if (radioButton3.Checked)
            {
                sql = "SELECT * FROM 제조업체 WHERE 전화번호=@전화번호";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@전화번호", textBox3.Text);
            }
            else if (radioButton4.Checked)
            {
                sql = "SELECT * FROM 제조업체 WHERE 위치=@위치";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@위치", textBox4.Text);

            }
            else if (radioButton5.Checked)
            {
                sql = "SELECT * FROM 제조업체 WHERE 담당자=@담당자";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@담당자", textBox5.Text);
            }
            try
            {
                conn.Open();
                dataSet.Clear();
                if (adapter.Fill(dataSet, "제조업체") > 0)
                {
                    dataGridView1.DataSource = dataSet.Tables["제조업체"];
                }
                else
                    MessageBox.Show("검색된 데이터가 없습니다!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "Insert INTO 제조업체(제조업체명, 전화번호, 위치, 담당자) " +
                            "VALUES(@제조업체명,@전화번호,@위치,@담당자)";

            adapter.InsertCommand = new MySqlCommand(sql, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@제조업체명", textBox1.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@전화번호", textBox2.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@위치", textBox3.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@담당자", textBox4.Text);

            DataRow newRow = dataSet.Tables["제조업체"].NewRow();
            newRow["제조업체명"] = textBox1.Text;
            newRow["전화번호"] = textBox2.Text;
            newRow["위치"] = textBox3.Text;
            newRow["담당자"] = textBox4.Text;
            dataSet.Tables["제조업체"].Rows.Add(newRow);

            try
            {
                conn.Open();
                adapter.Update(dataSet, "제조업체");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM 제조업체 WHERE 업체번호=@업체번호";
            adapter.DeleteCommand = new MySqlCommand(sql, conn);
            int id = (int)dataGridView1.SelectedRows[0].Cells["업체번호"].Value;
            adapter.DeleteCommand.Parameters.AddWithValue("@업체번호", id);

            try
            {
                conn.Open();
                if (adapter.DeleteCommand.ExecuteNonQuery() > 0)
                {
                    dataSet.Clear();
                    adapter.Fill(dataSet, "제조업체");
                    dataGridView1.DataSource = dataSet.Tables["제조업체"];
                }
                else
                {
                    MessageBox.Show("삭제된 데이터가 없습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE 제조업체 SET 전화번호=@전화번호, 위치=@위치, 담당자=@담당자 Where 제조업체명=@제조업체명";
            adapter.UpdateCommand = new MySqlCommand(sql, conn);
            adapter.UpdateCommand.Parameters.AddWithValue("@제조업체명", textBox1.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@전화번호", textBox2.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@위치", textBox3.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@담당자", textBox4.Text);

            int id = (int)dataGridView1.SelectedRows[0].Cells["업체번호"].Value;
            string filter = "업체번호=" + id;
            DataRow[] findRows = dataSet.Tables["제조업체"].Select(filter);
            findRows[0]["전화번호"] = textBox2.Text;
            findRows[0]["위치"] = textBox3.Text;
            findRows[0]["담당자"] = textBox4.Text;
            
            try
            {
                conn.Open();
                adapter.Update(dataSet, "제조업체");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Form4_Load_1(object sender, EventArgs e)
        {
            string connStr = "server=localhost;port=3306;database=한빛마트;uid=root;pwd=2ehdrjsrjs";
            conn = new MySqlConnection(connStr);

            adapter = new MySqlDataAdapter("SELECT * FROM 제조업체", conn);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "제조업체");
            dataGridView1.DataSource = dataSet.Tables["제조업체"];
        }
    }
}
