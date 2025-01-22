using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssgCode
{
    internal class Airline : Terminal
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public Airline() { }
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public bool AddFlight(Flight flight)
        {
            try
            {
                Flights.Add(flight.FlightNumber, flight);
                return true;
            }
            catch
            {
                Console.WriteLine("Flight Addition Unsuccessful. Do Try Again. ");
                return false;
            }
        }
        /*
        public double Calculatefees()
        {
            double fees = 0;

        }
        */
        public bool RemoveFlight(Flight flight)
        {
            try
            {
                Flights.Remove(flight.FlightNumber);
                return true;
            }
            catch
            {
                Console.WriteLine("Flight Removal Unsuccessful. Do Try Again. ");
                return false;
            } 
        }

        public override string ToString()
        {
            return $"Name: {Name,-10} Code: {Code,-10} ";
        }
    }
}
