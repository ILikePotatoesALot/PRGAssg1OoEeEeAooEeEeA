using System;
using System.Collections.Generic;
using System.Linq;

namespace AssgCode
{
    internal class Airline
    {
        private static bool isMessagePrinted = false;
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
                Console.WriteLine("Flight Addition Unsuccessful. Do Try Again.");
                return false;
            }
        }

        public bool RemoveFlight(Flight flight)
        {
            try
            {
                Flights.Remove(flight.FlightNumber);
                return true;
            }
            catch
            {
                Console.WriteLine("Flight Removal Unsuccessful. Do Try Again.");
                return false;
            }
        }

        public double CalculateFees()
            // Advance Q2 Seth
        {
            return 0;
        }

        public static bool CheckBGFlights(Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGateDict)
        {
            int count = 0;
            int total = flightDict.Count;
            foreach (var bg in boardingGateDict.Values)
            {
                if (bg.Flights != null)
                {
                    count += 1;
                }
            }
            return count == total;

        }

        //Advance Q3 Seth Listing which flights are unassigned for more convinience
        public static List<Flight> CheckFlight(Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGateDict)
        {
            List<Flight> flights = new List<Flight>();
            List<Flight> BG = new List<Flight>();
            List<Flight> unassign = new List<Flight>();
            foreach (BoardingGate bg in boardingGateDict.Values)
            {
                BG.Add(bg.Flights);
            }
            foreach (Flight fl in flightDict.Values)
            {
                flights.Add(fl);
            }
            foreach (Flight i in flights)
            {
                if (!BG.Contains(i))
                {
                    unassign.Add(i);
                }
            }
            return unassign;

        }

        public static double Discounts(Airline airline)
        {
            double firstDisc = Math.Floor(airline.Flights.Count / 3.0) * 350;
            double secondDisc = 0, thirdDisc = 0, fourthDisc = 0;

            foreach (Flight flight in airline.Flights.Values)
            {
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour >= 21)
                    secondDisc += 110;

                if (new[] { "Dubai (DXB)", "Bangkok (BKK)", "Tokyo (NRT)" }.Contains(flight.Origin))
                    thirdDisc += 25;

                if (flight is NORMFlight)
                    fourthDisc += 50;
            }

            return firstDisc + secondDisc + thirdDisc + fourthDisc;
        }

        public static (double total, double totalFee) CalculateTotal(Airline airline)
        {
            double total = 0, totalFee = 0;

            foreach (Flight fl in airline.Flights.Values)
            {
                double flightFee = fl.CalculateFees();
                total += flightFee;

                double gateFee = fl switch
                {
                    CFFTFlight => 150,
                    DDJBFlight => 300,
                    LWTTFlight => 500,
                    _ => 0
                };

                totalFee += gateFee;
            }

            return (total, totalFee);
        }

        public static void DisplayFees(Dictionary<string, Airline> airlinesDictionary, Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGateDict, Airline airline)
        {
            // Check if all flights are assigned to boarding gates
            if (!CheckBGFlights(flightDict, boardingGateDict))
            {
                // Print the error message only once
                if (!isMessagePrinted)
                {
                    Console.WriteLine("Error: Not all flights have been assigned to a boarding gate.");
                    isMessagePrinted = true;  // Set the flag to true after printing the message

                    // List unassigned flights
                    List<Flight> UnassignedFlights = CheckFlight(flightDict, boardingGateDict);
                    Console.WriteLine("Unassigned Flights are: ");
                    foreach (Flight flight in UnassignedFlights)
                    {
                        Console.WriteLine(flight.ToString());
                    }
                }
                return;  // Exit the method after printing the error
            }

            // If all flights are assigned, proceed with fee calculation
            double totalAmount = 0, totalDiscount = 0;

            Console.WriteLine("========== Airline Fees Breakdown ==========");

            (double total, double totalFees) = CalculateTotal(airline);
            double discount = Discounts(airline);

            if (airline.Flights.Count > 5)
                discount += total * 0.03;

            double finalFee = total - discount;

            Console.WriteLine($"Airline: {airline.Name}");
            Console.WriteLine($"  Total Fees: ${finalFee:F2}");
            Console.WriteLine($"  Initial Subtotal Fees: ${total:F2}");
            Console.WriteLine($"  Total Discount: ${discount:F2}");
            Console.WriteLine("=============================================");

            totalAmount += total;
            totalDiscount += discount;

            Console.WriteLine("\n========== Summary ==========");
            Console.WriteLine($"Total Subtotal Fees: ${totalAmount:F2}");
            Console.WriteLine($"Total Discounts: ${totalDiscount:F2}");
            Console.WriteLine($"Total Final Fees: ${(totalAmount - totalDiscount):F2}");
            Console.WriteLine($"Percentage of Discounts: {(totalDiscount / totalAmount) * 100:F2}%");
        }

        public override string ToString()
        {
            return $"Name: {Name,-10} Code: {Code,-10}";
        }
    }
}
