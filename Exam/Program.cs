using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

namespace Exam
{
    public class Program
    {

        //Quickly generated the JSON Classes from the API endpoints ussing http://json2csharp.com/

        //**************************************
        //Generic Structure for Post JSON Object

        //Post Class
        public class Post
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public string body { get; set; }
        }

        //**************************************
        //Generic Structures for User JSON Object
 
        //Geographical Lat/Lon Class (Associated with User)
        public class Geo
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }

        //Address Class (Associated with User)
        public class Address
        {
            public string street { get; set; }
            public string suite { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }
            public Geo geo { get; set; }
        }

        //Company Info Class (Associated with User)
        public class Company
        {
            public string name { get; set; }
            public string catchPhrase { get; set; }
            public string bs { get; set; }
        }
        
        //User Class
        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public Address address { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public Company company { get; set; }
        }


        //**************************************
        //Statistics Logging Element for our Users
        public class UserStats
        {
            public int userID = 0;
            public int posts = 0;
            public int words = 0;
        }

        //**************************************
        //Implementation of the post processing loop
        public static bool processStatistics(int postCount)
        {
            //Testing if values are within range
            if (postCount <= 0 || postCount > 100) return false;

            //Basic statistics collecting variable used for processing
            Dictionary<int, UserStats> userDict = new Dictionary<int, UserStats>();

            Console.Write("Loading data");

            try
            {
                //Looping over Posts to collect statistics
                for (int i = 1; i <= postCount; i++)
                {
                    //Fetching JSON Object, Deserializing & Cleaning up JSON Body Object
                    HttpClient hc = new HttpClient();
                    string jsonString = hc.GetStringAsync("http://jsonplaceholder.typicode.com/posts/" + i).Result;
                    Post post = JsonConvert.DeserializeObject<Post>(jsonString);
                    post.body = post.body.Replace("\n", " ");   //Replacing with spaces to ensure correct counts :)

                    //Checking if the user is already listed in the dictionary, including it if its not
                    if (!userDict.ContainsKey(post.userId))
                    {
                        UserStats newUser = new UserStats();
                        newUser.userID = post.userId;
                        userDict.Add(post.userId, newUser);
                    }

                    //Updating Statistics
                    userDict[post.userId].words += post.body.Split(' ').GetLength(0);
                    userDict[post.userId].posts++;
                    Console.Write(".");
                }
                Console.Write("\n");

                //Iterating over Users in our Dictionary and presenting statistics
                foreach (KeyValuePair<int, UserStats> entry in userDict)
                {
                    UserStats user = entry.Value;

                    //Checking for indexes out of range
                    if (user.userID < 0 || user.userID > 10) return false;

                    //Fetching JSON Object & Deserializing Request
                    HttpClient hc = new HttpClient();
                    string jsonString = hc.GetStringAsync("http://jsonplaceholder.typicode.com/users/" + user.userID).Result;
                    User json = JsonConvert.DeserializeObject<User>(jsonString);

                    //Writing statistics for user to Console
                    Console.WriteLine(json.name + ": " + (float)user.words / user.posts);
                }
            }
            catch (Exception exception)
            {
                //For the purpose of this test, web connection issues are all alike, all lead to premature stoppage
                Console.WriteLine("Caught Untreated Exception: " + exception.ToString());
                return false;
            }

            return true;
        }

        //**************************************
        //Main Code Loop
        public static void Main(string[] args)
        {
            //Creating and starting our worker thread
            Thread worker = new Thread(new ThreadStart(() => processStatistics(100) ));
            worker.Start();
        }
    }
}