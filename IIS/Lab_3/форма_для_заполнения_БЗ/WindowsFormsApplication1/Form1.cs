using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//TO DO: интерфейс запросов 2 и 3
//       метод в sem_net (фотка у яны в тетради)
//       запрос 1: является ли одна нода в иерархии другой ноды
//       запрос 2: вывести все ноды по заданной ноде и типу линка
//       запрос 3: самим придумать


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        SemantincNetwork sem_net;
        Dictionary<string, string> link_names = new Dictionary<string, string>
        {
            { "является родителем", "является подклассом" },
            { "имеет симптом", "относится к заболеванию" },
            { "влияет на", "подвержен влиянию" },
            { "а конкретно", "описывает" }
        };
        BinaryFormatter formatter = new BinaryFormatter();

        public Form1()
        {
            sem_net = new SemantincNetwork();            
            InitializeComponent();
            for (int i=0; i<sem_net.l_node.Count;i++)
            {
                checkedListBox1.Items.Add(sem_net.l_node[i].name);
                checkedListBox2.Items.Add(sem_net.l_node[i].name);
            }
            foreach (var pair in link_names)
                comboBox1.Items.Add(pair.Key);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int number1 = checkedListBox1.SelectedIndex;
            int number2 = checkedListBox2.SelectedIndex;
            string link_name_in = comboBox1.SelectedItem.ToString();
            string link_name_out = link_names[comboBox1.SelectedItem.ToString()];
            sem_net.add_link(number1, number2, link_name_in, link_name_out);
            MessageBox.Show("Добавлено!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog SvDlg = new SaveFileDialog();
            SvDlg.Filter = "Файлы .txt|*.txt";
            if (SvDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = SvDlg.FileName;
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, sem_net);
                    MessageBox.Show("Файл сохранен");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpDlg = new OpenFileDialog();
            if (OpDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = OpDlg.FileName;
                sem_net = new SemantincNetwork();
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    sem_net = (SemantincNetwork)formatter.Deserialize(fs);
                    MessageBox.Show("Файл загружен");
                }
            }
        }
    }
}
