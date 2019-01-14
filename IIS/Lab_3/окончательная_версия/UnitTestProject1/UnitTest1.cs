using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_connection_test()
        {
            int number1 = 0;
            int number2 = 1;
            string link_name_in = "является родителем";
            bool main_result = true;
            BinaryFormatter formatter = new BinaryFormatter();

            WindowsFormsApplication1.SemantincNetwork sn = new WindowsFormsApplication1.SemantincNetwork();
            string fileName = @"C:\Users\asus\Desktop\4курс 1семестр\интеллектуальные ИСИТ\semanticheart\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\sem_net.txt";
            sn = new WindowsFormsApplication1.SemantincNetwork();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
                sn = (WindowsFormsApplication1.SemantincNetwork)formatter.Deserialize(fs);
            bool result = sn.find_connection(number1, number2, link_name_in);

            Assert.AreEqual(main_result, result);
        }

        [TestMethod]
        public void find_connected_test()
        {
            int number1 = 0;
            string link_name_in = "является родителем";
            bool is_in = true;
            List<string> main_result = new List<string>();
            main_result.Add("ИБС");
            main_result.Add("Нарушение ритма");
            BinaryFormatter formatter = new BinaryFormatter();

            WindowsFormsApplication1.SemantincNetwork sn = new WindowsFormsApplication1.SemantincNetwork();
            string fileName = @"C:\Users\asus\Desktop\4курс 1семестр\интеллектуальные ИСИТ\semanticheart\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\sem_net.txt";
            sn = new WindowsFormsApplication1.SemantincNetwork();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
                sn = (WindowsFormsApplication1.SemantincNetwork)formatter.Deserialize(fs);
            List<string> result = sn.find_connected(number1, link_name_in, is_in);

            if (main_result.Count == result.Count)
            {
                for (int i = 0; i < main_result.Count; i++)
                    Assert.AreEqual(main_result[i], result[i]);
            }
        }
    }
}
