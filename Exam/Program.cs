using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Net.Http;

namespace Exam
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[] words = new int[10];
            int[] posts = new int[10];
            
            Console.Write("Loading data");
            
            for (int i = 1; i <= 100; i++)
            {
                HttpClient hc = new HttpClient();
                string s = hc.GetStringAsync("http://jsonplaceholder.typicode.com/posts/" + i).Result;
                
                int j = s.IndexOf("\"userId\": ");
                int k = s.IndexOf(",", j + 10);
                int id = int.Parse(s.Substring(j + 10, k - (j + 10)));
                
                j = s.IndexOf("\"body\": ");
                k = s.IndexOf("\"", j + 9);
                string body = s.Substring(j + 9, k - (j + 9));

                int w = 0;
                j = -1;
                while (true)
                {
                    j = body.IndexOf(" ", j + 1);
                    w++;
                    if (j == -1)
                        break;
                }
                words[id - 1] += w;
                posts[id - 1]++;
                
                Console.Write(".");
            }

            Console.WriteLine();
            
            for (int i = 1; i <= 10; i++)
            {
                HttpClient hc = new HttpClient();
                string s = hc.GetStringAsync("http://jsonplaceholder.typicode.com/users/" + i).Result;
                
                int j = s.IndexOf("\"id\": ");
                int k = s.IndexOf(",", j + 6);
                int id = int.Parse(s.Substring(j + 6, k - (j + 6)));
                
                j = s.IndexOf("\"name\": ");
                k = s.IndexOf("\"", j + 9);
                string name = s.Substring(j + 9, k - (j + 9));

                Console.WriteLine(name + ": " + (float)words[id - 1] / posts[id - 1]);
            }
        }
    }
}