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
    public partial class Form5 : Form
    {
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        DataSet dataSet;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string connStr = "server=localhost;port=3306;database=한빛마트;uid=root;pwd=2ehdrjsrjs";
            conn = new MySqlConnection(connStr);

            adapter = new MySqlDataAdapter("SELECT * FROM 주문", conn);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "주문");
            dataGridView1.DataSource = dataSet.Tables["주문"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;

            if (radioButton1.Checked)
            {
                sql = "SELECT * FROM 주문 WHERE 회원아이디=@회원아이디";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@회원아이디", textBox1.Text);
            }
            else if (radioButton2.Checked)
            {
                sql = "SELECT * FROM 주문 WHERE 상품번호=@상품번호";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@상품번호", textBox2.Text);
            }
            else if (radioButton3.Checked)
            {
                sql = "SELECT * FROM 주문 WHERE 주문번호=@주문번호";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@주문번호", textBox3.Text);
            }
            else if (radioButton4.Checked)
            {
                sql = "SELECT * FROM 주문 WHERE 주문수량=@주문수량";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@주문수량", textBox4.Text);

            }
            else if (radioButton5.Checked)
            {
                sql = "SELECT * FROM 주문 WHERE 배송지=@배송지";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@배송지", textBox5.Text);
            }
            else if (radioButton6.Checked)
            {
                sql = "SELECT * FROM 주문 WHERE 주문일자=@주문일자";
                adapter.SelectCommand = new MySqlCommand(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@주문일자", textBox6.Text);
            }
            try
            {
                conn.Open();
                dataSet.Clear();
                if (adapter.Fill(dataSet, "주문") > 0)
                {
                    dataGridView1.DataSource = dataSet.Tables["주문"];
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
            string sql = "Insert INTO 주문(회원아이디, 상품번호, 주문수량, 배송지, 주문일자) " +
                            "VALUES(@회원아이디, @상품번호,@주문수량, @배송지, @주문일자)";

            adapter.InsertCommand = new MySqlCommand(sql, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@회원아이디", textBox1.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@상품번호", textBox2.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@주문수량", textBox4.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@배송지", textBox5.Text);
            adapter.InsertCommand.Parameters.AddWithValue("@주문일자", DateTime.Now.ToString("yyyy-MM-dd"));

            DataRow newRow = dataSet.Tables["주문"].NewRow();
            newRow["회원아이디"] = textBox1.Text;
            newRow["상품번호"] = textBox2.Text;
            newRow["주문수량"] = textBox4.Text;
            newRow["배송지"] = textBox5.Text;
            newRow["주문일자"] = DateTime.Now.ToString("yyyy-MM-dd");
            dataSet.Tables["주문"].Rows.Add(newRow);

            try
            {
                conn.Open();
                adapter.Update(dataSet, "주문");
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
            string sql = "DELETE FROM 주문 WHERE 주문번호=@주문번호";
            adapter.DeleteCommand = new MySqlCommand(sql, conn);
            int id = (int)dataGridView1.SelectedRows[0].Cells["주문번호"].Value;
            adapter.DeleteCommand.Parameters.AddWithValue("@주문번호", id);

            try
            {
                conn.Open();
                if (adapter.DeleteCommand.ExecuteNonQuery() > 0)
                {
                    dataSet.Clear();
                    adapter.Fill(dataSet, "주문");
                    dataGridView1.DataSource = dataSet.Tables["주문"];
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
            string sql = "UPDATE 주문 SET 상품번호=@상품번호, " +
                "주문수량=@주문수량, 배송지=@배송지 Where 회원아이디=@회원아이디";
            adapter.UpdateCommand = new MySqlCommand(sql, conn);
            adapter.UpdateCommand.Parameters.AddWithValue("@회원아이디", textBox1.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@상품번호", textBox2.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@주문수량", textBox4.Text);
            adapter.UpdateCommand.Parameters.AddWithValue("@배송지", textBox5.Text);

            int id = (int)dataGridView1.SelectedRows[0].Cells["주문번호"].Value;
            string filter = "주문번호=" + id;
            DataRow[] findRows = dataSet.Tables["주문"].Select(filter);
            findRows[0]["상품번호"] = textBox2.Text;
            findRows[0]["주문수량"] = textBox4.Text;
            findRows[0]["배송지"] = textBox5.Text;

            try
            {
                conn.Open();
                adapter.Update(dataSet, "주문");
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
