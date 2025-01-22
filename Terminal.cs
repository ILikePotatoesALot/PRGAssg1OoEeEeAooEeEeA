using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssgCode
{
    internal class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        public Terminal() {; }
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
        }

        public bool AddAirline(Airline airline)
        {
            try
            {
                Airlines.Add(airline.Code, airline);
                return true;
            }
            catch
            {
                Console.WriteLine("Airline Addition Unsuccessful. Do try again. ");
                return false;
            }

        }
        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            try
            {
                BoardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }
            catch
            {
                Console.WriteLine("Boarding Gate Addition Unsuccessful. Do try again. ");
                return false;
            }
        }
        
        public Airline GetAirlineFromFlight(Flight flight)
        {
            //Airline dict -> airline -> flight dict -> flights
            foreach (Airline airline in Airlines.Values)
            {
                foreach (Flight flights in airline.Flights.Values)
                {
                    if (flight == flights) { return airline; }
                }
            }
            return null;
        }
        
        /*
        public void PrintAirlineFees()
        {

        }
        */

        public virtual string ToString()
        {
            return $"terminal name: {TerminalName}  gateFees: {GateFees} ";
        }
    }
}
