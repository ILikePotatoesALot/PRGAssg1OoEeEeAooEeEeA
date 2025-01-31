namespace AssgCode
{
    internal class BoardingGate
    {
        public string GateName { get; set; }

        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flights { get; set; }



        public BoardingGate(string gateName, bool supportsCFFT,
            bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;

        }


        public double CalculateFees()
        {
            if (Flights is CFFTFlight)
            {
                return 150;
            }
            else if (Flights is DDJBFlight)
            {
                return 300;
            }
            else if (Flights is LWTTFlight)
            {
                return 500;
            }
            return 0;
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

