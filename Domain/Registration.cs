using System;
using System.Collections.Generic;
using System.Text;


namespace DataLagringFörstaInlämning.Domain
{
    class Reservation
    {

       
            public int Id { get; }
            public string RegistrationNumber { get; }
            public DateTime Date { get; }
            

            public Reservation(string registrationNumber, DateTime date)
            {
                RegistrationNumber = registrationNumber;
                Date = date;
            }
            public Reservation(int id, string registrationNumber, DateTime date)
            {
                Id = id;
                RegistrationNumber = registrationNumber;
                Date = date;
            }
        
    }
}
