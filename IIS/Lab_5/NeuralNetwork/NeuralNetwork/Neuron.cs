using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    class Neuron
    {
        Random random = new Random();
        public Random Rnd
        {
            get { return random; }
        }

        // Цифры (Обучающая выборка)
        string num0 = "111101101101111";
        string num1 = "001001001001001";
        string num2 = "111001111100111";
        string num3 = "111001111001111";
        string num4 = "101101111001001";
        string num5 = "111100111001111";
        string num6 = "111100111101111";
        string num7 = "111001001001001";
        string num8 = "111101111101111";
        string num9 = "111101111001111";

        // Список всех вышеуказанных цифр
        List<string> nums = new List<string>();
        public List<string> Nums
        {
            get { return nums; }
        }

        // Виды цифры 5 (Тестовая выборка)
        string num51 = "111100111000111";
        string num52 = "111100010001111";
        string num53 = "111100011001111";
        string num54 = "110100111001111";
        string num55 = "110100111001011";
        string num56 = "111100101001111";

        List<string> nums5 = new List<string>();
        public List<string> Nums5
        {
            get { return nums5; }
        }

        //массив с весами сети
        int[] weights = new int[15];
        public int[] Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        // Порог функции активации
        int bias = 7;
        public int Bias
        {
            get { return bias; }
            set { bias = value; }
        }

        public Neuron()
        {
            nums.Add(num0);
            nums.Add(num1);
            nums.Add(num2);
            nums.Add(num3);
            nums.Add(num4);
            nums.Add(num5);
            nums.Add(num6);
            nums.Add(num7);
            nums.Add(num8);
            nums.Add(num9);

            for (int i = 0; i < weights.Length; i++)
                weights[i] = 0;

            nums5.Add(num51);
            nums5.Add(num52);
            nums5.Add(num53);
            nums5.Add(num54);
            nums5.Add(num55);
            nums5.Add(num56);
        }

        // Является ли данное число 5
        public bool proceed(string number)
        {
            // Рассчитываем взвешенную сумму
            int net = 0;
            for (int i = 0; i < weights.Length; i++)
                net += (int)char.GetNumericValue(number[i]) * weights[i];

            // Превышен ли порог? (Да - сеть думает, что это 5. Нет - сеть думает, что это другая цифра)
            if (net >= bias)
                return true;
            else return false;
        }

        //Уменьшение значений весов, если сеть ошиблась и выдала 1
        public void decrease(string number)
        {
            for (int i = 0; i < weights.Length; i++)
                if ((int)char.GetNumericValue(number[i]) == 1)//Возбужденный ли вход
                    weights[i] -= 1;//Уменьшаем связанный с ним вес на единицу
        }

        //Увеличение значений весов, если сеть ошиблась и выдала 0
        public void increase(string number)
        {
            for (int i = 0; i < weights.Length; i++)
                if ((int)char.GetNumericValue(number[i]) == 1)//Возбужденный ли вход
                    weights[i] += 1;//Увеличиваем связанный с ним вес на единицу
        }

        //Тренировка сети
        public void train()
        {
            for (int i = 0; i < 10000; i++)
            {
                //Генерируем случайное число от 0 до 9
                int option = Rnd.Next(0, 9);

                if (option != 5)//Если получилось НЕ число 5
                {
                    if (proceed(Nums[option]))//Если сеть выдала True/Да/1, то наказываем ее
                        decrease(Nums[option]);
                }
                else //Если получилось число 5
                {
                    if (!proceed(Nums[option]))//Если сеть выдала False/Нет/0, то показываем, что эта цифра - то, что нам нужно
                        increase(Nums[option]);
                }
            }

            //for (int i = 0; i < 100000; i++)
            //{
            //    int option = Rnd.Next(0, 3);

            //    switch (option)
            //    {
            //        case 0:
            //            if (proceed(Nums[option]))//Если сеть выдала True/Да/1, то хвалим ее
            //                decrease(Nums[option]);
            //            else
            //                increase(Nums[option]);
            //            break;
            //        case 1:
            //            if (proceed(Nums[option]))//Если сеть выдала True/Да/1, то хвалим ее
            //                decrease(Nums[option]);
            //            else
            //                increase(Nums[option]);
            //            break;
            //        case 2:
            //            if (proceed(Nums[option]))//Если сеть выдала True/Да/1, то хвалим ее
            //                decrease(Nums[option]);
            //            else
            //                increase(Nums[option]);
            //            break;
            //        case 3:
            //            if (proceed(Nums[option]))//Если сеть выдала True/Да/1, то хвалим ее
            //                decrease(Nums[option]);
            //            else
            //                increase(Nums[option]);
            //            break;
            //    }
            //}
        }
    }
}
