using System;
using System.Data.SqlClient;

namespace Hotel
{
    internal class Hotel
    {
        private static SqlConnection connection = new SqlConnection("Data Source=TRAVELMATE\\SQLEXPRESS;Initial Catalog=Hotels;Integrated Security=True");

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

            Console.Write("\n\t Enter option: ");
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
            Console.WriteLine("\n - Client sign up - ");
            Console.Write("\n\t Enter the client name: ");
            string clientName = Console.ReadLine();

            Console.Write("\n\t Enter the client surname: ");
            string clientSurname = Console.ReadLine();

            Console.Write("\n\t Enter the client ID number: ");
            string clientID = Console.ReadLine();

            connection.Open();

            string querySignup = $"INSERT INTO Clients (Firstname,Surname,DNI) values ('{clientName}','{clientSurname}','{clientID}')";

            SqlCommand command = new SqlCommand(querySignup, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void clientEdit()
        {
            Console.WriteLine("\n - Client details editing - ");
            Console.Write("\n\t Enter the client ID to check if exists: ");
            string idCheck = Console.ReadLine();

            string queryCheckId = $"SELECT * FROM Clients WHERE DNI = '{idCheck}'";

            SqlCommand command = new SqlCommand(queryCheckId, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Console.Write("\n\t Enter the name of the client: ");
                string clientName = Console.ReadLine();
                Console.Write("\n\t Enter the surname of the client: ");
                string clientSurname = Console.ReadLine();

                string queryClientEdit = $"UPDATE Clients SET FirstName='{clientName}',Surname='{clientSurname}' WHERE dni='{idCheck}'";
                SqlCommand commandIdUpdate = new SqlCommand(queryClientEdit, connection);
                connection.Close();

                connection.Open();
                Console.WriteLine(commandIdUpdate.ExecuteNonQuery());
                connection.Close();
            }
            else
            {
                Console.WriteLine("\n\t The client ID does not exists.");
                connection.Close();
                clientEdit();
            }

            connection.Close();
        }

        public static void clientCheckIn()
        {
            Console.WriteLine("\n - Client check in - ");
            Console.Write("\n\t Enter client ID: ");
            string idCheck = Console.ReadLine();

            string queryCheckId = $"SELECT * FROM Clients WHERE DNI = '{idCheck}'";

            SqlCommand command = new SqlCommand(queryCheckId, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                connection.Close();
                string queryCheckRooms = $"SELECT ID FROM Rooms WHERE Available = 'Y'";
                SqlCommand commandCheckRoom = new SqlCommand(queryCheckRooms, connection);

                connection.Open();
                SqlDataReader readerCheckRoom = commandCheckRoom.ExecuteReader();

                Console.WriteLine("\n ID of available rooms: \n");
                while (readerCheckRoom.Read())
                {
                    Console.WriteLine("\t" + readerCheckRoom[0].ToString());
                }
                connection.Close();

                Console.Write("\n Select the ID of the available room: ");
                string choosedRoom = Console.ReadLine();

                string queryChoosedRoom = $"UPDATE Rooms SET Available = 'N' WHERE ID = '{choosedRoom}'";


                SqlCommand commandChoosedRoom = new SqlCommand(queryChoosedRoom, connection);

                connection.Open();
                
                Console.WriteLine("\n Your room has been reserved: " + commandChoosedRoom.ExecuteNonQuery() + " room modified.");
            }
            else
            {
                Console.WriteLine("\n\t The client ID does not exists. A user must be registered to be able to do a booking");
                connection.Close();
                clientSignUp();
            }
        }

        public static void clientCheckOut()
        {
        }

        public static void roomCheck()
        {
        }
    }
}