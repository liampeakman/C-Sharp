using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp2
{

    public class Movie
    {
        //create static bst that stores the movies 
        static MovieCollection movieCollectionTree = new MovieCollection();
      
        //create strings to store staff inputs 
        private string Title;
        private string Starring;
        private string Director;
        private string Genre;
        private string Classification;
        private string Duration;
        private string ReleaseDate;
        private string NumberOfCopies;
        private string Popularity = "0"; //starting popularity for each movie / used to sort top 10

        public void AddMovie()
        {
            //ask for new movie info
            Console.Write("Enter the movie title: ");
            Title = Console.ReadLine();
            Console.Write("Enter the staring actor(s): ");
            Starring = Console.ReadLine();
            Console.Write("Enter the director(s): ");
            Director = Console.ReadLine();
            SelectGenre(); //run switch cases
            SelectClassification(); //run switch cases
            Console.Write("Enter the duration (minutes): ");
            Duration = Console.ReadLine();
            Console.Write("Enter the release date (year): ");
            ReleaseDate = Console.ReadLine();
            Console.Write("Enter the number of copies available: ");
            NumberOfCopies = Console.ReadLine();
        }
        public void SelectGenre()
        {
            //case for each type of genre
            Console.WriteLine("Select the genre:\n1. Drama\n2. Adventure\n3. Family\n4. Action\n5. Sci-Fi\n6. Comedy\n7. Thriller\n8. Other");
            Console.Write("Make selection (1-8): ");

            bool SwitchBreak = false;
            while (!SwitchBreak)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Genre = "Drama";
                        SwitchBreak = true;
                        break;
                    case "2":
                        Genre = "Adventure";
                        SwitchBreak = true;
                        break;
                    case "3":
                        Genre = "Family";
                        SwitchBreak = true;
                        break;

                    case "4":
                        Genre = "Action";
                        SwitchBreak = true;
                        break;
                    case "5":
                        Genre = "Sci-Fi";
                        SwitchBreak = true;
                        break;
                    case "6":
                        Genre = "Comedy";
                        SwitchBreak = true;
                        break;
                    case "7":
                        Genre = "Thriller";
                        SwitchBreak = true;
                        break;
                    case "8":
                        Genre = "Other";
                        SwitchBreak = true;
                        break;

                    default:
                        Console.WriteLine("Invalid selection");
                        Console.Write("\nPlease make a selection (1-8): ");
                        break;

                }

            }
        }
        public void SelectClassification()
        {            
            //case for each type of classification
            Console.WriteLine("Select the classification:\n1. General (G)\n2. Parental Guidance (PG)\n3. Mature (M15+)\n4. Mature Accompanied (MA15+)");
            Console.Write("Make selection (1-4): ");

            bool SwitchBreak = false;
            while (!SwitchBreak)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Classification = "General (G)";
                        SwitchBreak = true;
                        break;
                    case "2":
                        Classification = "Parental Guidance (PG)";
                        SwitchBreak = true;
                        break;
                    case "3":
                        Classification = "Mature (M15+)";
                        SwitchBreak = true;
                        break;

                    case "4":
                        Classification = "Mature Accompanied (MA15+)";
                        SwitchBreak = true;
                        break;                  
                    default:
                        Console.WriteLine("Invalid selection");
                        Console.Write("\nPlease make a selection (1-4): ");
                        break;

                }

            }
        }
        //insert into bst 
        public void AddToCollection()
        {
            //create array from the string inputs to store the movie
            string[] movie = new string[] { Title, Starring, Director, Genre, Classification, Duration, ReleaseDate, NumberOfCopies, Popularity };
            //insert new movie array into static bst to store and sort
            movieCollectionTree.InsertMovie(movie);

        }
        public static void RemoveMovie()
        {
            string movie; 
            //take user input and delete movie from static bst 
            Console.Write("Enter the movie title you wish to delete: ");
            movie = Console.ReadLine();
            movieCollectionTree.DeleteMovie(movie);

        }
        public static void DisplayAllMovies()
        {
            //display all movies currently stored in bst 
            Console.WriteLine("============ALL MOVIES==================");
            movieCollectionTree.DisplayMovies(movieCollectionTree.ReturnRoot());
            Console.WriteLine("========================================");
        }
        public static void BorrowMovie(string username)
        {
            //ask for user input
            Console.Write("Enter a movie you wish to borrow: ");
            string movie = Console.ReadLine();
            //check to see if already borrowing
            if (MemberCollection.CheckIfBorrowed(movie, username))
            {
                //if borrowing
                Console.WriteLine("You are already borrowing this movie");
            }
            //check to see if already borrowing 10 movies 
            else if (MemberCollection.CheckIfMax(username))
            {
                Console.WriteLine("You are already borrowing the max amount of movies");
            }
            //not already borrowing & hasn't borrowed 10 movies 
            else
            {
                //borrow movie from collection
                movieCollectionTree.BorrowMovie(movie, username);
            }
        
        }
        public static void ReturnMovie(string username)
        {
            //ask user input
            Console.Write("Enter the movie you wish to return: ");
            string movie = Console.ReadLine();
            //check to see if user currently has the movie
            if (MemberCollection.CheckIfBorrowed(movie, username))
            {
                //add movie back in collection
                movieCollectionTree.ReturnMovie(movie, username);
            }
            //user does not currently have the movie
            else
            {
                //display
                Console.WriteLine("You are not currently borrowing that movie");    
            }

        }
        public static void CurrentlyBorrowing(string username)
        {
            //display movies user is currently borrowing
            Console.WriteLine("=========CURRENTLY BORROWING============");
            for (int i = 0; i < 10; i++)
            {   
                //check user 
                if (username == MemberCollection.members[i, 5])
                {
                    for (int j = 0; j < 10; j++)
                    {
                        //display movie name
                        Console.WriteLine(MemberCollection.membersMovies[i, j]);

                    }
                }
            }
            Console.WriteLine("=======================================");

        }
        public static void DisplayMostPopular()
        {
            string[][] sortedArr = new string[11][]; //array to sort top 10 / enough for 11 movies 

            //display the top 10 movies based on their popularity 
            Console.WriteLine("============TOP 10 MOVIES===============");
            //makes array with all the movies from the bst with a starting count 0
            movieCollectionTree.MakeArray(movieCollectionTree.ReturnRoot(), 0, sortedArr);
            movieCollectionTree.QuickSort(sortedArr, 0, 10); // set to current amount of movies 

            for (int k = 0; k < 10; k++)
            {
                if (k != 10)
                {
                    Console.WriteLine("Title: " + sortedArr[k][0]);
                    Console.WriteLine("Times Borrowed: " + sortedArr[k][1]);
                    Console.WriteLine("----------------------");
                }
            }
            //sorts the array from most popular to least and displays
            // movieCollectionTree.MostPopular(sortedArr);
            Console.WriteLine("========================================");


        }
        public static void PreLoadedMovies()
        {
            //makes preloaded movies  and adds them to the bst 
            string[] pulpFiction = new string[] { "Pulp Fiction", "John Travolta, Samuel L. Jackson", "Quentin Tarantino", "Drama", "Mature Accompanied (MA15+)", "154", "1994", "3", "2" };
            string[] getOut = new string[] { "Get Out", "Daniel Kaluuya, Allison Williams", "Jordan Peele", "Thriller", "Mature Accompanied (MA15+)", "94", "2017", "5", "3" };
            string[] parasite = new string[] { "Parasite", "Cho Yeo-jeong, Park So-dam", "Bong Joon-ho", "Thriller", "Mature Accompanied (MA15+)", "132", "2019", "10", "20" };
            string[] okja = new string[] { "Okja", "Tilda Swinton, Paul Dano", "Bong Joon-ho", "Drama", "Mature (M15+)", "120", "2017", "3", "2" };
            string[] jurassicWorld = new string[] { "Jurassic World", "Chriss Pratt, Bryce Dallas Howard", "Colin Trevorrow", "Adventure", "Mature (M15+)", "124", "2015", "6", "1" };
            string[] aQuietPlace = new string[] { "A Quiet Place", "John Krasinki, Emily Blunt", "John Krasinki", "Sci-Fi", "Mature Accompanied (MA15+)", "216", "2018", "3", "5" };
            string[] avengersInfinityWar = new string[] { "Avengers: Infinity War", "Tom Holland, Robert Downey Jr.", "Joe Russo, Anothony Russo", "Sci-Fi", "Mature (M15+)", "160", "2018", "4", "9" };
            string[] avengersEndGame = new string[] { "Avengers: End Game", "Tom Holland, Robert Downey Jr.", "Joe Russo, Anothony Russo", "Sci-Fi", "Mature (M15+)", "182", "2019", "8", "17" };
            string[] uncutGems = new string[] { "Uncut Gems", "Adam Sandler", "Josh Safdie, Benny Safdie", "Other", "Mature Accompanied (MA15+)", "175", "2019", "3", "5" };
            string[] moonlight = new string[] { "Moonlight", "Trevante Rhodes, Ashton Sanders", "Barry Jenkins", "Drama", "Mature (M15+)", "115", "2016", "2", "8" };
            string[] toyStory4 = new string[] { "Toy Story 4", "Keanu Reeves, Tom Hanks", "Josh Cooley", "Family", "General (G)", "100", "2019", "8", "12" };

            movieCollectionTree.InsertMovie(pulpFiction);
            movieCollectionTree.InsertMovie(getOut);
            movieCollectionTree.InsertMovie(parasite);
            movieCollectionTree.InsertMovie(okja);
            movieCollectionTree.InsertMovie(jurassicWorld);
            movieCollectionTree.InsertMovie(aQuietPlace);
            movieCollectionTree.InsertMovie(avengersInfinityWar);
            movieCollectionTree.InsertMovie(avengersEndGame);
            movieCollectionTree.InsertMovie(uncutGems);
            movieCollectionTree.InsertMovie(moonlight);
            movieCollectionTree.InsertMovie(toyStory4);

        }
        public Movie() //called when adding new movie
        {
            AddMovie(); //create the movie 
            AddToCollection(); //add it to the bst 
            Console.WriteLine("Successfully added "+Title+" to movie collection"); //display success
        }
    }
}
