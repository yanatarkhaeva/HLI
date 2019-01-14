using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void yavn_winner_test()
        {
            //arrange
            int [,] arr = new int[4, 5] {   { 1, 2, 3, 4, 5 },//матрица оценок от экспертов
                                       { 2, 1, 3, 5, 4 },
                                       { 3, 1, 2, 4, 5 },
                                       { 2, 3, 4, 5, 1 }};
            int[] main_result = { 12, 13, 8, 2, 5 };

            //act
            WindowsFormsApplication20.solution solv = new WindowsFormsApplication20.solution();
            int[] result = solv.yavn_winner(arr);

            //assert
            if (main_result.Length == result.Length)
            {
                for (int i = 0;i<main_result.Length;i++)
                {
                    Assert.AreEqual(main_result[i], result[i]);
                }
            }
        }

        [TestMethod]
        public void kopland_test()
        {
            //arrange
            int[,] arr = new int[4, 5] {   { 1, 2, 3, 4, 5 },//матрица оценок от экспертов
                                       { 2, 1, 3, 5, 4 },
                                       { 3, 1, 2, 4, 5 },
                                       { 2, 3, 4, 5, 1 }};
            int[] main_result = { 8, 10, 0, -12, -6 };

            //act
            WindowsFormsApplication20.solution solv = new WindowsFormsApplication20.solution();
            int[] result = solv.kopland(arr);

            //assert
            if (main_result.Length == result.Length)
            {
                for (int i = 0; i < main_result.Length; i++)
                {
                    Assert.AreEqual(main_result[i], result[i]);
                }
            }
        }
    }
}
