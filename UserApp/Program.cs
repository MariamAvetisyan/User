using System;
using System.IO;

namespace UserApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 140;
            // string fileName = @"C:\Users\mariam.avetisyan\Desktop\new one\User.txt";
            string fileName = @"..\..\..\..\Users.xml";
            object command;
            int retryCount = 3;

            while (true)
            {
                if (!File.Exists(fileName))
                {
                    XmlWorker.CreateDocument(fileName);
                }

                Console.WriteLine($"Please type the command. \n Options: '{Commands.AddUser}'  '{Commands.RemoveUser}' '{Commands.EditUser} " +
                                  $"'{Commands.ShowUser}' '{Commands.ShowAllUsers}' '{Commands.RemoveAllUsers}'. If you want to quite type '{Commands.Quite}'.");

                bool isValidCommand = Enum.TryParse(typeof(Commands), Console.ReadLine(), out command);

                if (!isValidCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please provide valid command");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if ((Commands)command == Commands.AddUser)
                {
                    #region user input

                    User myUser = new User();
                    Console.WriteLine("Please enter your first name");
                    myUser.FirstName = Console.ReadLine();
                    Console.WriteLine("Please enter your last name");
                    myUser.Lastname = Console.ReadLine();
                    Console.WriteLine("Please enter your email address");
                    myUser.EmailAddress = Console.ReadLine();
                    Console.WriteLine("Please enter your phone number");
                    while (!int.TryParse(Console.ReadLine(), out myUser.PhoneNumber))
                    {
                        Console.WriteLine($"Please provide valid {UserProperties.PhoneNumber}");
                    }

                    #endregion

                    XmlWorker.LoadDocument(fileName);
                    myUser.UserId = XmlWorker.GetLastUserId(fileName) + 1;
                    XmlWorker.AppendToParentNode(XmlWorker.CreateUserNode(myUser));
                    XmlWorker.SaveDocument(fileName);
                }
                else if ((Commands)command == Commands.RemoveUser)
                {
                    int id;

                    for (int i = 0; i < retryCount; i++)
                    {
                        Console.WriteLine("Please provide valid id");

                        if (int.TryParse(Console.ReadLine(), out id) && XmlWorker.IsValidId(fileName, id))
                        {
                            XmlWorker.LoadDocument(fileName);
                            XmlWorker.RemoveUserById(id);
                            XmlWorker.SaveDocument(fileName);

                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"User with {id} Id is removed");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    }
                }
                else if ((Commands)command == Commands.EditUser)
                {
                    int id;
                    object element;
                    for (int i = 0; i < retryCount; i++)
                    {
                        Console.WriteLine("Please provide valid User ID");

                        if (int.TryParse(Console.ReadLine(), out id) && XmlWorker.IsValidId(fileName, id))
                        {
                            for (int j = 0; j < retryCount; j++)
                            {
                                Console.WriteLine($"Please provide the element to be modified. \n Options: {UserProperties.FirstName} " +
                                                    $"{UserProperties.LastName} {UserProperties.EmailAddress} {UserProperties.PhoneNumber}.");

                                if (Enum.TryParse(typeof(UserProperties), Console.ReadLine(), out element))
                                {
                                    Console.WriteLine("Please provide corresponding value");

                                    string elementValue = Console.ReadLine();
                                    XmlWorker.LoadDocument(fileName);
                                    XmlWorker.EditUserById(id, (UserProperties)element, elementValue);
                                    XmlWorker.SaveDocument(fileName);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                else if ((Commands)command == Commands.ShowUser)
                {
                    int id;

                    for (int i = 0; i < retryCount; i++)
                    {
                        Console.WriteLine("Please provide valid ID");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            XmlWorker.ShowUserById(fileName, id);
                            break;
                        }
                    }
                }
                else if ((Commands)command == Commands.ShowAllUsers)
                {
                    XmlWorker.ShowAllUsers(fileName);
                }
                else if ((Commands)command == Commands.RemoveAllUsers)
                {
                    XmlWorker.LoadDocument(fileName);
                    XmlWorker.RemoveAllUsers();
                    XmlWorker.SaveDocument(fileName);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"All users are removed");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if ((Commands)command == Commands.Quite)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(new String('*', 122));
                Console.ForegroundColor = ConsoleColor.White;

            }

            Console.WriteLine("Thank you. Bye");

            #region Comment

            /*
            DataWorker.WriteDataToFile(fileName, myUser.FirstName, myUser.Lastname,
                                        myUser.EmailAddress, myUser.PhoneNumber);

            Console.WriteLine($"Dear {myUser.FirstName} {myUser.Lastname} your data was successfully saved. \n" +
                              $"If you want to display your information please type 'Data'");

            if (Console.ReadLine() == "Data")
                DataWorker.ReadDataFromFile(fileName);
            */
            #endregion

        }
    }
}
