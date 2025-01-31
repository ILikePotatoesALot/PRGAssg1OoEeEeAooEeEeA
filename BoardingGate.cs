


//==========================================================
//Student Number : S10266831
//Student Name : Austin Goh
//Partner Name : Seth Lee
//==========================================================




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
                return $"{GateName,-15}" +
                    $"{SupportsCFFT,-20}" +
                    $"{SupportsDDJB,-20}" +
                    $"{SupportsLWTT,-20}";
        }


    }
}
