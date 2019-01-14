        public int[] simpson(int[,] arr)//�������
        {
            int n = arr.GetLength(0); //��������
            int m = arr.GetLength(1); //������������
            int[,] dop = new int[m, m];
            for (int k = 0; k < n; k++)//��� ������� ��������
                for (int i = 0; i < m; i++) //���������� �� �������
                    for (int j = 0; j < m; j++) //������� � ������� ����������
                    {
                        if (i != j) //�� ���������� � ����� �����
                        {
                            if (arr[k, i] < arr[k, j]) //���� ������� ������� �������� �������� ��� ���������, ��
                            {
                                dop[i, j] = dop[i, j] + 1;
                            }
                        }
                        else dop[i, j] = 1000;
                    }
            int[] min = new int[m];
            int minv = dop[0,0];
            //������� ����������� �� �������, ��������� � ������������ �� �in
            for (int i = 0; i < dop.GetLength(0); i++)
            {                
                for (int j = 0; j < dop.GetLength(1); j++)
                    if (dop[i, j] < minv && dop[i, j] != 0)
                        minv = dop[i, j];
                min[i] = minv;
            }
            return min;
        }