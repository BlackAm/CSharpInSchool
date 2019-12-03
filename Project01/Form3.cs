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
    public partial class Form3 : Form
    {
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        DataSet dataSet;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connStr = "server=localhost;port=3306;database=한빛마트;uid=root;pwd="; // pwd 입력
            conn = new MySqlConnection(connStr);

            adapter = new MySqlDataAdapter("SELECT * FROM 상품", conn);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "상품");
            dataGridView1.DataSource = dataSet.Tables["상품"];
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sql;

            if (radioButton1.Checked)
            {
                if(textBox1.Text!="")
                {
                    sql = "SELECT * FROM 상품 WHERE 상품번호=@상품번호";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@상품번호", textBox1.Text);
                }
                else
                {
                    sql = "SELECT * FROM 상품";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@상품번호", textBox1.Text);
                }
            }
            else if (radioButton2.Checked)
            {
                if (textBox2.Text != "")
                {
                    sql = "SELECT * FROM 상품 WHERE 상품명=@상품명";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@상품명", textBox2.Text);
                }
                else
                {
                    sql = "SELECT * FROM 상품";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@상품명", textBox2.Text);
                }
                
            }
            else if (radioButton3.Checked)
            {
                if (textBox3.Text != "")
                {
                    sql = "SELECT * FROM 상품 WHERE 재고량=@재고량";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@재고량", textBox3.Text);
                }
                else
                {
                    sql = "SELECT * FROM 상품";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@재고량", textBox3.Text);
                }
                
            }
            else if (radioButton4.Checked)
            {
                if (textBox4.Text != "")
                {
                    sql = "SELECT * FROM 상품 WHERE 단가=@단가";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@단가", textBox4.Text);
                }
                else
                {
                    sql = "SELECT * FROM 상품";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@단가", textBox4.Text);
                }
                

            }
            else if (radioButton5.Checked)
            {
                if (textBox5.Text != "")
                {
                    sql = "SELECT * FROM 상품 WHERE 제조업체명=@제조업체명";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@제조업체명", textBox5.Text);
                }
                else
                {
                    sql = "SELECT * FROM 상품";
                    adapter.SelectCommand = new MySqlCommand(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@제조업체명", textBox5.Text);
                }
                
            }
            try
            {
                conn.Open();
                dataSet.Clear();
                if (adapter.Fill(dataSet, "상품") > 0)
                {
                    dataGridView1.DataSource = dataSet.Tables["상품"];
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            string sql = "Insert INTO 상품(단가,상품명, 재고량,제조업체명) " +
                            "VALUES(@단가,@상품명,@재고량,@제조업체명)";

            adapter.InsertCommand = new MySqlCommand(sql, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@상품명", textBox2.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@재고량", textBox3.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@단가", textBox4.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@제조업체명", textBox5.Text);

            DataRow newRow = dataSet.Tables["상품"].NewRow();
            newRow["상품명"] = textBox2.Text;
            newRow["재고량"] = textBox3.Text;
            newRow["단가"] = textBox4.Text;
            newRow["제조업체명"] = textBox5.Text;
            dataSet.Tables["상품"].Rows.Add(newRow);

            try
            {
                conn.Open();
                adapter.Update(dataSet, "상품");
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

        private void button3_Click_1(object sender, EventArgs e)
        {
            string sql = "DELETE FROM 상품 WHERE 상품번호=@상품번호";
            adapter.DeleteCommand = new MySqlCommand(sql, conn);
            int id = (int)dataGridView1.SelectedRows[0].Cells["상품번호"].Value;
            adapter.DeleteCommand.Parameters.AddWithValue("@상품번호", id);

            try
            {
                conn.Open();
                if (adapter.DeleteCommand.ExecuteNonQuery() > 0)
                {
                    dataSet.Clear();
                    adapter.Fill(dataSet, "상품");
                    dataGridView1.DataSource = dataSet.Tables["상품"];
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            string sql = "UPDATE 상품 SET 재고량=@재고량, 단가=@단가, 제조업체명=@제조업체명 Where 상품명=@상품명";
            adapter.UpdateCommand = new MySqlCommand(sql, conn);
            adapter.UpdateCommand.Parameters.AddWithValue("@상품명", textBox2.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@재고량", textBox3.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@단가", textBox4.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@제조업체명", textBox5.Text);

            int id = (int)dataGridView1.SelectedRows[0].Cells["상품번호"].Value;
            string filter = "상품번호=" + id;
            DataRow[] findRows = dataSet.Tables["상품"].Select(filter);
            findRows[0]["재고량"] = textBox3.Text;
            findRows[0]["단가"] = textBox4.Text;
            findRows[0]["제조업체명"] = textBox5.Text;

            try
            {
                conn.Open();
                adapter.Update(dataSet, "상품");
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
