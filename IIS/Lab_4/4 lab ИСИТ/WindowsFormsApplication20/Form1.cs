using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication20
{
    public partial class Form1 : Form
    {
        solution solu;
        int[,] arrays;

        public Form1()
        {
            solu = new solution();
            arrays = new int[4, 5] {   { 1, 2, 3, 4, 5 },//матрица оценок от экспертов
                                       { 2, 1, 3, 5, 4 },
                                       { 3, 1, 2, 4, 5 },
                                       { 2, 3, 4, 5, 1 }};
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private void button7_Click(object sender, EventArgs e) //явный победитель
        {
            //передача в массив
            int[,] array = new int[dataGridView1.Rows.Count, dataGridView1.Columns.Count];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    array[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);

            //решение
            int [] reshenie = solu.yavn_winner(array);

            dataGridView2.Rows.Clear();
            //загружаем решение
            for (int k = 0; k < reshenie.GetLength(0); k++)
              dataGridView2[k,0].Value = reshenie[k];
            button1.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)//Правило Копленда
        {
            //передача в массив
            int[,] array = new int[dataGridView3.Rows.Count, dataGridView3.Columns.Count];
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                    array[i, j] = Convert.ToInt32(dataGridView3.Rows[i].Cells[j].Value);

            //решение
            int[] reshenie = solu.kopland(array);

            dataGridView4.Rows.Clear();
            //загружаем решение
            for (int k = 0; k < reshenie.GetLength(0); k++)
                dataGridView4[k, 0].Value = reshenie[k];
        }

        private void button9_Click(object sender, EventArgs e)//Правило Симпсона
        {
            int[,] array = new int[dataGridView5.Rows.Count, dataGridView5.Columns.Count];
            for (int i = 0; i < dataGridView5.Rows.Count; i++)
                for (int j = 0; j < dataGridView5.Columns.Count; j++)
                    array[i, j] = Convert.ToInt32(dataGridView5.Rows[i].Cells[j].Value);

            //решение
            int[] reshenie = solu.simpson(array);

            dataGridView6.Rows.Clear();
            //загружаем решение
            for (int k = 0; k < reshenie.GetLength(0); k++)
                dataGridView6[k, 0].Value = reshenie[k];
        }        

        private void button10_Click(object sender, EventArgs e)//модель Борда
        {
            int[,] array = new int[dataGridView9.Rows.Count, dataGridView9.Columns.Count];
            for (int i = 0; i < dataGridView9.Rows.Count; i++)
                for (int j = 0; j < dataGridView9.Columns.Count; j++)
                    array[i, j] = Convert.ToInt32(dataGridView9.Rows[i].Cells[j].Value);

            int[] candidat = solu.board(array);

            dataGridView8.Rows.Clear();
            for (int i = 0; i<candidat.Length; i++)
                dataGridView8[i, 0].Value = candidat[i];
        }
        

        private void button11_Click(object sender, EventArgs e)//заполнение для Симпсона
        {
            dataGridView5.Rows.Clear();
            dataGridView5.RowCount = arrays.GetLength(0);
            dataGridView5.ColumnCount = arrays.GetLength(1);
            for (int i = 0; i < arrays.GetLength(0); i++)
                for (int j = 0; j < arrays.GetLength(1); j++)
                    dataGridView5.Rows[i].Cells[j].Value = arrays[i, j];
        }

        private void button12_Click(object sender, EventArgs e)//заполнение для явного победителя
        {
            dataGridView1.Rows.Clear();
            dataGridView1.RowCount = arrays.GetLength(0);
            dataGridView1.ColumnCount = arrays.GetLength(1);
            for (int i = 0; i < arrays.GetLength(0); i++)
                for (int j = 0; j < arrays.GetLength(1); j++)
                    dataGridView1.Rows[i].Cells[j].Value = arrays[i, j];
        }

        private void button13_Click(object sender, EventArgs e)//заполнение для правила Копленда
        {
            dataGridView3.Rows.Clear();
            dataGridView3.RowCount = arrays.GetLength(0);
            dataGridView3.ColumnCount = arrays.GetLength(1);
            for (int i = 0; i < arrays.GetLength(0); i++)
                for (int j = 0; j < arrays.GetLength(1); j++)
                    dataGridView3.Rows[i].Cells[j].Value = arrays[i, j];
        }

        private void button14_Click(object sender, EventArgs e)//заполнение для модели Борда 
        {
            dataGridView9.Rows.Clear();
            dataGridView9.RowCount = arrays.GetLength(0);
            dataGridView9.ColumnCount = arrays.GetLength(1);
            for (int i = 0; i < arrays.GetLength(0); i++)
                for (int j = 0; j < arrays.GetLength(1); j++)
                    dataGridView9.Rows[i].Cells[j].Value = arrays[i, j];
        }

        private void button3_Click(object sender, EventArgs e)//относительное большинство
        {
            if (radioButton1.Checked)
            {
                dataGridView7[0, 0].Value = Convert.ToInt32(dataGridView7[0, 0].Value) + 1;
                radioButton1.Checked = false;
            }
            else if (radioButton2.Checked)
            {
                dataGridView7[1, 0].Value = Convert.ToInt32(dataGridView7[1, 0].Value) + 1;
                radioButton2.Checked = false;
            }
            else if (radioButton3.Checked)
            {
                dataGridView7[2, 0].Value = Convert.ToInt32(dataGridView7[2, 0].Value) + 1;
                radioButton3.Checked = false;
            }
            else if (radioButton4.Checked)
            {
                dataGridView7[3, 0].Value = Convert.ToInt32(dataGridView7[3, 0].Value) + 1;
                radioButton4.Checked = false;
            }
            else if (radioButton5.Checked)
            {
                dataGridView7[4, 0].Value = Convert.ToInt32(dataGridView7[4, 0].Value) + 1;
                radioButton5.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int m = Convert.ToInt32(dataGridView2[0, 0].Value);
            string win = "None";
            for (int i = 0; i< dataGridView2.Columns.Count; i++)
            {
                if (m < Convert.ToInt32(dataGridView2[i, 0].Value))
                {
                    m = Convert.ToInt32(dataGridView2[i,0].Value);
                    win = dataGridView2.Columns[i].HeaderText;

                }
                   

            }
            label4.Text = "Победитель -- " + win;
        }
    }
}

