using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UserApp
{
    public static class DataWorker
    {
        public static void WriteDataInFile(string filePath, params object[] data)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    sw.WriteLine(data[i]);
                }
            }
        }

        public static void ReadDataFromFile(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string userData = "";
                while ((userData = sr.ReadLine()) != null)
                {
                    Console.WriteLine(userData);
                }
            }
        }
    }
}
