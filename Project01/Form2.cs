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
    public partial class Form2 : Form
    {
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        DataSet dataSet;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connStr = "server=localhost;port=3306;database=한빛마트;uid=root;pwd="; // pwd 
            conn = new MySqlConnection(connStr);

            adapter = new MySqlDataAdapter("SELECT * FROM 게시글", conn);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "게시글");
            dataGridView1.DataSource = dataSet.Tables["게시글"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;

            if (radioButton1.Checked)
            {
                if(textBox1.Text!="")
                {
                    sql = "SELECT * FROM 게시글 WHERE 글번호=@글번호";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@글번호", textBox1.Text);
                }
                else
                {
                    sql = "SELECT * FROM 게시글";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@글번호", textBox1.Text);
                }
            }
            else if (radioButton2.Checked)
            {
                if (textBox2.Text != "")
                {
                    sql = "SELECT * FROM 게시글 WHERE 글내용=@글내용";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@글내용", textBox2.Text);
                }
                else
                {
                    sql = "SELECT * FROM 게시글";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@글내용", textBox2.Text);
                }
            }
            else if (radioButton3.Checked)
            {
                if (textBox3.Text != "")
                {
                    sql = "SELECT * FROM 게시글 WHERE 작성일자=@작성일자";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@작성일자", textBox3.Text);
                }
                else
                {
                    sql = "SELECT * FROM 게시글";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@작성일자", textBox3.Text);
                }
            }
            else if (radioButton4.Checked)
            {
                if (textBox4.Text != "")
                {
                    sql = "SELECT * FROM 게시글 WHERE 글제목=@글제목";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@글제목", textBox4.Text);
                }
                else
                {
                    sql = "SELECT * FROM 게시글";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@글제목", textBox4.Text);
                }
            }
            else if (radioButton5.Checked)
            {
                if (textBox5.Text != "")
                {
                    sql = "SELECT * FROM 게시글 WHERE 회원아이디=@회원아이디";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@회원아이디", textBox5.Text);
                }
                else
                {
                    sql = "SELECT * FROM 게시글";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@회원아이디", textBox5.Text);
                }
            }
            try
            {
                conn.Open();
                dataSet.Clear();
                if (adapter.Fill(dataSet, "게시글") > 0)
                {
                    dataGridView1.DataSource = dataSet.Tables["게시글"];
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
            string sql = "Insert INTO 게시글 (글제목, 작성일자,글내용, 회원아이디)" +
                "VALUES(@글제목,@작성일자 ,@글내용, @회원아이디)";

            adapter.InsertCommand = new MySqlCommand(sql, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@글제목", textBox4.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@글내용", textBox2.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@작성일자", DateTime.Now.ToString("yyyy-MM-dd"));
            adapter.InsertCommand.Parameters.AddWithValue("@회원아이디", textBox5.Text);

            DataRow newRow = dataSet.Tables["게시글"].NewRow();
            //newRow["글번호"] = textBox1.Text;
            newRow["글내용"] = textBox2.Text;
            newRow["글제목"] = textBox4.Text;
            newRow["작성일자"] = DateTime.Now.ToString("yyyy-MM-dd");
            newRow["회원아이디"] = textBox5.Text;
            dataSet.Tables["게시글"].Rows.Add(newRow);

            try
            {
                conn.Open();
                adapter.Update(dataSet, "게시글");
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
            string sql = "DELETE FROM 게시글 WHERE 글번호=@글번호";
            adapter.DeleteCommand = new MySqlCommand(sql, conn);
            int id = (int)dataGridView1.SelectedRows[0].Cells["글번호"].Value;
            adapter.DeleteCommand.Parameters.AddWithValue("@글번호", id);

            try
            {
                conn.Open();
                if (adapter.DeleteCommand.ExecuteNonQuery() > 0)
                {
                    dataSet.Clear();
                    adapter.Fill(dataSet, "게시글");
                    dataGridView1.DataSource = dataSet.Tables["게시글"];
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
            string sql = "UPDATE 게시글 SET 글내용=@글내용, 글제목=@글제목 Where 회원아이디=@회원아이디";
            string query = "SELECT COUNT(*) FROM 게시글";
            MySqlCommand cnt = new MySqlCommand(query, conn);

            adapter.UpdateCommand = new MySqlCommand(sql, conn);
            adapter.UpdateCommand.Parameters.AddWithValue("@글내용", textBox2.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@글제목", textBox4.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@회원아이디", textBox5.Text);

            int id = (int)dataGridView1.SelectedRows[0].Cells["글번호"].Value;
            string filter = "글번호=" + id;
            DataRow[] findRows = dataSet.Tables["게시글"].Select(filter);
            findRows[0]["글내용"] = textBox2.Text;
            findRows[0]["글제목"] = textBox4.Text;

            try
            {
                conn.Open();
                
                adapter.Update(dataSet, "게시글");
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
    }
}
