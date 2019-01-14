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

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
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

        struct QueryInfo
        {
            public QueryInfo(int num1, int num2, string link_name)
            {
                numNode1 = num1;
                numNode2 = num2;
                linkNameIn = link_name;
            }
            public int numNode1;
            public int numNode2;
            public string linkNameIn;
        }

        public Form2()
        {
            sem_net = new SemantincNetwork();
            InitializeComponent();
            for (int i = 0; i < sem_net.l_node.Count; i++)
            {
                checkedListBox1.Items.Add(sem_net.l_node[i].name);
                checkedListBox2.Items.Add(sem_net.l_node[i].name);
                checkedListBox3.Items.Add(sem_net.l_node[i].name);
            }
            foreach (var pair in link_names)
            {
                comboBox1.Items.Add(pair.Key);
                comboBox1.Items.Add(pair.Value);
                comboBox2.Items.Add(pair.Key);
                comboBox2.Items.Add(pair.Value);
            }
            load_basa();
        }

        public void load_basa()//загрузка базы знаний по известному пути
        {
            string fileName = @"sem_net.txt";
            sem_net = new SemantincNetwork();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
                sem_net = (SemantincNetwork)formatter.Deserialize(fs);
        }

        private void загрузитьСемантСетьToolStripMenuItem_Click(object sender, EventArgs e)//загрузка из меню с диалогом
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

        private void button1_Click(object sender, EventArgs e)//запрос 1: берем две ноды, и связь междду ними. Верно ли утверждение?
        {
            int number1 = checkedListBox1.SelectedIndex;
            int number2 = checkedListBox2.SelectedIndex;
            string link_name = comboBox1.SelectedItem.ToString();
            QueryInfo queryInfo = UpOrDown(number1, number2, link_name);
            bool result = sem_net.find_connection(queryInfo.numNode1, queryInfo.numNode2, queryInfo.linkNameIn);
            if (result)
                MessageBox.Show("Утверждение верно.");
            else
            {
                for (int i = 0; i < sem_net.l_link.Count; i++)
                    sem_net.l_link[i].deeper = false;
                MessageBox.Show("Утверждение не верно.");
            }
        }

        private QueryInfo UpOrDown(int num1, int num2, string link_name)//метод для замены нод местами в зависимости от направления стрелки
        {
            QueryInfo queryInfo = new QueryInfo(0,0,"");
            if (link_names.ContainsKey(link_name))
            {
                queryInfo = new QueryInfo(num1, num2, link_name);
                return queryInfo;
            }
            else
                foreach (KeyValuePair<string, string> pair in link_names)
                    if (pair.Value == link_name)
                    {
                        queryInfo = new QueryInfo(num2, num1, pair.Key);
                        return queryInfo;
                    }
            return queryInfo;
        }

        private void button2_Click(object sender, EventArgs e)//запрос 2: по ноде и типу линка вывести связанные с ними ноды
        {
            listBox1.Items.Clear();
            int number1 = checkedListBox3.SelectedIndex;
            string link_name = comboBox2.SelectedItem.ToString();
            bool is_in = InOrOut(link_name);
            List<string> nodeNames = sem_net.find_connected(number1, link_name, is_in);        
            listBox1.Items.AddRange(nodeNames.ToArray());
        }

        private bool InOrOut(string link_name)//определение, входящая это линка или выходящая для выбранной ноды
        {
            bool result = false;//true - входит в выбранную ноду, false - выходит
            if (link_names.ContainsKey(link_name))
            {
                result = true;
                return result;
            }
            return result;
        }

        private void button3_Click(object sender, EventArgs e)//запрос 3: по симптому отпределять диагноз
        {
            string node_name = treeView1.SelectedNode.Text;

            bool is_simple = false;//определяем, где находится узел: в корне или это листик на ветке
            if (treeView1.SelectedNode.Level == 0)//если корень, то true
                is_simple = true;

            textBox1.Text = sem_net.find_disease(node_name, is_simple);
        }
    }
}
