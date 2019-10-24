using System;
using System.Threading;
using DataLagringFörstaInlämning.Domain;



namespace DataLagringFörstaInlämning
{
    class Program
    {
        static void Main(string[] args)
        {
            bool shouldRun = true;
            SqlClass.GetAllReservations();
            while (shouldRun) 
            {
                Console.Clear();
                Console.WriteLine("1. Ny reservation\n2. Lista reservationer\n3. Utför besiktning\n4. Lista besiktningar\n5. Avsluta");

                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key) 
                {
                    case ConsoleKey.D1:
                        CreateReservation();
                        break;
                    case ConsoleKey.D2:
                        ListReservations();
                        break;
                    case ConsoleKey.D3:
                        ConductInspection();
                        break;
                    case ConsoleKey.D4:
                        ListInspections();
                        break;
                    case ConsoleKey.D5:
                        shouldRun = false;
                        break;



                }






            }
        }

        private static void ListInspections()
        {
            throw new NotImplementedException();
        }

        private static void CreateReservation()
        {
            Console.Clear();
            SqlClass.GetAllReservations();

            Console.WriteLine("Registration number: ");
            Console.WriteLine("Date for inspection: ");
            Console.SetCursorPosition(25, 0);
           
            string regNumber = Console.ReadLine();
            Console.SetCursorPosition(25, 1);
            DateTime date = DateTime.Parse(Console.ReadLine());

            Reservation reservationPlaceHolder = new Reservation(regNumber, date);

            Engine.reservationList.Add(reservationPlaceHolder);
            SqlClass.AddReservation(reservationPlaceHolder);
            Console.Clear(); 
            Console.WriteLine("Reservation created");
            Thread.Sleep(2000);
        }

        private static void ListReservations()
        {
            Console.Clear();
            SqlClass.GetAllReservations();
            Console.WriteLine("Registration Number:\t\tDate:");
            Console.WriteLine("--------------------------------------------------------");
            foreach (var reservation in Engine.reservationList)
            {
                Console.WriteLine($"{reservation.RegistrationNumber}\t\t\t\t{reservation.Date}");
            }
            Console.ReadKey(true);
        }

        private static void ConductInspection()
        {
            SqlClass.GetAllReservations();
            Console.Clear();
            Console.WriteLine("Registration number: ");
            Console.SetCursorPosition(25, 0);
            string regNumber = Console.ReadLine();
            bool isBooked = false;
            foreach (var reservation in Engine.reservationList)
            {
                if(reservation.RegistrationNumber == regNumber) 
                {
                    isBooked = true;
                    break;
                }
            }
            if (isBooked) 
            {
                Console.Clear();
                Console.WriteLine("There is a booking yes");
                Thread.Sleep(2000);
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("There is no booking in that particular registration number");
                Thread.Sleep(2000);
            }
        }
    }
}
