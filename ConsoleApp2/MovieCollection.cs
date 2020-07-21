using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    public class MovieCollection {

        public class Node
        {
            public string[] Data; //store movie array
            public Node Left; //left node
            public Node Right; //right node
  
        }
        public Node root; 
        public Node ReturnRoot()
        {
            return root;
        }
        public MovieCollection()
        {
            root = null;
        }
        //insert movie array into tree
        public void InsertMovie(string[] movie)
        {
            Node newNode = new Node(); //create new node
            newNode.Data = movie; //set data to array
          

            if (root == null)
                root = newNode;
            else
            {
                Node current = root;
                Node parent;
                while (true)
                {
                    parent = current;
                    //compare movie titles 
                    if (string.Compare(movie[0], current.Data[0]) == 1) // = 1
                    {
                        //move to left of tree
                        current = current.Left;
                        if (current == null)
                        {
                            parent.Left = newNode;
                            return;
                        }
                    }
                    else // = -1
                    {
                        //move to right of tree
                        current = current.Right;
                        if (current == null)
                        {
                            parent.Right = newNode;
                            return;
                        }
                    }
                }
            }
        }
        public void BorrowMovie(string movie, string username)
        {
            Node current = root; // search reference
            Node parent = null; // parent of ptr
            while ((current != null) && (movie.CompareTo(current.Data[0]) != 0))
            {
                parent = current;
                if (movie.CompareTo(current.Data[0]) == 1) // move to the left child of ptr
                    current = current.Left;
                else
                    current = current.Right;
            }
            // if the search was unsuccessful
            if (current == null)
            {
                Console.WriteLine("Movie is not in our library");
            }
            // if the search was successful
            else
            {
                // check to see if there are any copies
                if (Int16.Parse(current.Data[7]) >= 1)
                {
                    //for copies
                    int oldCop = Int16.Parse(current.Data[7]);
                    oldCop--; //convert to int and -1 copy
                    string newCop = oldCop.ToString();
                    current.Data[7] = newCop;

                    //for popularity 
                    int oldPop = Int16.Parse(current.Data[8]);
                    oldPop++; //convert to int and +1 popularity
                    string newPop = oldPop.ToString();
                    current.Data[8] = newPop;

                    //assign movie to user
                    MemberCollection.AssignMovie(movie, username);
                }
                // no copies available 
                else
                {
                    Console.WriteLine("Movie is out of stock");
                }

            }

        }
        public void ReturnMovie(string movie, string username)
        {
            Node current = root; // search reference
            Node parent = null; // parent of ptr
            while ((current != null) && (movie.CompareTo(current.Data[0]) != 0))
            {
                parent = current;
                if (movie.CompareTo(current.Data[0]) == 1) // move to the left child of ptr
                    current = current.Left;
                else
                    current = current.Right;
            }

            int oldCop = Int16.Parse(current.Data[7]);
            oldCop++; //convert to int and +1 copy
            string newCop = oldCop.ToString();
            current.Data[7] = newCop;

            //remove movie from user
            MemberCollection.RemoveMovie(movie, username);
        }
        public void DisplayMovies(Node Root)
        {
            //check tree has movies 
            if (Root != null)
            {
                //display inorder 
                DisplayMovies(Root.Left);
                Console.WriteLine("Title: "+ Root.Data[0]);
                Console.WriteLine("Actor(s): " + Root.Data[1]);
                Console.WriteLine("Director(s): " + Root.Data[2]);
                Console.WriteLine("Genre: " + Root.Data[3]);
                Console.WriteLine("Classification: " + Root.Data[4]);
                Console.WriteLine("Duration: " + Root.Data[5]+" minutes");
                Console.WriteLine("ReleaseDate: " + Root.Data[6]);
                Console.WriteLine("Copies Available: " + Root.Data[7]);
                Console.WriteLine("Times Borrowed: " + Root.Data[8]);
                Console.WriteLine("----------------------");
                DisplayMovies(Root.Right);
            }
        }
        //make array to sort for top 10
        public void MakeArray(Node Root, int pos, string[][] arr)
        {
            if (Root != null)
            {
                while (arr[pos] != null){
                    pos++;
                }
                arr[pos] = new string[] { Root.Data[0], Root.Data[8] };
                MakeArray(Root.Left, pos, arr);
                MakeArray(Root.Right, pos, arr);
            }

        }
        //use quicksort to sort top 10
        public void QuickSort(string[][] arr, int start, int end)
        {
            int i;
            if (start < end)
            {
                i = Partition(arr, start, end);

                QuickSort(arr, start, i - 1);
                QuickSort(arr, i + 1, end);
            }
        }

        private int Partition(string[][] arr, int start, int end)
        {
            string temp;
            int p = Int16.Parse(arr[end][1]);
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (Int16.Parse(arr[j][1]) >= p)
                {
                    i++;
                    temp = arr[i][1];
                    arr[i][1] = arr[j][1];
                    arr[j][1] = temp;

                    temp = arr[i][0];
                    arr[i][0] = arr[j][0];
                    arr[j][0] = temp;
                }
            }

            temp = arr[i + 1][1];
            arr[i + 1][1] = arr[end][1];
            arr[end][1] = temp;

            temp = arr[i + 1][0];
            arr[i + 1][0] = arr[end][0];
            arr[end][0] = temp;
            return i + 1;
        }

        public void DeleteMovie(string movie)
        {
            // search for item and its parent
            Node current = root; // search reference
            Node parent = null; // parent of ptr
            while ((current != null) && (movie.CompareTo(current.Data[0]) != 0))
            {
                parent = current;
                if (movie.CompareTo(current.Data[0]) == 1) // move to the left child of ptr
                    current = current.Left;
                else
                    current = current.Right;
            }

            if (current != null) // if the search was successful
            {
                // case 3: item has two children
                if ((current.Left != null) && (current.Right != null))
                {
                    // find the right-most node in left subtree of ptr
                    if (current.Left.Right == null) // a special case: the right subtree of ptr.LChild is empty
                    {
                        current.Data = current.Left.Data;
                        current.Left = current.Left.Left;
                    }
                    else
                    {
                        Node p = current.Left;
                        Node pp = current; // parent of p
                        while (p.Right != null)
                        {
                            pp = p;
                            p = p.Right;
                        }
                        // copy the item at p to ptr
                        current.Data = p.Data;
                        pp.Right = p.Left;
                    }
                }
                else // cases 1 & 2: item has no or only one child
                {
                    Node c;
                    if (current.Left != null)
                        c = current.Left;
                    else
                        c = current.Right;

                    // remove node ptr
                    if (current == root) //need to change root
                        root = c;
                    else
                    {
                        if (current == parent.Left)
                            parent.Left = c;
                        else
                            parent.Right = c;
                    }
                }
                Console.WriteLine("Successfully removed " + movie + " from movie collection");
                }
            else
            {
                Console.WriteLine("Cannot find " + movie + " in movie collection");
            }
        }
    }
}

