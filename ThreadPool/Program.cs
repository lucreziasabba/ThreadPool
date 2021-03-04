using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPool
{
    class Program
    {
        public static void Main(string[] args)
        {
            string names = @"C:\Users\Sabba Lucrezia\source\repos\threadpool\threadpool\bin\Debug\netcoreapp3.1\names.txt";

            StreamReader reader = new StreamReader(names);
            if (File.Exists(names))
            {
                List<string> listnames = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    listnames.Add(line);
                }

                Console.WriteLine("Name to search for :");
                string name = Console.ReadLine();
                
                Stopwatch mywatch = new Stopwatch();
                Console.WriteLine("Esecuzione Thread Pool");

                mywatch.Start();
                UseThreadPool();
                mywatch.Stop();

                Console.WriteLine("ThreadPool time used: " + mywatch.ElapsedTicks.ToString());
                mywatch.Reset();

                Console.WriteLine("Esecuzione Thread");

                mywatch.Start();
                UseThread(name, listnames);
                mywatch.Stop();

                Console.WriteLine("Thread time used: " + mywatch.ElapsedTicks.ToString());
                Console.Read();

                static void UseThread(string n, List<string> ns)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Thread t = new Thread(() => Research(n, ns));
                        t.Start();
                    }
                }

                static void UseThreadPool()
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(Research()));
                    }
                }
                
            }
        }
        public static string Research(string n, List<string> ns)
        {
            for (int i = 0; i < ns.Count; i++)
            {
                if (n == ns[i])
                {
                    return $"' {n} ' is in position {i}.";
                }
            }
            return $"' {n} ' was found succesfully"; ;
        }
    }
}
