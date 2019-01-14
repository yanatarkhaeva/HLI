using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    [Serializable]
    public class Node  // класс узла семантической сети
    {
        public string name = "";//хранит имя узла

        public Node(string Name)
        {
            name = Name;
        }
    }

    [Serializable]
    public struct LinkTypes
    {
        public string to_out;//выходящая=явлется родителем
        public string to_in;//входящая=является подклассом(в обратную сторону)
    }

    [Serializable]
    public class Link  // класс связи между узлами
    {
        public LinkTypes lt_names;  //имя связи в обе стороны
        public Node incoming;//входящий узел(из него выходит стрелка)
        public Node outgoing;//исходящий узел, тот на который стрелка направлена(выходит из стрелки)
        public bool deeper = false;//показывает надо ли идти глубже

        public Link(Node inc, Node outg)//конструктор, дура слепая
        {
            incoming = inc;
            outgoing = outg;
        }

        public void SetLinkTypes(string inc, string outg)//установка связей
        {
            lt_names.to_in = inc;
            lt_names.to_out = outg;
        }
                    
    }

    [Serializable]
    public class SemantincNetwork//класс самой семантичекой сети
    {
        public List<Node> l_node;//список узлов
        public List<Link> l_link;//список связей

        public SemantincNetwork()
        {
            l_node = new List<Node>();//инициализируеми добавляем в спиок
            l_link = new List<Link>();
            add_node();
        }

        public void add_node()//добавление узла
        {
            string[] names = new string[27]
            {
                "Сердечно-сосудистые заболевания, затрагивающие в основном сердце",
                "ИБС",
                "Болевые ощущения",
                "Область сердца",
                "Предынфарктное состояние",
                "Болевые ощущения",
                "Живот, горло, лопатка, рука",
                "Стенокардия",
                "Болевые ощущения",
                "Левое плечо, левая рука, шея",
                "Стабильная стенокардия",
                "Болевые ощущения",
                "Физнагрузки",
                "Нестабильная стенокардия",
                "Нарушение ритма",
                "Экстрасистолия",
                "Перебои в работе сердца",
                "Физнагрузки",
                "Синусовая брадикардия",
                "ЧСС низкое",
                "Тахикардия",
                "Пароксизмальная тахикардия",
                "Резкий скачок ЧСС до 140-240 ударов",
                "Синусовая тахикардия",
                "ЧСС высокое",
                "Синусовая аритмия",
                "ЧСС в норме"
            };
            for (int i = 0; i < names.Length; i++)
            {
                Node node = new Node(names[i]);
                l_node.Add(node);
            }
        }

        public void add_link(int number1, int number2, string link_name_in, string link_name_out)//добавление связей
        {
            Link link1 = new Link(l_node[number1], l_node[number2]);
            link1.SetLinkTypes(link_name_in, link_name_out);
            l_link.Add(link1);
        }
    }
}
