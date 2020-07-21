using System;

/**
 * N10203036
 * LIAM PEAKMAN
 */

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //load default users and movies
            Member.PreLoadedUsers();
            Movie.PreLoadedMovies();

            //run main menu
            MainMenu();

            Console.ReadLine();
        }
        public static void MainMenu()
        {
            //clear window and display menu
            Console.Clear();
            Console.WriteLine("Welcome to the Community Library");
            Console.WriteLine("============Main Menu===========");
            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine("================================");
            Console.Write("\nPlease make a selection (1-2, or 0 to exit): ");
            //bool for invalid selection, repeats switch until valid selection
            bool SwitchBreak = false; 
            while (!SwitchBreak)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        StaffLogin(); //go to staff login
                        SwitchBreak = true;
                        break;

                    case "2":
                        MemberLogin();//go to member login
                        SwitchBreak = true;
                        break;

                    case "0":
                        Environment.Exit(0);//exit console
                        SwitchBreak = true;
                        break;

                    default: //continues while loop if invalid selection
                        Console.WriteLine("Invalid selection");
                        Console.Write("\nPlease make a selection (1-2, or 0 to exit): ");
                        break;

                }

            }
        }

        public static void StaffMenu()
        {
            //display staff menu
            Console.WriteLine("============Staff Menu==========");
            Console.WriteLine("1. Add a new movie DVD");
            Console.WriteLine("2. Remove a movie DVD");
            Console.WriteLine("3. Register a new Member");
            Console.WriteLine("4. Find a registered member's phone number");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("================================");
            Console.Write("\nPlease make a selection (1-4, or 0 to exit): ");
            //bool for invalid selection, repeats switch until valid selection
            bool SwitchBreak = false;
            while (!SwitchBreak)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        new Movie(); //runs movie class to add new movie
                        StaffMenu(); //return to staff menu when done
                        SwitchBreak = true;
                        break;
                    case "2":
                        Movie.RemoveMovie(); //removes movie from collection tree in movie class
                        StaffMenu();
                        SwitchBreak = true;
                        break;
                    case "3":
                        new Member(); //runs member class to add a new member
                        StaffMenu();
                        SwitchBreak = true;
                        break;
                    case "4":
                        MemberCollection.FindNumber(); //searches member collection array for users number
                        StaffMenu();
                        SwitchBreak = true;
                        break;

                    case "0":
                        MainMenu(); // exit to main menu
                        SwitchBreak = true;
                        break;

                    default:
                        Console.WriteLine("Invalid selection");
                        Console.Write("\nPlease make a selection (1-4, or 0 to exit): ");
                        break;

                }

            }
        }

        public static void MemberMenu()
        {
            //display member menu
            Console.WriteLine("===========Member Menu==========");
            Console.WriteLine("1. Display all movies");
            Console.WriteLine("2. Borrow a movie DVD");
            Console.WriteLine("3. Return a movie DVD");
            Console.WriteLine("4. List current borrowed movie DVDs");
            Console.WriteLine("5. Display top 10 most popular movies");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("================================");
            Console.Write("\nPlease make a selection (1-5, or 0 to exit): ");
            bool SwitchBreak = false;
            while (!SwitchBreak)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Movie.DisplayAllMovies(); //displays all movies stored in collection tree  
                        MemberMenu();
                        SwitchBreak = true;
                        break;

                    case "2":
                        Movie.BorrowMovie(InputUsername); //removes -1 of chosen movie from collection tree / stores under member movie array
                        MemberMenu();
                        SwitchBreak = true;
                        break;
                    case "3":
                        Movie.ReturnMovie(InputUsername); //adds +1 of borrowed movie from collection tree / removes from member movie array
                        MemberMenu();
                        SwitchBreak = true;
                        break;
                    case "4":
                        Movie.CurrentlyBorrowing(InputUsername); //checks what the user is currently borrowing from member movie array
                        MemberMenu();
                        SwitchBreak = true;
                        break;
                    case "5":
                        Movie.DisplayMostPopular(); //display list of movies stored in collection tree
                        MemberMenu();
                        SwitchBreak = true;
                        break;
                    case "0":
                        MainMenu(); //return to main menu
                        SwitchBreak = true;
                        break;

                    default:
                        Console.WriteLine("Invalid selection");
                        Console.Write("\nPlease make a selection (1-5, or 0 to exit): ");
                        break;

                }

            }
        }

        //strings for staff username & pw
        static string StaffUsername;
        static private string StaffPassword;
        public static void StaffLogin()
        {
            //default staff login info
            StaffUsername = "staff";
            StaffPassword = "today123";
            //ask for username & pw
            Console.Write("Enter username: ");
            string InputUsername = Console.ReadLine();
            Console.Write("Enter password: ");
            string InputPassword = Console.ReadLine();
            //check if input matches assigned username & pw
            if (InputUsername == StaffUsername && InputPassword == StaffPassword)
            {
                StaffMenu(); //if correct go to staff menu
            }
            else //incorrect
            {
                //ask to try again or exit
                Console.WriteLine("Username or password is incorrect.");
                Console.Write("Choose 1 to try again or 0 to exit: ");
                //loop until correct or exit
                bool SwitchBreak = false;
                while (!SwitchBreak)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            StaffLogin();
                            SwitchBreak = true;
                            break;

                        case "0":
                            MainMenu();
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

        //strings for user input username & pw
        static string InputUsername; //to be used when calling menu functions
        static private string InputPassword;
        public static void MemberLogin()
        {
            //ask for input
            Console.Write("Enter username: ");
            InputUsername = Console.ReadLine();
            Console.Write("Enter password: ");
            InputPassword = Console.ReadLine();
            //check member collection array to find if username & pw matches 
            bool Incorrect;
            for (int i = 0; i < 9; i++) 
            {
                if (InputUsername==MemberCollection.members[i, 5] && InputPassword==MemberCollection.members[i, 4])
                {
                    MemberMenu(); //if correct run member menu
                }
             }Incorrect = true;
            //use if statement and bool to work as an else 
            if (Incorrect) //if incorrect try again or exit
            {
                Console.WriteLine("Username or password is incorrect.");
                Console.Write("Choose 1 to try again or 0 to exit: ");

                bool SwitchBreak = false;
                while (!SwitchBreak)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            MemberLogin();
                            SwitchBreak = true;
                            break;

                        case "0":
                            MainMenu();
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
    }
}


