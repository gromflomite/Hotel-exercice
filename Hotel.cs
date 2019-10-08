using System;

namespace Hotel
{
    internal class Hotel
    {
        private static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine("\n - Welcome to the Hotel management menu - ");
            Console.WriteLine("\n\t 1. Sign up new client");
            Console.WriteLine("\t 2. Edit client data");
            Console.WriteLine("\t 3. Check in client");
            Console.WriteLine("\t 4. Check out client");
            Console.WriteLine("\t 5. Check rooms");
            Console.WriteLine("\t 6. Quit application");

            int userSelect = int.Parse(Console.ReadLine());

            switch (userSelect)
            {
                case 1:
                    clientSignUp();
                    break;

                case 2:
                    clientEdit();
                    break;

                case 3:
                    clientCheckIn();
                    break;

                case 4:
                    clientCheckOut();
                    break;

                case 5:
                    roomCheck();
                    break;

                case 6:
                    break;
            }
        }

        public static void clientSignUp()
        {
            Console.Write("Enter the client name: ");
            string clientName = Console.ReadLine();

            Console.Write("Enter the client surname: ");
            string clientSurname = Console.ReadLine();

            Console.Write("Enter the client ID number: ");
            string clientID = Console.ReadLine();

            string querySignUp = $"INSERT INTO Hotels VALUES('{clientName}','{clientSurname}','{clientID}'";




        }

        public static void clientEdit()
        {

        }

        public static void clientCheckIn()
        {

        }

        public static void clientCheckOut()
        {

        }

        public static void roomCheck()
        {

        }
    }
}