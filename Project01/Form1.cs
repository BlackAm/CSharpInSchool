﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 Dig = new Form2();
            Dig.Owner = this;
            Dig.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 Dig = new Form3();
            Dig.Owner = this;
            Dig.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 Dig = new Form4();
            Dig.Owner = this;
            Dig.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 Dig = new Form5();
            Dig.Owner = this;
            Dig.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 Dig = new Form6();
            Dig.Owner = this;
            Dig.ShowDialog();
        }
    }
}