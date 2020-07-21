using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Member
    {
        //strings to store user inputs 
        private string FirstName;
        private string LastName;
        private string Address;
        private string PhoneNumber;
        private string Password;
        private string UserName;

        public void CreateMember()
        {   
            //ask for user info / store in strings 
            Console.Write("Enter member's first name: ");
            FirstName = Console.ReadLine();
            Console.Write("Enter member's last name: ");
            LastName = Console.ReadLine();
            Console.Write("Enter member's address: ");
            Address = Console.ReadLine();
            CreatePhoneNumber(); //add user phone number
        }
        public void CreatePhoneNumber()
        {
            Console.Write("Enter member's phone number (+61): ");
            PhoneNumber = Console.ReadLine();
            //convert to int & check if its numbers (int)
            bool isNumeric = int.TryParse(PhoneNumber, out _);

            if (isNumeric)
            {
                CreatePassword(); //if int create pw
            }
            else //if contains string try again or exit
            {
                Console.WriteLine("Invalid Phone Number");
                Console.Write("Choose 1 to try again or 0 to exit: ");

                bool SwitchBreak = false;
                while (!SwitchBreak)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CreatePhoneNumber();
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
        public void CreatePassword()
        {
            //ask for 4 digits
            Console.Write("Enter member's password (4 digits): ");
            Password = Console.ReadLine();
            //convert to int & check 
            int PasswordLength = Password.Length;
            bool isNumeric = int.TryParse(Password, out _);
            //check it is int and 4 digits long
            if (PasswordLength == 4 && isNumeric)
            {
                CreateUsername(); //if meets criteria make username
            }
            else //if longer than 4 digits or contains string try again or exit
            {
                Console.WriteLine("Invalid Password");
                Console.Write("Choose 1 to try again or 0 to exit: ");

                bool SwitchBreak = false;
                while (!SwitchBreak)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CreatePassword();
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
        public void CreateUsername()
        {
            //add firstname to end of lastname
            UserName = LastName + FirstName;
        }
        public static void PreLoadedUsers()  
        {
            //makes preloaded users and adds them to the array
            new MemberCollection("John", "Smith", "123 Real Street","434904117", "1234", "SmithJohn");
        }
        public Member() //called when adding new member 
        {
            CreateMember(); //create the member and gather the inputs
            new MemberCollection(FirstName, LastName, Address, PhoneNumber, Password, UserName); //use the inputs to add to array
        }
    }
}

