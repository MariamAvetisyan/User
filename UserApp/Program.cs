﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UserApp.Common;
using UserApp.Repositories;

namespace UserApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 140;
            object command;
            int retryCount = 3;
            int id = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine($"Please type the command. \n Options: '{Commands.AddUser}'  '{Commands.RemoveUser}'" +
                                      $" '{Commands.EditUser} '{Commands.ShowUser}' '{Commands.ShowAllUsers}' '{Commands.RemoveAllUsers}'." +
                                      $" If you want to quite type '{Commands.Quite}'.");
                    IXmlWorker xmlWorker = new Common.XmlWorker(ConfigurationManager.AppSettings["xmlPath"]);
                    IUserRepository userRepository = new UserRepository(xmlWorker);

                    bool isValidCommand = Enum.TryParse(typeof(Commands), Console.ReadLine(), out command);

                    if (isValidCommand)
                    {
                        switch ((Commands)command)
                        {
                            case Commands.AddUser:
                                User myUser = new User();
                                Console.WriteLine("Please enter your first name");
                                myUser.FirstName = Console.ReadLine();
                                Console.WriteLine("Please enter your last name");
                                myUser.Lastname = Console.ReadLine();
                                Console.WriteLine("Please enter your email address");
                                myUser.EmailAddress = Console.ReadLine();
                                Console.WriteLine("Please enter your phone number");
                                myUser.PhoneNumber = Console.ReadLine();
                                userRepository.AddUser(myUser);
                                break;
                            case Commands.RemoveUser:
                                Console.WriteLine("Please enter User Id to be removed");
                                id = Convert.ToInt32(Console.ReadLine());
                                userRepository.RemoveUser(id);
                                break;
                            case Commands.ShowUser:
                                Console.WriteLine("Please enter User Id to show");
                                id = Convert.ToInt32(Console.ReadLine());
                                var myuser = userRepository.GetUser(id);
                                if (myuser == null)
                                {
                                    Console.WriteLine("User with given Id doesnt exsist");
                                }
                                Console.WriteLine(myuser);
                                break;
                            case Commands.EditUser:
                                object element;
                                for (int i = 0; i < retryCount; i++)
                                {
                                    Console.WriteLine("Please provide valid User ID");

                                    if (int.TryParse(Console.ReadLine(), out id) && (userRepository.GetUser(id) != null))
                                    {
                                        for (int j = 0; j < retryCount; j++)
                                        {
                                            Console.WriteLine($"Please provide the element to be modified. \n Options: {UserProperties.FirstName} " +
                                                                $"{UserProperties.LastName} {UserProperties.EmailAddress} {UserProperties.PhoneNumber}.");

                                            if (Enum.TryParse(typeof(UserProperties), Console.ReadLine(), out element))
                                            {
                                                Console.WriteLine("Please provide corresponding value");
                                                string elementValue = Console.ReadLine();
                                                userRepository.Update(userRepository.GetUser(id), (UserProperties)element, elementValue);
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            case Commands.ShowAllUsers:
                                var users = userRepository.GetAll();
                                foreach (var user in users)
                                {
                                    Console.WriteLine(user);
                                }
                                break;
                            case Commands.RemoveAllUsers:
                                userRepository.RemoveAllUsers();
                                Console.WriteLine("All users are removed");
                                break;
                            case Commands.Quite:
                                Console.WriteLine("Bye Bye");
                                return;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid command");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine(new String('*', 122));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;

                }

            }
        }
    }
}