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

        public List<Link> find_links(Node nd1, Node nd2, string link_name_in)//собираем все связи в которых есть во входящих нодах эта нода
        {//берем ноду и ищем у нее все выходныщие стрелки
            Node node1 = nd1;
            Node node2 = nd2;
            LinkTypes lt = new LinkTypes();
            lt.to_in = link_name_in;

            List<Link> The_link = new List<Link>();

            for (int i = 0; i < l_link.Count; i++)
                if (l_link[i].incoming == node1 & l_link[i].lt_names.to_in==lt.to_in)
                    //здесь при формировании зе_линков учесть дипер
                    if (l_link[i].deeper == false)
                        The_link.Add(l_link[i]);
            return The_link;
        }

//запр 1
        public bool find_connection(int number1, int number2, string link_name_in) // TODO: функция проверки: Есть ли между нодой 1 и нодой 2 связь типа ____
        {//берем две ноды смотрим связь между ними
            // цикл по всем линкам: выбираем линки к ноде 1
            // цикл по выбранным линкам: есть ли у этих линков в качстве второй ноды -- нода 2
            // рекурсионность
            Node node1 = l_node[number1]; // первый узел для проверки
            Node node2 = l_node[number2]; // второй узел для проверки
            LinkTypes lt = new LinkTypes();
            lt.to_in = link_name_in;
            bool result = false;

            List<Link> The_link = new List<Link>();            
            The_link = find_links(node1, node2, link_name_in);//передаем ноды и входящую стрелку

            for (int i=0;i<The_link.Count;i++)
            {//проверка, есть ли между нод1 и нод2 нужная нам связь
                if (result) break;
                if (The_link[i].outgoing == node2)
                {
                    result = true;
                    break;
                }
                else
                {//если не нашли идем глубже
                    The_link[i].deeper = true;                    
                    Node nodeX = The_link[i].outgoing;
                    result = this.find_connection(l_node.IndexOf(nodeX), number2, link_name_in);
                }
            }
            if (result)
            {//если получили результат обнуляем дип
                for (int i = 0; i < l_link.Count; i++)
                    l_link[i].deeper = false;
                return result;
            }
            else
            {
                //MessageBox.Show("Утверждение не верно.");
                return result;
            }
        }

        //---ЗАПРОС 2---
        public List<string> find_connected(int numNode1, string link_name, bool is_in)//запрос 2 - выбрать все узлы, связанные с данным узлом данным типом связи
        {
            List<string> connected = new List<string>();//список найденных нодов
            Node node1 = l_node[numNode1];

            List<Link> The_link = new List<Link>();

            for (int i = 0; i < l_link.Count; i++)
            {//проходим по списку всех линков, ищем линки подходящие нашему ноду и типу линка
                if (is_in && l_link[i].incoming == node1 && l_link[i].lt_names.to_in == link_name)
                    The_link.Add(l_link[i]);
                if (!is_in && l_link[i].outgoing == node1 && l_link[i].lt_names.to_out == link_name)
                    The_link.Add(l_link[i]);
            //}//получим вконце список подходящих линков, с которым будем дальше работать

            for (int i=0;i<The_link.Count;i++)//учитываем направление нашей стрелки
                if (is_in)//набираем концы всех линков по полученнному списку выше
                    connected.Add(The_link[i].outgoing.name);
                else
                    connected.Add(The_link[i].incoming.name);

            return connected;
        }

        //---ЗАПРОС 3---

        public string find_disease(string node_name, bool is_simple)//запрос 3
        {//по исмптому находим болезнь
            if (is_simple)//если простое, если это не болевое ощущение
            {
                //todo: найти линку с зеленым типом, outgoing == node_name
                for (int i =0; i<l_link.Count;i++)
                    if (l_link[i].lt_names.to_in == "имеет симптом" && l_link[i].outgoing.name == node_name)
                        return l_link[i].incoming.name;//ищем связь имеет симптом на конце которой наша болезнь
            }
            else
            {
                //ищем линку с черным типом, outgoing == node_name. outgoing -> incoming, поиск зеленых линков
                for (int i = 0; i < l_link.Count; i++)//ищем, какие именно номер узла
                    if (node_name == "Физнагрузки")
                    {//синяя связь
                        if (l_link[i].lt_names.to_in == "влияет на" && l_link[i].outgoing.name == node_name)
                            return find_node_name(i);
                    }
                    else
                    {//черная связь
                        if (l_link[i].lt_names.to_in == "а конкретно" && l_link[i].outgoing.name == node_name)
                            return find_node_name(i);
                    }
            }

            return "Это странно...";
        }
        //находим у болего ощущения конкретность=боль в руке и так далее, а потом как в прошлом методе ищем связь имеет сиптом
        private string find_node_name(int count_node)
        {
            string result = "";
            for (int j = 0; j < l_link.Count; j++)
                if (l_link[j].lt_names.to_in == "имеет симптом" && l_link[j].outgoing == l_link[count_node].incoming)
                    result = l_link[j].incoming.name;

            return result;
        }
    }
}
