void Bg2FlighAdvFeature(string flighcode, Dictionary<string, BoardingGate> boardinggateDictionary, Dictionary<String, List<BoardingGate>> UnAssgBoardingGates,Flight UnassgFlight)
{
    foreach (BoardingGate Bgs in UnAssgBoardingGates[flighcode])
    {
        if (Bgs.Flights != null)
        {
            Console.WriteLine("Boarding Gate is already assigned.");
        }
        else if (Bgs.Flights == null)
        {
            boardinggateDictionary[Bgs.GateName].Flights = UnassgFlight;
            break;
        }
    }
}



void BulkAssignFligh2BoardG(Dictionary<String, Flight> flighdict, Dictionary<string, BoardingGate> boardinggateDictionary)
{
    Queue<Flight> UnassignedFlights = new Queue<Flight>();
    int UnassgBGCounter = 0;
    List<Flight> AssignedFlights = new List<Flight>();
    foreach (KeyValuePair<String,BoardingGate> kv in boardinggateDictionary)
    {
        if (kv.Value.Flights != null) { AssignedFlights.Add(kv.Value.Flights); }
        else { UnassgBGCounter += 1; }
    }
    foreach (KeyValuePair<String, Flight> fd in flighdict)
    {
        if (!AssignedFlights.Contains(fd.Value)) { UnassignedFlights.Enqueue(fd.Value); }
    }
    Console.WriteLine($"{UnassignedFlights.Count()} Flights Are Not Assigned To A Boarding Gate. ");
    Console.WriteLine($"{UnassgBGCounter} Boarding Gates Do Not have a flight Number Assigned. ");

    // AssgCode.DDJBFlight
    // UnassignedFlights.Dequeue();
    //make a dict of {statuscode:Flights}


    foreach (Flight fligh in UnassignedFlights)
    {
        string flighcode = fligh.GetType().ToString();
        if (flighcode.Substring(flighcode.Length - 10) == "DDJBFlight") { flighcode = "DDJB"; }
        else if (flighcode.Substring(flighcode.Length - 10) == "CFFTFlight") { flighcode = "CFFT"; }
        else if (flighcode.Substring(flighcode.Length - 10) == "LWTTFlight") { flighcode = "LWTT"; }
        else { flighcode = "None"; }

        Dictionary<String,List<BoardingGate>> UnAssgBoardingGates =new Dictionary<String,List<BoardingGate>>(); // Code : List of BoardingGates under that Code
        List<BoardingGate> Nonelis = new List<BoardingGate>();
        List<BoardingGate> CFFTlis = new List<BoardingGate>();
        List<BoardingGate> DDJBlis = new List<BoardingGate>();
        List<BoardingGate> LWTTlis = new List<BoardingGate>();
        foreach (KeyValuePair<String, BoardingGate> Bgs in boardinggateDictionary)
        {

            if (Bgs.Value.SupportsCFFT == false && Bgs.Value.SupportsDDJB == false && Bgs.Value.SupportsLWTT == false)
            {
                Nonelis.Add(Bgs.Value);
                UnAssgBoardingGates["None"] = Nonelis;
            }
            if (Bgs.Value.SupportsCFFT == true) { CFFTlis.Add(Bgs.Value); UnAssgBoardingGates["CFFT"] = CFFTlis; }
            if (Bgs.Value.SupportsDDJB == true) { DDJBlis.Add(Bgs.Value); UnAssgBoardingGates["DDJB"] = DDJBlis; }
            if (Bgs.Value.SupportsLWTT == true) { LWTTlis.Add(Bgs.Value); UnAssgBoardingGates["LWTT"] = LWTTlis; }
        }

        Bg2FlighAdvFeature(flighcode, boardinggateDictionary, UnAssgBoardingGates, fligh);
        
    }

}




//BulkAssignFligh2BoardG(T5.Flights, T5.BoardingGates);

//SortedFlightInfo(T5.Flights, T5.Airlines, T5.BoardingGates);
