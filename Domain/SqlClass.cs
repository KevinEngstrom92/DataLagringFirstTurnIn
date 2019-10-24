using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace DataLagringFörstaInlämning.Domain
{
    class SqlClass
    {
        static string connectionString = @"Data Source = .;Initial Catalog = FIRSTTURNINADO; Integrated Security = True;";

      public static SqlConnection sqlconn = new SqlConnection(connectionString);
        

        public static void GetAllInspections() 
        {
            sqlconn.Open();
            
            Engine.inspectionList.Clear();

            string sql = @" 
                            SELECT [Id]
                            ,[RegistrationNumber]
                            ,[PerformedAt]
                            ,[IsApproved]
                         
                            FROM [FIRSTTURNINADO].[dbo].[Inspection]
                                                        ";
            SqlCommand command = new SqlCommand(sql, sqlconn);

            

            //Skicka SQL kommando till servern
            //Servern svarar nu i dataReader
            SqlDataReader dataReader = command.ExecuteReader();
    
            while (dataReader.Read())
            {

                int id = int.Parse(dataReader["Id"].ToString());
                string registrationNumber = dataReader["RegistrationNumber"].ToString();
                DateTime date = DateTime.Parse(dataReader["PerformedAt"].ToString());
                bool isApproved = bool.Parse(dataReader["IsApproved"].ToString());
               
                 
          

                


                Inspection inspectionPlaceHolder = new Inspection(id, registrationNumber, date, isApproved);

                // string completed = dataReader["Completed"].ToString();




                Engine.inspectionList.Add(inspectionPlaceHolder);
                
            }

            //Console.WriteLine("Hello Database Yes?");


            sqlconn.Close();

        }
    

        public static void GetAllReservations() 
        {
           
            sqlconn.Open();
            Engine.reservationList.Clear();

            string sql = @" 
                            SELECT [Id]
                            ,[RegistrationNumber]
                            ,[Date]
                         
                            FROM [FIRSTTURNINADO].[dbo].[Reservation]
                                                        ";
            SqlCommand command = new SqlCommand(sql, sqlconn);

           
           

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
            sqlconn.Open();
            string sql = $"INSERT INTO Reservation (RegistrationNumber, Date) VALUES (@REGISTRATIONNUMBER, @DATE)";
            
            SqlCommand command = new SqlCommand(sql, sqlconn);
            command.Parameters.AddWithValue("REGISTRATIONNUMBER", reservationPlaceHolder.RegistrationNumber);
            command.Parameters.AddWithValue("DATE", reservationPlaceHolder.Date);

         

            command.ExecuteNonQuery();
            Console.Clear();
            Console.WriteLine("Successfully added a reservation to the database");




            sqlconn.Close();

        }

        public static void SaveInspection(Inspection inspectionPlaceHolder)
        {
       
            SqlClass.GetAllReservations();
            SqlClass.GetAllInspections();

            sqlconn.Open();
            bool isBooked = false;
            Reservation deleteReservation = null;
            foreach (var reservation in Engine.reservationList)
            {
                if(reservation.RegistrationNumber == inspectionPlaceHolder.RegistrationNumber) 
                {
                    isBooked = true;
                    
                    deleteReservation = new Reservation(reservation.Id, reservation.RegistrationNumber, reservation.Date);
                }
            }
            if (isBooked)
            {
                string sql = $"INSERT INTO Inspection (RegistrationNumber, PerformedAt, IsApproved) VALUES (@REGISTRATIONNUMBER, @DATE, @ISAPPROVED)";

                SqlCommand command = new SqlCommand(sql, sqlconn);
                command.Parameters.AddWithValue("REGISTRATIONNUMBER", inspectionPlaceHolder.RegistrationNumber);
                command.Parameters.AddWithValue("DATE", inspectionPlaceHolder.PerformedAt);
                 
                command.Parameters.AddWithValue("ISAPPROVED", inspectionPlaceHolder.IsApproved);
              

                command.ExecuteNonQuery();

                string sqlDelete = $"DELETE FROM Reservation WHERE Id = @ID";
                
                SqlCommand commandDelte = new SqlCommand(sqlDelete, sqlconn);
                commandDelte.Parameters.AddWithValue("ID", deleteReservation.Id);

                commandDelte.ExecuteNonQuery();
                Console.Clear();
                Console.WriteLine("Inspection successfull");
                Thread.Sleep(2000);
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("There is no booking for that registration number");
                Thread.Sleep(2000);
            }
            sqlconn.Close();
        }
        
    }
    }

