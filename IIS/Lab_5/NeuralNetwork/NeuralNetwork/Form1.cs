﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork
{
    public partial class Form1 : Form
    {
        Neuron neuron;
        public Form1()
        {
            InitializeComponent();

            neuron = new Neuron();
            neuron.train();
        }

        int[] inputPixels = new int[15];

        private void ChangeState(Button b, int index)
        {
            if (b.BackColor == Color.Black)
            {
                b.BackColor = Color.White;
                inputPixels[index] = 0;
            }
            else if (b.BackColor == Color.White)
            {
                b.BackColor = Color.Black;
                inputPixels[index] = 1;
            }
        }        

        private void checkNumber_Click(object sender, EventArgs e)
        {
            string num = "";
            for (int i=0; i<inputPixels.Length;i++)
            {
                num += inputPixels[i].ToString();
            }
            if (neuron.proceed(num))
                MessageBox.Show("5");
            else
                MessageBox.Show("Не 5");
        }

        private void button1_Click_1(object sender, EventArgs e) => ChangeState(button1, 0);
        private void button2_Click(object sender, EventArgs e) => ChangeState(button2, 1);
        private void button3_Click(object sender, EventArgs e) => ChangeState(button3, 2);
        private void button4_Click(object sender, EventArgs e) => ChangeState(button4, 3);
        private void button5_Click(object sender, EventArgs e) => ChangeState(button5, 4);
        private void button6_Click(object sender, EventArgs e) => ChangeState(button6, 5);
        private void button7_Click(object sender, EventArgs e) => ChangeState(button7, 6);
        private void button8_Click(object sender, EventArgs e) => ChangeState(button8, 7);
        private void button9_Click(object sender, EventArgs e) => ChangeState(button9, 8);
        private void button10_Click(object sender, EventArgs e) => ChangeState(button10, 9);
        private void button11_Click(object sender, EventArgs e) => ChangeState(button11, 10);
        private void button12_Click(object sender, EventArgs e) => ChangeState(button12, 11);
        private void button13_Click(object sender, EventArgs e) => ChangeState(button13, 12);
        private void button14_Click(object sender, EventArgs e) => ChangeState(button14, 13);
        private void button15_Click(object sender, EventArgs e) => ChangeState(button15, 14);
    }
}
