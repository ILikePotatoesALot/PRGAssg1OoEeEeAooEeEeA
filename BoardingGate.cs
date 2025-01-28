
namespace AssgCode
{
    internal class BoardingGate
    {
        public string GateName { get; set; }

        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flightt { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT,
            bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flightt = null;

        }
        /*public double CalculateFees()
        {
            
            if (SupportsCFFT == true) { fee += 150; }
            if (SupportsDDJB == true) { fee += 300; }
            if (SupportsLWTT == true) {fee += 500; }
            
            return fee;
            

        }*/

        public override string ToString()
        {
            return $"GateName: {GateName,-10}" +
                $"SupportsCFFT: {SupportsCFFT,-10}" +
                $"SupportsDDJB: {SupportsDDJB,-10}" +
                $"SupportsLWTT: {SupportsLWTT,-10}";
        }


    }
}
