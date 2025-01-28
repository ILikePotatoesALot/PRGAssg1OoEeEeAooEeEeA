using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssgCode
{
    internal class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = "On Time";
        }


        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }

        public virtual double CalculateFees()
        {
            double fee = 300;
            if (Destination == "Singapore (SIN)") { fee += 500; }
            if (Origin == "Singapore (SIN)") { fee += 800; }
            return fee;
        }

        public virtual string ToString()
        {
            return $"Flight Number: {FlightNumber,-10}" +
                $"Origin: {Origin,-10}" +
                $"Destination: {Destination,-10}" +
                $"Expected Time: {ExpectedTime,-10}" +
                $"Status: {Status,-10}";
        }

        public int CompareTo(Flight other)
        {
            if (other == null)
                return 1;

            return this.ExpectedTime.CompareTo(other.ExpectedTime);
        }
    }
}
