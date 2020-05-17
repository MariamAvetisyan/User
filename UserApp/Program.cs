using System;

namespace UserApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\mariam.avetisyan\Desktop\new one\User.txt";
            User myUser = new User();

            Console.WriteLine("Please enter your first name");
            myUser.FirstName = Console.ReadLine();
            Console.WriteLine("Please enter your last name");
            myUser.Lastname = Console.ReadLine();
            Console.WriteLine("Please enter your email address");
            myUser.EmailAddress = Console.ReadLine();
            do
            {
                Console.WriteLine("Please enter your phone number");
                myUser.PhoneNumber = Console.ReadLine();

            } while (!myUser.IsNumber);

            DataWorker.WriteDataInFile(fileName, myUser.FirstName, myUser.Lastname,
                                        myUser.EmailAddress, myUser.PhoneNumber);

            Console.WriteLine($"Dear {myUser.FirstName} {myUser.Lastname} your data was successfully saved. \n" +
                              $"If you want to display your information please type 'Data'");

            if (Console.ReadLine() == "Data")
                DataWorker.ReadDataFromFile(fileName);

            Console.WriteLine("Thank you. Bye");
            Console.ReadLine();
        }
    }
}
