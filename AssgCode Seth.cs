
//==========================================================
//Student Number : S10265753
//Student Name : Seth Lee
//Partner Name : Austin Goh
//==========================================================



using AssgCode;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

//Q1

var airlinesDictionary = new Dictionary<string, Airline>();
var boardinggateDictionary = new Dictionary<string, BoardingGate>();


using (StreamReader sr = new StreamReader("airlines.csv"))
{
    string? line;
    sr.ReadLine(); // Skip header line
    while ((line = sr.ReadLine()) != null)
    {
        string[] columns = line.Split(",");

        Airline airline = new Airline(columns[0], columns[1]);
        airlinesDictionary[columns[1]] = airline;
    }
}

using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    string? line;
    sr.ReadLine(); // Skip header line
    while ((line = sr.ReadLine()) != null)
    {
        string[] columns = line.Split(",");

        BoardingGate boardinggate = new BoardingGate(columns[0], Convert.ToBoolean(columns[1]), Convert.ToBoolean(columns[2]), Convert.ToBoolean(columns[3]));
        boardinggateDictionary[columns[0]] = boardinggate;
    }
}

//Q2)

Dictionary<String, Flight> ReadFligh()
{
    Dictionary<String, Flight> flighdict = new Dictionary<String, Flight>();
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string? data = sr.ReadLine();
        if (data != null)
        {
            string[] heading = data.Split(",");
        }
        while (data != null)
        {
            string tempdata = sr.ReadLine();
            if (tempdata == null) { break; }
            string[] Objdata = tempdata.Split(",");
            string aircode = Objdata[0].Substring(0, 2);
            Airline airline = airlinesDictionary[aircode];
            if (Objdata[4] == "CFFT")
            {
                CFFTFlight flightt = new CFFTFlight(Objdata[0], Objdata[1], Objdata[2], Convert.ToDateTime(Objdata[3]));
                flighdict[Objdata[0]] = flightt;
                airline.Flights[Objdata[0]] = flightt;
            }
            if (Objdata[4] == "DDJB")
            {
                DDJBFlight flightt = new DDJBFlight(Objdata[0], Objdata[1], Objdata[2], Convert.ToDateTime(Objdata[3]));
                flighdict[Objdata[0]] = flightt;
                airline.Flights[Objdata[0]] = flightt;
            }
            if (Objdata[4] == "LWTT")
            {
                LWTTFlight flightt = new LWTTFlight(Objdata[0], Objdata[1], Objdata[2], Convert.ToDateTime(Objdata[3]));
                flighdict[Objdata[0]] = flightt;
                airline.Flights[Objdata[0]] = flightt;
            }
            if (Objdata[4] == "")
            {
                NORMFlight flightt = new NORMFlight(Objdata[0], Objdata[1], Objdata[2], Convert.ToDateTime(Objdata[3]));
                flighdict[Objdata[0]] = flightt;
                airline.Flights[Objdata[0]] = flightt;
            }

        }
    }
    return flighdict;
}
Dictionary<String, Flight> flighdict = ReadFligh();
Terminal T5 = new Terminal("Terminal 5", airlinesDictionary, flighdict, boardinggateDictionary);

//3 List all Flights with their basic info

void AllFlightBasicInfo(Dictionary<String, Flight> flighdict, Dictionary<string, Airline> airlinesDictionary)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination Departure/Arrival Time",-40}{"Special Request Code:",-10}");
    foreach (KeyValuePair<String, Flight> kv in flighdict)
    {

        string AirlineName = "";
        string flightcode = kv.Value.FlightNumber.Substring(0, 2);
        foreach (KeyValuePair<String, Airline> ad in airlinesDictionary)
        {
            if (ad.Value.Code == flightcode) { AirlineName = ad.Key; break; }
        }
        if (String.IsNullOrWhiteSpace(kv.Value.Status) == false)
        { Console.WriteLine($"{kv.Value.FlightNumber,-16}{AirlineName,-23}{kv.Value.Origin,-23}{kv.Value.Destination,-40}{kv.Value.Status,-10}"); }
        else { Console.WriteLine($"{kv.Value.FlightNumber,-16}{AirlineName,-23}{kv.Value.Origin,-23}{kv.Value.Destination,-40}{"None",-10}"); }
    }
}

//Q4

void DisplayBG()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine($"{"Gate Name",-15}{"DDJB",-20}{"CFFT",-20}{"LWTT",-20}");
    foreach (string bg in boardinggateDictionary.Keys)
    {
        BoardingGate i = boardinggateDictionary[bg];
        Console.WriteLine($"{i.GateName,-15}{i.SupportsDDJB,-20}{i.SupportsCFFT,-20}{i.SupportsLWTT,-20}");
    }
}

//5 Austin

bool checkflighnum(Dictionary<String, Flight> flighdict, string FlightNumber)
{
    if (flighdict.ContainsKey(FlightNumber)) { return true; }
    else { Console.WriteLine("Flight Number Not Found. "); return false; }
}

bool checkBoardinGate(Dictionary<String, BoardingGate> boardinggatedictionary, string BoardingGate)
{
    if (boardinggateDictionary.ContainsKey(BoardingGate)) { return true; }
    else { Console.WriteLine("Boarding Gate Not Found. "); return false; }
}

void OneFlightInfo(Dictionary<String, Flight> flighdict, Dictionary<string, Airline> airlinesDictionary, string FlightNumber)
{
    foreach (KeyValuePair<String, Flight> kv in flighdict)
    {
        if (kv.Key.Equals(FlightNumber))
        {
            string AirlineName = "";
            string flightcode = kv.Value.FlightNumber.Substring(0, 2);
            foreach (KeyValuePair<String, Airline> ad in airlinesDictionary)
            {
                if (ad.Value.Code == flightcode) { AirlineName = ad.Key; break; }
            }
            Console.WriteLine($"Flight Number: {kv.Value.FlightNumber,-10}");
            Console.WriteLine($"Airline Name: {AirlineName,-10} ");
            Console.WriteLine($"Origin:  {kv.Value.Origin,-10} ");
            Console.WriteLine($"Destination: {kv.Value.Destination,-10} ");
            Console.WriteLine($"Expected Departure/Arrival Time: {kv.Value.ExpectedTime,-10} ");
            if (String.IsNullOrWhiteSpace(kv.Value.Status) == false)
            { Console.WriteLine($"Status: {kv.Value.Status}"); }
            else { Console.WriteLine($"Status: Normal"); }
            break;
        }
    }
}

void OneBoardinGateInfo(Dictionary<String, BoardingGate> boardingGateDict, string BoardingGate)
{
    foreach (KeyValuePair<String, BoardingGate> kv in boardingGateDict)
    {
        if (kv.Key.Equals(BoardingGate))
        {
            Console.WriteLine($"Boarding Gate Name: {kv.Key}");
            Console.WriteLine($"Supports DDJB: {kv.Value.SupportsDDJB}");
            Console.WriteLine($"Supports CFFT: {kv.Value.SupportsCFFT}");
            Console.WriteLine($"Supports LWTT: {kv.Value.SupportsLWTT}");
            break;
        }
    }
}

string BGInput()
{
    string BoardinGate = "";
    while (true)
    {
        Console.WriteLine("Enter Boarding Gate Name: ");
        BoardinGate = Console.ReadLine();                              //Validation
        if (checkBoardinGate(boardinggateDictionary, BoardinGate) == true) { break; }
        else { continue; }
    }
    return BoardinGate;
}

string AssignFligh2BoardG(Dictionary<String, Flight> flighdict, Dictionary<string, BoardingGate> BoardinGateDictionary, string Flighnum)
{
    string BoardinGate = "";
    bool loopt = true;
    while (loopt == true)
    {
        BoardinGate = BGInput();
        OneBoardinGateInfo(boardinggateDictionary, BoardinGate);
        foreach (KeyValuePair<String, BoardingGate> kvp in boardinggateDictionary)
        {
            if (kvp.Key == BoardinGate && kvp.Value.Flights != null)
            {
                Console.WriteLine("Boarding Gate is already assigned.");
                break;
            }
            else if (kvp.Key == BoardinGate && kvp.Value.Flights == null)
            {
                boardinggateDictionary[kvp.Key].Flights = flighdict[Flighnum];
                loopt = false;
                break;
            }
        }
    }
    return BoardinGate;

}    //modifies BoardinGate Dict and has info on boarding gate for each fligh i think

void AssignFlighStatus(Dictionary<String, Flight> flighdict, string Flighnum, Dictionary<String, BoardingGate> boardinggatedictionary, string BoardinGate)
{
    string statopt = "";
    while (true)
    {
        Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
        statopt = Console.ReadLine();
        if (statopt != "Y" && statopt != "N") { Console.WriteLine("Invalid Response. Try Again."); }
        else { break; }
    }

    string status = "";
    if (statopt == "Y")
    {
        while (true)
        {
            Console.WriteLine("1.Delayed");
            Console.WriteLine("2.Boarding");
            Console.WriteLine("3.On Time");
            Console.WriteLine("Please select the new status of the flight: ");
            status = Console.ReadLine();
            if (status != "1" && status != "2" && status != "3") { Console.WriteLine("Invalid Response. Try Again."); }
            else { break; }
        }

        if (status == "1")
        {
            flighdict[Flighnum].Status = "Delayed";
        }
        else if (status == "2")
        {
            flighdict[Flighnum].Status = "Boarding";
        }
        else { flighdict[Flighnum].Status = "On Time"; }
        boardinggateDictionary[BoardinGate].Flights = flighdict[Flighnum];
    }
    Console.WriteLine($"Flight {Flighnum} has been assigned to Gate {BoardinGate}");
}

void Fligh2BoarWStatus(Dictionary<String, Flight> flighdict, Dictionary<String, BoardingGate> boardinggatedictionary)
{
    string Flighnum = "";
    while (true)
    {
        Console.WriteLine("Enter Flight Number: ");
        Flighnum = Console.ReadLine();
        if (checkflighnum(flighdict, Flighnum) == true) { break; }
        else { continue; }
    }
    OneFlightInfo(flighdict, airlinesDictionary, Flighnum);
    string boardinNum = AssignFligh2BoardG(flighdict, boardinggateDictionary, Flighnum);
    AssignFlighStatus(flighdict, Flighnum, boardinggateDictionary, boardinNum);


}

//Fligh2BoarWStatus(T5.Flights, T5.BoardingGates);

//Q6

void CreateNewFlight(Dictionary<String, Flight> flighdict, Dictionary<string, Airline> airlinesDictionary)
{
    while (true)
    {
        //FlighNum
        string NewFlighNum = "";
        while (true)
        {
            Console.Write("Enter Flight Number: ");
            NewFlighNum = Console.ReadLine();
            List<string> allowedAirlines = airlinesDictionary.Keys.ToList();
            List<string> CurrentFlighNums = flighdict.Keys.ToList(); // Example dictionary of existing strings

            string pattern = @"^[A-Za-z]{2} [0-9]{3}$";
            if (Regex.IsMatch(NewFlighNum, pattern))
            {
                string prefix = NewFlighNum.Substring(0, 2);    //taking out firs 2 letters (SQ)

                if (allowedAirlines.Contains(prefix) && !CurrentFlighNums.Contains(NewFlighNum))  //Check if prefix is in the allowed dictionary/if a existing flighnum exists
                {
                    break;
                }
                else
                {
                    Console.WriteLine("The input is not valid. Either the Airline Code is invalid or the Flight Number is not unique.");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("The input does not match the flight code requirements.");
                continue;
            }
        }


        //Origin + Destination
        Dictionary<string, string> airports = new Dictionary<string, string>
    {
        { "NRT", "Tokyo" },
        { "KUL", "Kuala Lumpur" },
        { "HKD", "Hong Kong" },
        { "SYD", "Sydney" },
        { "BKK", "Bangkok" },
        { "DXB", "Dubai" },
        { "MNL", "Manila" },
        { "CGK", "Jakarta" },
        { "MEL", "Melbourne" },
        { "SIN", "Singapore" },
        { "JFK", "New York City" },
        { "LHR", "London" },
        { "CDG", "Paris" },
        { "HND", "Tokyo" },
        // (Taken from Chatgpt. Point is  more can be easily added if needed)
    };

        List<String> airportsList = new List<String>();
        foreach (KeyValuePair<String, String> kv in airports)
        {
            airportsList.Add($"{kv.Value} ({kv.Key})");
        }
        string NewOrigin = "";
        string NewDestination = "";
        while (true)
        {
            Console.WriteLine("Enter Origin (e.g Bangkok (BKK))");
            NewOrigin = Console.ReadLine();
            if (airportsList.Contains(NewOrigin) == false) { Console.WriteLine("Invalid Flight Origin."); continue; }
            Console.WriteLine("Enter Destination (e.g. Singapore (SIN))");
            NewDestination = Console.ReadLine();
            if ((airportsList.Contains(NewDestination) == false)) { Console.WriteLine("Invalid Flight Destination."); continue; }
            if (NewDestination == NewOrigin) { Console.WriteLine("Origin and Destination Cannot Be the Same. "); continue; };
            if (NewDestination != "Singapore (SIN)" && NewOrigin != "Singapore (SIN)") { Console.WriteLine("Origin Or Destination Is Not Singapore (SIN)"); continue; }
            else { break; }
        }


        //Expc Arrival Time

        DateTime NewFormattedDATime;
        while (true)
        {
            Console.WriteLine("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            String NewdepartureArrivalTime = Console.ReadLine();
            if (DateTime.TryParse(NewdepartureArrivalTime, out NewFormattedDATime))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid Expected Departure/Arrival Time.");
            }

        }
        // Validate Special Request Code

        List<string> validRequestCodes = new List<string> { "CFFT", "DDJB", "LWTT", "None" };
        string SRC = "";

        while (true)   //Adding new flight to file
        {
            Console.WriteLine("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            SRC = Console.ReadLine();
            if (validRequestCodes.Contains(SRC))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid Special Request Code.");
            }
        }



        if (SRC == "None") { flighdict[NewFlighNum] = new Flight(NewFlighNum, NewOrigin, NewDestination, NewFormattedDATime); }
        else if (SRC == "CFFT") { flighdict[NewFlighNum] = new CFFTFlight(NewFlighNum, NewOrigin, NewDestination, NewFormattedDATime); }
        else if (SRC == "DDJB") { flighdict[NewFlighNum] = new DDJBFlight(NewFlighNum, NewOrigin, NewDestination, NewFormattedDATime); }
        else { flighdict[NewFlighNum] = new LWTTFlight(NewFlighNum, NewOrigin, NewDestination, NewFormattedDATime); }


        //Writing into file
        using (StreamWriter sw = new StreamWriter("flights.csv", false))
        {
            sw.WriteLine("Flight Number", "Origin", "Destination", "Expected Departure/Arrival", "Special Request Code");
            foreach (KeyValuePair<String, Flight> s in flighdict)
            {
                Flight Vals = s.Value;
                string test = Vals.FlightNumber + "," + Vals.Origin + "," + Vals.Destination + "," + Vals.ExpectedTime + "," + SRC;
                sw.WriteLine(test);
            }
        }

        Console.WriteLine($"{NewFlighNum} has been added! ");
        string Anotherfligh = "";
        while (true)
        {
            Console.WriteLine("Would you like to add another flight? (Y/N)");
            Anotherfligh = Console.ReadLine();
            if (Anotherfligh != "Y" && Anotherfligh != "N")
            {
                Console.WriteLine("Invalid Input, Try Again. ");
            }
            else { break; }
        }                    //Validate Add another flight


        ReadFligh();
        flighdict = ReadFligh();
        OneFlightInfo(flighdict, airlinesDictionary, NewFlighNum);
        if (Anotherfligh == "N") { break; }    // if no new flight break

    }



}

//7
// Display the list of airlines

void DisplayAirlines()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine($"{"Airline Code",-20}{"Airline Name",-20}");
    foreach (var airline in airlinesDictionary)
    {
        Console.WriteLine($"{airline.Value.Code,-20}{airline.Value.Name,-20}");
    }
}

// Check if an airline code exists in the dictionary
bool CheckAirline(Dictionary<string, Airline> airlinesDictionary, string airCode)
{
    if (airlinesDictionary.ContainsKey(airCode))
    {
        return true;
    }
    else
    {
        Console.WriteLine("Airline Code not found");
        return false;
    }
}

// Display full flight details
void FullFlightDetails()
{
    DisplayAirlines();

    string airCode;
    while (true)
    {
        Console.Write("Enter Airline Code: ");
        airCode = Console.ReadLine();

        if (CheckAirline(airlinesDictionary, airCode))
        {
            break;
        }
    }

    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}{"Estimated Time",-25}{"Status"}");

    foreach (var kv in flighdict)
    {
        var flight = kv.Value;

        // Extract the airline code from the flight number
        string flightAirlineCode = flight.FlightNumber.Substring(0, 2);

        // Only print the flight if the airline code matches the user input
        if (flightAirlineCode.Equals(airCode, StringComparison.OrdinalIgnoreCase))
        {
            string airlineName = airlinesDictionary.ContainsKey(flightAirlineCode)
                ? airlinesDictionary[flightAirlineCode].Name
                : "Unknown";

            string status = string.IsNullOrWhiteSpace(flight.Status) ? "None" : flight.Status;

            Console.WriteLine($"{flight.FlightNumber,-16}{airlineName,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime,-25}{status}");
        }
    }
} //shld rename this to fullairlinedetails

//8 Seth






void ModifyBI(string i)
{
    Console.Write("Enter new Origin: ");
    string origin = Console.ReadLine();
    Console.Write("Enter new Destination: ");
    string des = Console.ReadLine();
    Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
    string time = Console.ReadLine();
    Flight flightt = flighdict[i];
    flightt.Origin = origin;
    flightt.Destination = des;
    flightt.ExpectedTime = Convert.ToDateTime(time);
    flighdict[i] = flightt;
}

void ModifyStatus(string i)
{
    Console.Write("Enter new Status: ");
    string status = Console.ReadLine();
    Flight flightt = flighdict[i];
    flightt.Status = status;
    flighdict[i] = flightt;

}

void ModifySRC(string i)
{
    Console.Write("Enter new Special Request Code: ");
    string src = Console.ReadLine();
    Flight fl = flighdict[i];

    if (src == "CFFT")
    {
        CFFTFlight flightt = new CFFTFlight(fl.FlightNumber, fl.Origin, fl.Destination, Convert.ToDateTime(fl.ExpectedTime));
        flighdict[i] = flightt;
    }
    if (src == "DDJB")
    {
        DDJBFlight flightt = new DDJBFlight(fl.FlightNumber, fl.Origin, fl.Destination, Convert.ToDateTime(fl.ExpectedTime));
        flighdict[i] = flightt;
    }
    if (src == "LWTT")
    {
        LWTTFlight flightt = new LWTTFlight(fl.FlightNumber, fl.Origin, fl.Destination, Convert.ToDateTime(fl.ExpectedTime));
        flighdict[i] = flightt;
    }
    if (src == "")
    {
        NORMFlight flightt = new NORMFlight(fl.FlightNumber, fl.Origin, fl.Destination, Convert.ToDateTime(fl.ExpectedTime));
        flighdict[i] = flightt;
    }



}

void ModifyBG(string i)
{
    Flight fl = flighdict[i];
    foreach (string f in boardinggateDictionary.Keys)
    {
        BoardingGate bg = boardinggateDictionary[f];
        if (bg.Flights == fl)
        {
            bg.Flights = null;
            break;
        }
    }
    Console.Write("Enter Boarding Gate Number: ");
    string board = Console.ReadLine();
    BoardingGate bgg = boardinggateDictionary[board];
    bgg.Flights = fl;
}

void Modify(string i)
{
    Console.WriteLine("1. Modify Basic Information");
    Console.WriteLine("2. Modify Status");
    Console.WriteLine("3. Modify Special Request Code");
    Console.WriteLine("4. Modify Boarding Gate");
    Console.WriteLine("Choose an option: ");
    string choice = Console.ReadLine();
    if (choice == "1")
    {
        ModifyBI(i);
    }
    else if (choice == "2")
    {
        ModifyStatus(i);
    }
    else if (choice == "3")
    {
        ModifySRC(i);
    }
    else if (choice == "4")
    {
        ModifyBG(i);
    }

}


void ModifyFlight()
{
    FullFlightDetails();
    Console.WriteLine("Choose an existing Flight to modify or delete: ");
    string i = Console.ReadLine();
    Console.WriteLine("1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    Console.WriteLine("Choose an option: ");
    string choice = Console.ReadLine();
    if (choice == "1")
    {
        Modify(i);
    }
    else if (choice == "2")
    {
        Console.Write("Would you like to delete this flight (Y/N): ");
        string choose = Console.ReadLine();
        if (choose.ToUpper() == "Y")
        {
            flighdict.Remove(i);
        }

    }
}




//9 Austin

void SortedFlightInfo(Dictionary<String, Flight> flighdict, Dictionary<string, Airline> airlinesDictionary, Dictionary<String, BoardingGate> boardingatedict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}{"Expected Departure/Arrival Time",-40}{"Status",-17}{"Boarding Gate",-23}");
    List<Flight> Sortlis = new List<Flight>(flighdict.Values);
    Sortlis.Sort();
    foreach (Flight kv in Sortlis)
    {

        string AirlineName = "";
        string BoardinGate = "";
        string flightcode = kv.FlightNumber.Substring(0, 2);
        foreach (KeyValuePair<String, Airline> ad in airlinesDictionary)
        {
            if (ad.Value.Code == flightcode) { AirlineName = ad.Value.Name; break; }
        }
        foreach (KeyValuePair<String, BoardingGate> bg in boardingatedict)
        {
            if (bg.Value.Flights != null && bg.Value.Flights.FlightNumber == kv.FlightNumber) { BoardinGate = bg.Key; break; }
            else { BoardinGate = "Unassigned"; }
        }
        if (String.IsNullOrWhiteSpace(kv.Status) == false)
        { Console.WriteLine($"{kv.FlightNumber,-16}{AirlineName,-23}{kv.Origin,-23}{kv.Destination,-23}{kv.ExpectedTime,-40}{kv.Status,-17}{BoardinGate,-23}"); }
        else { Console.WriteLine($"{kv.FlightNumber,-16}{AirlineName,-23}{kv.Origin,-23}{kv.Destination,-23}{kv.ExpectedTime,-40}{"None",-17}{BoardinGate,-23}"); }
    }
}

//SortedFlightInfo(T5.Flights, T5.Airlines, T5.BoardingGates);
