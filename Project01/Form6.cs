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
    public partial class Form6 : Form
    {
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        DataSet dataSet;

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string connStr = "server=localhost;port=3306;database=한빛마트;uid=root;pwd="; // pwd 입력
            conn = new MySqlConnection(connStr);

            adapter = new MySqlDataAdapter("SELECT * FROM 회원", conn);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "회원");
            dataGridView1.DataSource = dataSet.Tables["회원"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;

            if (radioButton1.Checked)
            {
                if(textBox1.Text!="")
                {
                    sql = "SELECT * FROM 회원 WHERE 회원번호=@회원번호";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@회원번호", textBox1.Text);
                }
                else
                {
                    sql = "SELECT * FROM 회원";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@회원번호", textBox1.Text);
                }
            }
            else if(radioButton2.Checked)
            {
                if(textBox2.Text!="")
                {
                    sql = "SELECT * FROM 회원 WHERE 회원아이디=@회원아이디";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@회원아이디", textBox2.Text);
                }
                else
                {
                    sql = "SELECT * FROM 회원";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@회원아이디", textBox2.Text);
                }
            }
            else if (radioButton3.Checked)
            {
                if(textBox3.Text!="")
                {
                    sql = "SELECT * FROM 회원 WHERE 비밀번호=@비밀번호";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@비밀번호", textBox3.Text);
                }
                else
                {
                    sql = "SELECT * FROM 회원";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@비밀번호", textBox3.Text);
                }
            }
            else if (radioButton4.Checked)
            {
                if(textBox4.Text!="")
                {
                    sql = "SELECT * FROM 회원 WHERE 이름=@이름";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@이름", textBox4.Text);
                }
                else
                {
                    sql = "SELECT * FROM 회원";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@이름", textBox4.Text);
                }
            }
            else if (radioButton5.Checked)
            {
                if (textBox5.Text != "")
                {
                    sql = "SELECT * FROM 회원 WHERE 나이=@나이";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@나이", textBox5.Text);
                }
                else
                {
                    sql = "SELECT * FROM 회원";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@나이", textBox5.Text);
                }
            }
            else if (radioButton6.Checked)
            {
                if (textBox6.Text != "")
                {
                    sql = "SELECT * FROM 회원 WHERE 직업=@직업";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@직업", textBox6.Text);
                }
                else
                {
                    sql = "SELECT * FROM 회원";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@직업", textBox6.Text);
                }
            }
            else if (radioButton7.Checked)
            {
                if (textBox7.Text != "")
                {
                    sql = "SELECT * FROM 회원 WHERE 등급=@등급";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@등급", textBox7.Text);
                }
                else
                {
                    sql = "SELECT * FROM 회원";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@등급", textBox7.Text);
                }
                
            }
            else if (radioButton8.Checked)
            {
                if (textBox8.Text != "")
                {
                    sql = "SELECT * FROM 회원 WHERE 적립금=@적립금";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@적립금", textBox8.Text);
                }
                else
                {
                    sql = "SELECT * FROM 회원";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@적립금", textBox8.Text);
                }
            }
            try
            {
                conn.Open();
                dataSet.Clear();
                if (adapter.Fill(dataSet, "회원") > 0)
                {
                    dataGridView1.DataSource = dataSet.Tables["회원"];
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
            string sql = "Insert INTO 회원(회원아이디, 비밀번호, 이름, 나이, 직업, 등급, 적립금) " +
                            "VALUES(@회원아이디,@비밀번호, @이름, @나이, @직업, @등급, @적립금)";

            adapter.InsertCommand = new MySqlCommand(sql, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@회원아이디", textBox2.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@비밀번호", textBox3.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@이름", textBox4.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@나이", textBox5.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@등급", textBox6.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@직업", textBox7.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@적립금", textBox8.Text);

            DataRow newRow = dataSet.Tables["회원"].NewRow();
            newRow["회원아이디"] = textBox2.Text;
            newRow["비밀번호"] = textBox3.Text;
            newRow["이름"] = textBox4.Text;
            newRow["나이"] = textBox5.Text;
            newRow["등급"] = textBox6.Text;
            newRow["직업"] = textBox7.Text;
            newRow["적립금"] = textBox8.Text;
            dataSet.Tables["회원"].Rows.Add(newRow);

            try
            {
                conn.Open();
                adapter.Update(dataSet, "회원");
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
            string sql = "DELETE FROM 회원 WHERE 회원번호=@회원번호";
            adapter.DeleteCommand = new MySqlCommand(sql, conn);
            int id = (int)dataGridView1.SelectedRows[0].Cells["회원번호"].Value;
            adapter.DeleteCommand.Parameters.AddWithValue("@회원번호", id);

            try
            {
                conn.Open();
                if (adapter.DeleteCommand.ExecuteNonQuery() > 0)
                {
                    dataSet.Clear();
                    adapter.Fill(dataSet, "회원");
                    dataGridView1.DataSource = dataSet.Tables["회원"];
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
            string sql = "UPDATE 회원 SET 비밀번호=@비밀번호, 이름=@이름, 나이=@나이, 직업=@직업, " +
                "등급=@등급, 적립금=@적립금 Where 회원아이디=@회원아이디";
            adapter.UpdateCommand = new MySqlCommand(sql, conn);
            adapter.UpdateCommand.Parameters.AddWithValue("@회원아이디", textBox2.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@비밀번호", textBox3.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@이름", textBox4.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@나이", textBox5.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@직업", textBox6.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@등급", textBox7.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@적립금", textBox8.Text);


            int id = (int)dataGridView1.SelectedRows[0].Cells["회원번호"].Value;
            string filter = "회원번호=" + id;
            DataRow[] findRows = dataSet.Tables["회원"].Select(filter);
            findRows[0]["회원번호"] = id;
            findRows[0]["회원아이디"] = textBox2.Text;
            findRows[0]["비밀번호"] = textBox3.Text;
            findRows[0]["이름"] = textBox4.Text;
            findRows[0]["나이"] = textBox5.Text;
            findRows[0]["직업"] = textBox6.Text;
            findRows[0]["등급"] = textBox7.Text;
            findRows[0]["적립금"] = textBox8.Text;

            try
            {
                conn.Open();
                adapter.Update(dataSet, "회원");
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
