using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp2
{
    class MemberCollection
    {   
        //create members array (enough for 10 members) / static so it doesnt override 
        public static string[,] members = new string[10,6]; //stores members
        public static string[,] membersMovies = new string[10, 10]; //stores members borrowed movies

        static int count = 0; //static count for new members

        public MemberCollection(string firstname, string lastname, string address, string phonenumber, string password, string username)
        {
            //parameters contain inputs from new member 
            //add them to the array
            members[count,0] = firstname;
            members[count,1] = lastname;
            members[count, 2] = address;
            members[count,3] = phonenumber;
            members[count,4] = password;
            members[count,5] = username;
            //display new member info
            Console.WriteLine("=======================================");
            Console.WriteLine("Successfully added " + members[count, 0] + " " + members[count, 1]);
            Console.WriteLine("Lives At " + members[count, 2]);
            Console.WriteLine("Number is (+61)" + members[count, 3]);
            Console.WriteLine("Username = " + members[count, 5]);
            Console.WriteLine("Password = ****");
            Console.WriteLine("=======================================");
            count++; //add count for next member
        }
        //Find any registered users phone number
        public static void FindNumber()
        {
            //ask for username of desired member
            Console.Write("Enter username: ");
            string InputUsername = Console.ReadLine();
            //search memberCollection array for member 
            bool Incorrect;
            for (int i = 0; i < 10; i++) //loop for 10 potential users
            {   
                if (InputUsername == MemberCollection.members[i, 5])
                {
                    //if correct display members number 
                    Console.WriteLine("=======================================");
                    Console.WriteLine("Users number is (+61) " + members[i, 3]);
                    Console.WriteLine("=======================================");
                    return;
                }
            }
            Incorrect = true;

            if (Incorrect) //else cannot find user try again or exit
            {
                Console.WriteLine("Cannot find username");
                Console.Write("Choose 1 to try again or 0 to exit: ");

                bool SwitchBreak = false;
                while (!SwitchBreak)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            FindNumber();
                            SwitchBreak = true;
                            break;

                        case "0":
                            Program.StaffMenu();
                            SwitchBreak = true;
                            break;

                        default:
                            Console.WriteLine("Invalid selection");
                            Console.Write("\nPlease make a selection (1 or 0): ");
                            break;

                    }
                }
            }

        }
        //check if the user has already borrowed that movie to avoid them from borrowing again / called from movieCollection tree 
        public static bool CheckIfBorrowed(string movie, string username)
        {   //search for user in members array and then search for movie and memberMovies array
            for (int i = 0; i < 10; i++)
            {   
                if (username == MemberCollection.members[i, 5])
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (movie == MemberCollection.membersMovies[i, j])
                        {
                            return true; //user has the movie already
                        }
                    }
                }
            } return false; //user does not have the movie
        }
        public static bool CheckIfMax(string username)
        {   //search for user in members array and then check to see if they have borrowed 10 movies
            for (int i = 0; i < 10; i++)
            {
                if (username == MemberCollection.members[i, 5])
                {
                    if (MemberCollection.membersMovies[i, 9] != null)
                    {
                        return true; //user has borrowed 10 movies
                    }
                   
                }
            }
            return false; //user does have max movies
        }

        //asign the borrowed movie to the user / called from movieCollection tree 
        public static void AssignMovie(string movie, string username)
        {
            for (int i = 0; i < 10; i++)
            {   //find user and member array
                if (username == MemberCollection.members[i, 5])
                {
                    for (int j = 0; j < 10; j++)
                    {   //look for next spot in member movie array
                        if (MemberCollection.membersMovies[i, j] == null)
                        {
                            //if space is free asign movie to that position
                            MemberCollection.membersMovies[i, j] = movie;
                            //display member and borrowed movie
                            Console.WriteLine("User " + MemberCollection.members[i, 5] + " Successfully borrowed " + MemberCollection.membersMovies[i, j]);
                            return; //end for-loop
                        }
                    }
                }
            }
        }
        //remove movie from user when returning / called from movieCollection tree 
        public static void RemoveMovie(string movie, string username)
        {
            for (int i = 0; i < 10; i++)
            {
                if (username == MemberCollection.members[i, 5])
                {
                    for (int j = 0; j < 10; j++)
                    { //looks for movie in member movie array
                        if (MemberCollection.membersMovies[i, j] == movie)
                        {
                            //asign it null value 
                            MemberCollection.membersMovies[i, j] = null;
                            //display member and returned movie
                            Console.WriteLine("User " + MemberCollection.members[i, 5] + " Successfully returned " + movie);
                            return; //end for-loop
                        }
                    }
                }
            }
        }

    }
}
