using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssgCode
{
    internal class BoardingGate : Terminal
    {
        public string GateName { get; set; }

        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flightt { get; set; }

        public BoardingGate(string terminalName,string gateName, bool supportsCFFT,
            bool supportsDDJB, bool supportsLWTT, Flight flightt) : base(terminalName)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flightt = null;
        }
        public double CalculateFees()
        {
            double fees = 300;
            return fees;
        
        }
        
        public override string ToString()
        {
            return $"GateName: {GateName,-10}" +
                $"SupportsCFFT: {SupportsCFFT,-10}" +
                $"SupportsDDJB: {SupportsDDJB,-10}" +
                $"SupportsLWTT: {SupportsLWTT,-10}";
        }


    }
}
