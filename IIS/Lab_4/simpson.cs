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