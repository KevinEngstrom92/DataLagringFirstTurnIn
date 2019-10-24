using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace DataLagringFörstaInlämning.Domain
{
    class SqlClass
    {
        static string connectionString = @"Data Source = .;Initial Catalog = FIRSTTURNINADO; Integrated Security = True;";

      static SqlConnection sqlconn = new SqlConnection(connectionString);


        public static void GetAllReservations() 
        {
            Engine.reservationList.Clear();

            string sql = @" 
                            SELECT [Id]
                            ,[RegistrationNumber]
                            ,[Date]
                         
                            FROM [FIRSTTURNINADO].[dbo].[Reservation]
                                                        ";
            SqlCommand command = new SqlCommand(sql, sqlconn);

           
            sqlconn.Open();

            //Skicka SQL kommando till servern
            //Servern svarar nu i dataReader
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {

                int id = int.Parse(dataReader["Id"].ToString());
                string registrationNumber = dataReader["RegistrationNumber"].ToString();
                DateTime date = DateTime.Parse(dataReader["Date"].ToString());



               
                Reservation reservationPlaceHolder = new Reservation(id, registrationNumber, date);

                // string completed = dataReader["Completed"].ToString();




                Engine.reservationList.Add(reservationPlaceHolder);

            }

            //Console.WriteLine("Hello Database Yes?");
            sqlconn.Close();


            
        }

        public static void AddReservation(Reservation reservationPlaceHolder)
        {

            string sql = $"INSERT INTO Reservation (RegistrationNumber, Date) VALUES (@REGISTRATIONNUMBER, @DATE)";
            
            SqlCommand command = new SqlCommand(sql, sqlconn);
            command.Parameters.AddWithValue("REGISTRATIONNUMBER", reservationPlaceHolder.RegistrationNumber);
            command.Parameters.AddWithValue("DATE", reservationPlaceHolder.Date);

            sqlconn.Open();

            command.ExecuteNonQuery();
            Console.Clear();
            Console.WriteLine("Successfully added a reservation to the database");
            

            //Console.WriteLine("Hello Database Yes?");
            sqlconn.Close();



        }
    }
    }

