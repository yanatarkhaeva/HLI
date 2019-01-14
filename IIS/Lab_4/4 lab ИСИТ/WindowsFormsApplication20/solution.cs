using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication20
{
    public class solution
    {
        public int[] yavn_winner(int[,] arr) //явный победитель
        {
            int n = arr.GetLength(0);//количество экспертов
            int m = arr.GetLength(1);//количество альтернатив
            int[] result = new int[m];//оценки по каждой альтернативе

            for (int k = 0; k < n; k++)//для каждого эксперта
                for (int i = 0; i < m; i++) //берем оценку
                    for (int j = 0; j < m; j++) //сравниваем ее со всеми оценками этого эксперта по другим альтернативам
                        if (arr[k, i] < arr[k, j] && i != j) //если главный элемент значимее элемента для сравнения, то
                            result[i] = result[i] + 1; //в таблицу результатов добавляем +1
            return result;
        }

        public int[] kopland(int[,] arr) //копланд
        {
            int n = arr.GetLength(0);//кол-во экспертов
            int m = arr.GetLength(1);//кол-во альтернатив
            int[] result = new int[m];//решение

            for (int k = 0; k < n; k++)//для каждого эксперта
                for (int i = 0; i < m; i++) //берем оценку
                    for (int j = 0; j < m; j++) //сравниваем ее со всеми оценками этого эксперта по другим альтернативам
                    {
                        if (arr[k, i] < arr[k, j] && i != j) //если главный элемент значимее элемента для сравнения, то
                            result[i] = result[i] + 1; //в таблицу результатов добавляем +1
                        else if (arr[k, i] > arr[k, j])
                            result[i] = result[i] - 1;//если оценка проигрывает, то -1
                    }
            return result;
        }

        public int[] simpson(int[,] arr)//симпсон
        {
            int n = arr.GetLength(0); //эксперты
            int m = arr.GetLength(1); //альтернативы
            int[,] dop = new int[m, m];
            for (int k = 0; k < n; k++)//для каждого эксперта
                for (int i = 0; i < m; i++) //проходимся по оценкам
                    for (int j = 0; j < m; j++) //элемент с которым сравниваем
                    {
                        if (i != j) //не сравнимаем с самим собой
                        {
                            if (arr[k, i] < arr[k, j]) //если главный элемент значимее элемента для сравнения, то
                            {
                                dop[i, j] = dop[i, j] + 1;
                            }
                        }
                        else dop[i, j] = 1000;
                    }
            int[] min = new int[m];
            int minv = dop[0,0];
            //выбрать минимальное по строкам, побеждает с максимальным из мin
            for (int i = 0; i < dop.GetLength(0); i++)
            {                
                for (int j = 0; j < dop.GetLength(1); j++)
                    if (dop[i, j] < minv && dop[i, j] != 0)
                        minv = dop[i, j];
                min[i] = minv;
            }
            return min;
        }

        public int[] board(int[,] arr)//борд
        {
            int n = arr.GetLength(0);//кол-воэкспертов
            int p = arr.GetLength(1);//количество альтернатив - кол-во мест
            int[] arr_ball = new int[p];//массив для подсчета баллов
            
            for (int k = 0; k < n; k++)
                for (int i = 0; i < p; i++) //проходимся по оценкам
                {
                    arr_ball[i] += p - arr[k, i];
                }
            return arr_ball;
        }
    }
}
