using System;
using DataLagringFörstaInlämning.Domain;



namespace DataLagringFörstaInlämning
{
    class Program
    {
        static void Main(string[] args)
        {
            bool shouldRun = true;

            while (shouldRun) 
            {

                Console.WriteLine("1. Ny reservation\n2.Lista reservationer\n3.Utför besiktning\n4.Avsluta");

                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key) 
                {
                    case ConsoleKey.D1:

                        break;
                    case ConsoleKey.D2:
                        break;
                    case ConsoleKey.D3:

                        break;
                    case ConsoleKey.D4:
                        shouldRun = false;
                        break;



                }






            }
        }
    }
}
