using System;
using System.Data.SqlClient;

namespace Hotel
{
    internal class Hotel
    {
        private static SqlConnection connection = new SqlConnection("Data Source=TRAVELMATE\\SQLEXPRESS;Initial Catalog=Hotels;Integrated Security=True");
        // (Above) We put the connection string in here in order to be used from the methods below. The elements declared in here, it will be able to be used by any other 
        // elements in the archive. Note that the name is "connection".

        private static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine("\n - Welcome to the Hotel management menu - ");
            Console.WriteLine("\n\t 1. Register new client");
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
            Console.Clear();
            Console.WriteLine("\n - Client register - ");
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

            Console.WriteLine("\n\t User registered properly (press Enter to return to main menu).");
            Console.ReadLine();
            Console.Clear();
            Menu();
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

                int idClient = int.Parse(reader[3].ToString()); // To extract the client ID (NOT DNI). We use the DNI introduced by the user to extract the ID from the Clients DB.

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

                DateTime dateCheckIn = DateTime.Now;

                string queryChoosedRoom = $"UPDATE Rooms SET Available = 'N' WHERE ID = '{choosedRoom}' INSERT INTO Bookings (CheckinDate, IDRoom, IDClient) VALUES ('{dateCheckIn}','{choosedRoom}','{idClient}')";
                // (Above) We enter the values in two DB, Bookings and Rooms (to set available or not).

                SqlCommand commandChoosedRoom = new SqlCommand(queryChoosedRoom, connection);

                connection.Open();

                Console.WriteLine("\n Your room has been reserved. (" + commandChoosedRoom.ExecuteNonQuery() + " rows modified). \n\n Press Enter to return to main menu.");
                
            }
            else
            {
                Console.WriteLine("\n\t The client ID does not exists. A user must be registered to be able to do a booking (press Enter to continue)");
                Console.ReadLine();
                connection.Close();
                clientSignUp();
            }
        }

        public static void clientCheckOut()
        {
            Console.WriteLine("\n - Check out- ");
            Console.Write("\n\t Enter client ID (DNI): ");
            string idCheck = Console.ReadLine();

            string queryCheckId = $"SELECT * FROM Clients WHERE DNI = '{idCheck}'";

            SqlCommand command = new SqlCommand(queryCheckId, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                

                Console.WriteLine($"\n\t Internal ID client: {reader[3]}");
                
            }
            else
            {
                Console.WriteLine("\n\t The client ID does not exists. Press Enter to return to main menu.");
                Console.ReadLine();
                Console.Clear();

                connection.Close();
                Menu();
            }

            connection.Close();


        }



        public static void roomCheck()
        {
        }
    }
}