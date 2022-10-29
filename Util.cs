using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library_System
{
    class Util
    {
        //---------CODE ATTRIBUTION---------
        //The following method was from StackOverFlow
        //Authors : dtb
        //Link: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
        //---------END---------

        //Create instance of random object, used for making a random string
        private static readonly Random random = new Random();

        //Method used to create a random string of length 10
        public static string generateRandomString()
        {
            //generates the id for the book
            //Declares length
            const int length = 10;
            //Declares the characters that can be used
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //Makes the combined string
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string generateBookNumber()
        {
            //used to create the book number and book topic
            Random rnd = new Random();

            int randomNum = rnd.Next(999);

            return randomNum.ToString();
        }
        public static string generateBookTopic()
        {
            Random rnd = new Random();
            string topicNumber = "";
            int randomNum = rnd.Next(900);
            if (randomNum <= 10)
            {
                topicNumber = "00" + randomNum.ToString();
                return topicNumber;
            }
            if (randomNum > 10 && randomNum < 100)
            {
                topicNumber = "0" + randomNum.ToString();
                return topicNumber;
            }
            return randomNum.ToString();
        }
        public static string generateName()
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from StackOverFlow
            //Authors : Waqar Khan
            //Link: https://stackoverflow.com/questions/14687658/random-name-generator-in-c-sharp
            //---------END---------
            //crates random obj
            Random r = new Random();
            //declares arrays to craete name
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u"};
            string Name = "";
            //swaps between vowals amd consonants to make a name
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            Name += consonants[r.Next(consonants.Length)];
           

            return Name;


        }
        public static void writeNewUser(string username)
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from GeeksForGeeks
            //Authors : GeeksForGeeks
            //Link: https://www.geeksforgeeks.org/how-to-read-and-write-a-text-file-in-c-sharp/
            //---------END---------
            // Creating a file
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MyLibrary.txt");
            // Appending the given texts
            using (StreamWriter sw = File.AppendText(path))
            {
                //writes the user to the textfile and sets the current user 
                sw.WriteLine(username + ", " + "0");
                currentUser.username = username;
                currentUser.userPoints = 0;
            }
            //saves the position in the textfile
            currentUser.userPos = File.ReadAllLines(path).Length;
        }
        public static void checkIfUserExists(string username)
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from GeeksForGeeks
            //Authors : GeeksForGeeks
            //Link: https://www.geeksforgeeks.org/how-to-read-and-write-a-text-file-in-c-sharp/
            //---------END---------

            // Creating a file
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MyLibrary.txt");
            bool check = false;
            int counter = 0;
            // To read the entire file at once
            if (File.Exists(path))
            {
                // Read all the content in one string
                // and display the string
                StreamReader Textfile = new StreamReader(path);
                string line = "";
                //goes through the file till the last line
                while ((line = Textfile.ReadLine()) != null)
                {
                    //counts the number of lines
                    counter++;
                    //checks if the username is in the line
                    if (line.Contains(username))
                    {
                        //found is true
                        check = true;
                        currentUser.userPos = counter;
                        //set user info
                        currentUser.username = line.Substring(0, line.IndexOf(","));
                        currentUser.userPoints = Convert.ToInt32(line.Substring(line.IndexOf(",") + 1));
                    }

                }
                //closes the file
                Textfile.Close();
                //if not found makes a new user
                if (check == false)
                {
                    writeNewUser(username);
                }
            }
            else
            {
                MessageBox.Show("File not Found. Please try again");
            }
        }
        public static List<User> sortUserPoints()
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from GeeksForGeeks
            //Authors : GeeksForGeeks
            //Link: https://www.geeksforgeeks.org/how-to-read-and-write-a-text-file-in-c-sharp/
            //---------END---------

            // Creating a file
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MyLibrary.txt");
            List<User> userLists = new List<User>();
            // To read the entire file at once
            if (File.Exists(path))
            {
                // Read all the content in one string
                // and display the string
                StreamReader Textfile = new StreamReader(path);
                string line = "";
                

                while ((line = Textfile.ReadLine()) != null)
                {
                    string username = line.Substring(0, line.IndexOf(","));
                    int points = Convert.ToInt32(line.Substring(line.IndexOf(",") + 1));
                    User user = new User(username,points);
                    userLists.Add(user);
                }
                userLists = userLists.OrderByDescending(item => item.UserPoints).ThenBy(item=>item.Username).ToList();
                Textfile.Close();
                return userLists;
            }
            else
            {
                MessageBox.Show("File not Found. Please try again");
                return userLists;
            }
          
        }
        //method to add points to the current user
        public static void addPoints()
        {
            //---------CODE ATTRIBUTION---------
            //The following method was from GeeksForGeeks
            //Authors : GeeksForGeeks
            //Link: https://www.geeksforgeeks.org/how-to-read-and-write-a-text-file-in-c-sharp/
            //---------END---------

            // Creating a file
            var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MyLibrary.txt");
            //points to add
            int POINTS = 100;
            // To read the entire file at once
            if (File.Exists(path))
            {       
                //gets all the lines
                string[] arrLines = File.ReadAllLines(path);
                //adds points to the user
                int addedPoints = currentUser.userPoints + 100;
                //create the new line
                string newLine = currentUser.username + ", " + addedPoints;
                //writes the new line to the array
                arrLines[currentUser.userPos - 1] = newLine;
                //writes the new line to the textfile
                File.WriteAllLines(path, arrLines);
            }
            currentUser.userPoints += 100;
        }
    }
}

