using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.Data;

namespace AlgorithmsAndDataStructures
{
    class Project4
    { 
        public static void Main()
        {
            int i = 0;
            int[] dataSet = null;
            int datasetSize = 0;
            string[,] bubbleData = new string[11, 10];
            string[,] quickData = new string[11, 10];
            for (i = 0; i < 11; i++)
            {
                //determines size of the dataset
                if (i == 0)
                {
                    datasetSize = 500;
                }
                else if (i == 1)
                {
                    datasetSize = 1000;
                }
                else if (i > 1)
                {
                    datasetSize += 1000;
                }

                for (int x = 0; x < 10; x++)
                {
                    //generates data
                    dataSet = generateData(datasetSize);
                    //bubblesort stopwatch
                    Stopwatch bubbleWatch = new Stopwatch();
                    //quicksort stopwatch
                    Stopwatch quickWatch = new Stopwatch();

                    //bubblesort
                    bubbleWatch.Start();
                    int[] dataSetB = bubbleSort(dataSet);
                    bubbleWatch.Stop();

                    Console.WriteLine("\nBubblesort");
                    //foreach (var item in dataSetB)
                    //{
                    //    Console.WriteLine(item.ToString());
                    //}
                    Console.WriteLine("Time elapsed: {0}", bubbleWatch.Elapsed);

                    //timer to string
                    TimeSpan bubWatch = bubbleWatch.Elapsed;
                    string bubWatchString = string.Format("{0:0}.{1:000000}", bubWatch.Seconds, bubWatch.Ticks / 10);
                    Console.WriteLine(bubWatchString);

                        //quicksort
                        int high = dataSet.Length - 1;
                        int low = 0;

                    quickWatch.Start();
                    int[] dataSetQ = quickSort(dataSet, low, high);
                    quickWatch.Stop();

                    Console.WriteLine("\nQuickSort");
                    //foreach (var item in dataSetQ)
                    //{
                    //    Console.WriteLine(item.ToString());
                    //}
                    Console.WriteLine("Time elapsed: {0}", quickWatch.Elapsed);

                    //timer to string
                    TimeSpan quiWatch = quickWatch.Elapsed;
                    string quiWatchString = string.Format("{0:0}.{1:000000}", quiWatch.Seconds, quiWatch.Ticks / 10);
                    Console.WriteLine(quiWatchString);

                    string datatext = "";
                    bubbleData[i, x] += bubWatchString;

                    quickData[i, x] += quiWatchString;
                }
            }
            //plotting data
            writeToFile(dataSet, i, bubbleData, quickData);
        }
        //Write a function that generates a dataset of n size. The dataset should be random integers.
        public static int[] generateData(int datasetSize)
        {
            //Console.Write("Please input what size you want the dataset to be: ");
            //int datasetSize = int.Parse(Console.ReadLine());
            //Console.WriteLine("\n");

            int[] dataSet = new int[datasetSize];
            Random rnd = new Random();
            for (int i = 0; i < datasetSize; i++)
            {
                //this generates 1 to 1000 as the 2nd value isn't inclusive
                int randNum = rnd.Next(1, 1001);
                dataSet[i] = randNum;
            }
            return dataSet;
        }
        public class tableBQ
        {
            public int Data_Size { get; set; }
            public int Bubble_Sort { get; set; }
            public int Quick_Sort { get; set; }
        }
        public static void writeToFile(int[] dataSet, int dataSize, string[,] bubbleData, string[,] quickData)
        {
            string filePathA = "C:\\Users\\charl\\OneDrive\\Desktop\\Term 1\\Algorithms and Data Structures\\Coursework\\Project 4\\dataValues.csv";
            string filePathB = "C:\\Users\\charl\\OneDrive\\Desktop\\Term 1\\Algorithms and Data Structures\\Coursework\\Project 4\\experimentResults.csv";

            string datatext = "";
            // enters all the different values in dataSet to an exel file
            using (StreamWriter writer1 = new StreamWriter(filePathA))
            {
                foreach (var item in dataSet)
                {
                    datatext += item.ToString() + ", ";
                }
                writer1.WriteLine(datatext);
            }

            string datatext1 = "";

            using (StreamWriter writer2 = new StreamWriter(filePathB))
            {
                writer2.WriteLine("Bubble Sort");
                for (int i = 0; i < bubbleData.GetLength(0); i++)
                {
                    datatext1 = "";
                    for (int x = 0; x < bubbleData.GetLength(1); x++)
                    {
                        datatext1 += bubbleData[i, x] + ", ";
                    }
                    writer2.WriteLine(datatext1);
                }

                datatext1 = "";
                writer2.WriteLine("\n");
                writer2.WriteLine("Quick Sort");
                for (int i = 0; i < quickData.GetLength(0); i++)
                {
                    datatext1 = "";
                    for (int x = 0; x < quickData.GetLength(1); x++)
                    {
                        datatext1 += quickData[i, x] + ", ";
                    }
                    writer2.WriteLine(datatext1);
                }
            }
        }

        //bubblesort
        public static int[] bubbleSort(int[] dataSet)
        {
            int n = dataSet.Length;
            int temp;
            bool noswap = false;
            while (noswap == false)
            {
                noswap = true;
                for (int i = 0; i < n - 1; i++)
                {
                    if (dataSet[i] > dataSet[i + 1])
                    {
                        temp = dataSet[i];
                        dataSet[i] = dataSet[i + 1];
                        dataSet[i + 1] = temp;
                        noswap = false;
                    }
                }
            }
            return dataSet;
        }

        //quicksort
        public static int[] quickSort(int[] dataSet, int low, int high)
        {
            if (low < high)
            {
                int pivot = partition(dataSet, low, high);
                quickSort(dataSet, low, pivot - 1);
                quickSort(dataSet, pivot + 1, high);
            }

            return dataSet;
        }
        public static int partition(int[] dataSet, int low, int high)
        {
            int pivot = dataSet[high];
            int i = low - 1;
            for (int j = low; j <= high - 1; j++)
            {
                if (dataSet[j] < pivot)
                {
                    i++;
                    dataSet = swap(dataSet, i, j);
                }
            }
            dataSet = swap(dataSet, i + 1, high);
            return i + 1;
        }
        public static int[] swap(int[] dataSet, int i, int j)
        {
            int temp = dataSet[i];
            dataSet[i] = dataSet[j];
            dataSet[j] = temp;

            return dataSet;
         }
    }
}