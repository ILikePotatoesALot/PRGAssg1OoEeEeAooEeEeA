//==========================================================
//Student Number : S10265753
//Student Name : Seth Lee
//Partner Name : Austin Goh
//==========================================================



using AssgCode;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

//Q1 Seth

var ADictionary = new Dictionary<string, Airline>();
var BGDictionary = new Dictionary<string, BoardingGate>();


using (StreamReader sr = new StreamReader("airlines.csv"))
{
    string? line;
    sr.ReadLine(); // Skip header line
    while ((line = sr.ReadLine()) != null)
    {
        string[] columns = line.Split(",");

        Airline airline = new Airline(columns[0], columns[1]);
        ADictionary[columns[1]] = airline;
    }

}
Console.WriteLine("Loading Airlines...");
Console.WriteLine($"{ADictionary.Count()} Airlines Loaded!");


using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    string? line;
    sr.ReadLine(); // Skip header line
    while ((line = sr.ReadLine()) != null)
    {
        string[] columns = line.Split(",");

        BoardingGate boardinggate = new BoardingGate(columns[0], Convert.ToBoolean(columns[1]), Convert.ToBoolean(columns[2]), Convert.ToBoolean(columns[3]));
        BGDictionary[columns[0]] = boardinggate;

    }
}
Console.WriteLine("Loading Boarding Gates...");
Console.WriteLine($"{BGDictionary.Count()} Boarding Gates Loaded!");



//Q2)  Austin

Dictionary<String, Flight> ReadFligh()
{
    Dictionary<String, Flight> flighdict = new Dictionary<String, Flight>();
    int count = 0;
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string? data = sr.ReadLine();   //skip header line
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
            Airline airline = ADictionary[aircode];   
            count++;
            if (Objdata[4] == "CFFT")  //assign to each diff flight and their code
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
            if (String.IsNullOrWhiteSpace(Objdata[4]))
            {
                NORMFlight flightt = new NORMFlight(Objdata[0], Objdata[1], Objdata[2], Convert.ToDateTime(Objdata[3]));
                flighdict[Objdata[0]] = flightt;
                airline.Flights[Objdata[0]] = flightt;
            }
        }
        Console.WriteLine("Loading Flights...");
        Console.WriteLine($"{count} Flights Loaded!");
       

    }
    return flighdict;
}



//3 List all Flights with their basic info Austin

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
        if (String.IsNullOrWhiteSpace(kv.Value.Status) == false) //check if they have a special code using IsNullorWhiteSpace
        { Console.WriteLine($"{kv.Value.FlightNumber,-16}{AirlineName,-23}{kv.Value.Origin,-23}{kv.Value.Destination,-40}{kv.Value.Status,-10}"); }
        else { Console.WriteLine($"{kv.Value.FlightNumber,-16}{AirlineName,-23}{kv.Value.Origin,-23}{kv.Value.Destination,-40}{"None",-10}"); }
    }
}

//Q4 Seth

void DisplayBG(Dictionary<String,BoardingGate> BoardinGateDict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine($"{"Gate Name",-15}{"DDJB",-20}{"CFFT",-20}{"LWTT",-20}");
    foreach (string bg in BoardinGateDict.Keys)
    {
        BoardingGate i = BoardinGateDict[bg];
        Console.WriteLine($"{i.GateName,-15}{i.SupportsDDJB,-20}{i.SupportsCFFT,-20}{i.SupportsLWTT,-20}");
    }
}

//5 Austin

bool checkflighnum(Dictionary<String, Flight> flighdict, string FlightNumber)
{
    if (flighdict.ContainsKey(FlightNumber)) { return true; }
    else { Console.WriteLine("Flight Number Not Found. "); return false; }    //shortcuts for validation later on
}

bool checkBoardinGate(Dictionary<String, BoardingGate> boardinggateDictionary, string BoardingGate)
{
    if (boardinggateDictionary.ContainsKey(BoardingGate)) { return true; }
    else { Console.WriteLine("Boarding Gate Not Found. "); return false; }
}

void OneFlightInfo(Dictionary<String, Flight> flighdict, Dictionary<string, Airline> airlinesDictionary, string FlightNumber)   //Displays the info of a specific flight i want.
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
       //     Console.WriteLine($"Airline Name: {AirlineName,-10} ");
            Console.WriteLine($"Origin: {kv.Value.Origin,-10} ");
            Console.WriteLine($"Destination: {kv.Value.Destination,-10} ");
            Console.WriteLine($"Expected Time: {kv.Value.ExpectedTime,-10} ");
            if (String.IsNullOrWhiteSpace(kv.Value.Status) == false)
            { Console.WriteLine($"Status: {kv.Value.Status}"); }
            else { Console.WriteLine($"Status: Normal"); }
            break;
        }
    }
}

void OneBoardinGateInfo(Dictionary<String, BoardingGate> boardingGateDict, string BoardingGate)   //Same as before but for boarding gates
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

string BGInput(Dictionary<String, BoardingGate> BoardinGateDictionary)  //the ibput for the boarding gate with validation
{
    string BoardinGate = "";
    while (true)
    {
        Console.WriteLine("Enter Boarding Gate Name: ");
        BoardinGate = Console.ReadLine();                              //Validation
        if (checkBoardinGate(BoardinGateDictionary, BoardinGate) == true) { break; }
        else { continue; }
    }
    return BoardinGate;
}

string AssignFligh2BoardG(Dictionary<String, Flight> flighdict, Dictionary<string, BoardingGate> BoardinGateDictionary,Dictionary<String,Airline> AirlineDictionary, string Flighnum)
{
    string BoardinGate = "";
    bool loopt = true;
    while (loopt == true)
    {
        BoardinGate = BGInput(BoardinGateDictionary);
        OneFlightInfo(flighdict, AirlineDictionary, Flighnum);
        OneBoardinGateInfo(BoardinGateDictionary, BoardinGate);
        foreach (KeyValuePair<String, BoardingGate> kvp in BoardinGateDictionary)  //checking if the Bg is assigned already as it runs down
        {
            if (kvp.Key == BoardinGate && kvp.Value.Flights != null)
            {
                Console.WriteLine("Boarding Gate is already assigned.");
                break;
            }
            else if (kvp.Key == BoardinGate && kvp.Value.Flights == null)
            {
                BoardinGateDictionary[kvp.Key].Flights = flighdict[Flighnum];
                loopt = false;
                break;
            }
        }
    }
    return BoardinGate;

}    //modifies BoardinGate Dict and has info on boarding gate for each fligh i think

void AssignFlighStatus(Dictionary<String, Flight> flighdict, string Flighnum, Dictionary<String, BoardingGate> boardinggatedictionary, string BoardinGate) //Assigns the status of the flight
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
        boardinggatedictionary[BoardinGate].Flights = flighdict[Flighnum];
    }
    Console.WriteLine($"Flight {Flighnum} has been assigned to Gate {BoardinGate}");
}

void Fligh2BoarWStatus(Dictionary<String, Flight> flighdict, Dictionary<String, BoardingGate> boardinggatedictionary,Dictionary<String,Airline> AirlineDictionary) //the accumalation of all the methods before 
{
    string Flighnum = "";
    while (true)
    {
        Console.WriteLine("Enter Flight Number: ");
        Flighnum = Console.ReadLine();
        if (checkflighnum(flighdict, Flighnum) == true) { break; }
        else { continue; }
    }
    string boardinNum = AssignFligh2BoardG(flighdict, boardinggatedictionary,AirlineDictionary, Flighnum);
    AssignFlighStatus(flighdict, Flighnum, boardinggatedictionary, boardinNum);


}


//Q6 Austin

void CreateNewFlight(Dictionary<String, Flight> flighdict, Dictionary<string, Airline> airlinesDictionary,Terminal T5)
{
    while (true)
    {
        //FlighNum
        string NewFlighNum = "";
        while (true)
        {
            List<string> allowedAirlines = airlinesDictionary.Keys.ToList();
            List<string> CurrentFlighNums = flighdict.Keys.ToList(); // Example dictionary of existing strings
            Console.Write("Enter Flight Number: ");
            NewFlighNum = Console.ReadLine();

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
        // (Taken from Chatgpt. Point is more can be easily added if needed)
    };

        List<String> airportsList = new List<String>();
        foreach (KeyValuePair<String, String> kv in airports)
        {
            airportsList.Add($"{kv.Value} ({kv.Key})");
        }
        string NewOrigin = "";
        string NewDestination = "";
        while (true)    //to check if the origin and destination is registered under the current dictionary of accepted airports.
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

        //depending on the SRC the type of flight it is differs
        
        if (SRC == "None") { flighdict[NewFlighNum] = new Flight(NewFlighNum, NewOrigin, NewDestination, NewFormattedDATime); }
        else if (SRC == "CFFT") { flighdict[NewFlighNum] = new CFFTFlight(NewFlighNum, NewOrigin, NewDestination, NewFormattedDATime); }
        else if (SRC == "DDJB") { flighdict[NewFlighNum] = new DDJBFlight(NewFlighNum, NewOrigin, NewDestination, NewFormattedDATime); }
        else { flighdict[NewFlighNum] = new LWTTFlight(NewFlighNum, NewOrigin, NewDestination, NewFormattedDATime); }

        Airline airline = T5.GetAirlineFromFlight(flighdict[NewFlighNum]);
        airline.AddFlight(flighdict[NewFlighNum]);   //add the new flight to the airline dictionary

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


//7  Display the list of airlines Seth

void DisplayAirlines(Dictionary<String,Airline> airlinesDictionary)
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
void FullAirlineFlightDetails(Dictionary<String,Flight> flighdict,Dictionary<String,Airline> airlinesdictionary)
{
    DisplayAirlines(airlinesdictionary);
    string airCode;
    while (true)
    {
        Console.Write("Enter Airline Code: ");
        airCode = Console.ReadLine();

        if (CheckAirline(airlinesdictionary, airCode))
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
            string airlineName = airlinesdictionary.ContainsKey(flightAirlineCode)
                ? airlinesdictionary[flightAirlineCode].Name
                : "Unknown";

            string status = string.IsNullOrWhiteSpace(flight.Status) ? "None" : flight.Status;

            Console.WriteLine($"{flight.FlightNumber,-16}{airlineName,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime,-25}{status}");
        }
    }
} 

//8 Seth



void ModifyBI(string i, Dictionary<string, Flight> flighdict)
{
    // Validate if the flight exists in the dictionary
    if (!flighdict.ContainsKey(i))
    {
        Console.WriteLine("Flight not found in the dictionary.");
        return;
    }

    // Validate and get new origin
    string origin;
    do
    {
        Console.Write("Enter new Origin: ");
        origin = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(origin))
        {
            Console.WriteLine("Origin cannot be empty. Please try again.");
        }
    } while (string.IsNullOrWhiteSpace(origin));

    // Validate and get new destination
    string des;
    do
    {
        Console.Write("Enter new Destination: ");
        des = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(des))
        {
            Console.WriteLine("Destination cannot be empty. Please try again.");
        }
    } while (string.IsNullOrWhiteSpace(des));

    // Validate and get new expected departure/arrival time
    DateTime expectedTime;
    bool isValidTime;
    do
    {
        Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
        string time = Console.ReadLine();
        isValidTime = DateTime.TryParseExact(time, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out expectedTime);
        if (!isValidTime)
        {
            Console.WriteLine("Invalid date/time format. Please use the format dd/MM/yyyy HH:mm.");
        }
    } while (!isValidTime);

    // Update the flight details
    Flight flightt = flighdict[i];
    flightt.Origin = origin;
    flightt.Destination = des;
    flightt.ExpectedTime = expectedTime;

    Console.WriteLine("Flight details updated successfully.");
}

void ModifyStatus(string i, Dictionary<string, Flight> flightDict)
{
    if (!flightDict.ContainsKey(i))
    {
        Console.WriteLine("Error: Flight not found.");
        return;
    }

    Console.Write("Enter new Status: ");
    string? status = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(status))
    {
        Console.WriteLine("Error: Invalid status.");
        return;
    }

    flightDict[i].Status = status; // No need to reassign if Flight is a class
    Console.WriteLine("Status updated successfully.");
}


void ModifySRC(string i, Dictionary<string, Flight> flightDict)
{
    if (!flightDict.ContainsKey(i))
    {
        Console.WriteLine("Error: Flight not found.");
        return;
    }

    Console.Write("Enter new Special Request Code: ");
    string? src = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(src))
    {
        Console.WriteLine("Error: Special Request Code cannot be empty.");
        return;
    }

    Flight fl = flightDict[i];

    // Creating a new flight object based on the SRC code
    Flight newFlight;
    switch (src)
    {
        case "CFFT":
            newFlight = new CFFTFlight(fl.FlightNumber, fl.Origin, fl.Destination, fl.ExpectedTime);
            break;
        case "DDJB":
            newFlight = new DDJBFlight(fl.FlightNumber, fl.Origin, fl.Destination, fl.ExpectedTime);
            break;
        case "LWTT":
            newFlight = new LWTTFlight(fl.FlightNumber, fl.Origin, fl.Destination, fl.ExpectedTime);
            break;
        case "NORM":
            newFlight = new NORMFlight(fl.FlightNumber, fl.Origin, fl.Destination, fl.ExpectedTime);
            break;
        default:
            Console.WriteLine("Error: Invalid Special Request Code.");
            return;
    }

    flightDict[i] = newFlight;
    Console.WriteLine($"Flight {fl.FlightNumber} updated to type {newFlight.GetType().Name}.");
}


void ModifyBG(string i, Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGateDict)
{
    if (!flightDict.ContainsKey(i))
    {
        Console.WriteLine("Error: Flight not found.");
        return;
    }

    Flight fl = flightDict[i];

    // Remove the flight from the previous gate
    foreach (var bg in boardingGateDict.Values)
    {
        if (bg.Flights == fl)
        {
            bg.Flights = null;
            break;
        }
    }

    Console.Write("Enter Boarding Gate Number: ");
    string? board = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(board) || !boardingGateDict.ContainsKey(board))
    {
        Console.WriteLine("Error: Invalid or non-existent boarding gate number.");
        return;
    }

    BoardingGate newGate = boardingGateDict[board];
    newGate.Flights = fl;

    Console.WriteLine($"Flight {fl.FlightNumber} is now assigned to Gate {board}.");
}


void Modify(string i, Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGateDict)
{
    if (!flightDict.ContainsKey(i))
    {
        Console.WriteLine("Error: Flight not found.");
        return;
    }

    Console.WriteLine("1. Modify Basic Information");
    Console.WriteLine("2. Modify Status");
    Console.WriteLine("3. Modify Special Request Code");
    Console.WriteLine("4. Modify Boarding Gate");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine()?.Trim();

    switch (choice)
    {
        case "1":
            ModifyBI(i, flightDict);
            break;
        case "2":
            ModifyStatus(i, flightDict);
            break;
        case "3":
            ModifySRC(i, flightDict);
            break;
        case "4":
            ModifyBG(i, flightDict, boardingGateDict);
            break;
        default:
            Console.WriteLine("Error: Invalid option. Please enter a number between 1 and 4.");
            break;
    }
}


void ModifyFlight(Dictionary<string, Flight> flightDict, Dictionary<string, Airline> airlineDict, Dictionary<string, BoardingGate> boardingGateDict, Terminal T5)
{
    FullAirlineFlightDetails(flightDict, airlineDict);

    Console.Write("Choose an existing Flight to modify or delete: ");
    string? i = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(i) || !flightDict.ContainsKey(i))
    {
        Console.WriteLine("Error: Flight not found.");
        return;
    }

    Console.WriteLine("1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine()?.Trim();

    switch (choice)
    {
        case "1":
            Modify(i, flightDict, boardingGateDict);
            break;
        case "2":
            Console.Write("Would you like to delete this flight (Y/N): ");
            string? confirmDelete = Console.ReadLine()?.Trim().ToUpper();

            if (confirmDelete == "Y")
            {
                Airline airline = T5.GetAirlineFromFlight(flightDict[i]);

                if (airline != null)
                {
                    airline.RemoveFlight(flightDict[i]);
                    flightDict.Remove(i);
                    Console.WriteLine($"Flight {i} successfully deleted.");
                }
                else
                {
                    Console.WriteLine("Error: Could not find associated airline.");
                }
            }
            break;
        default:
            Console.WriteLine("Error: Invalid option. Please enter 1 or 2.");
            break;
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

//Advance Q1 Austin


void BulkAssignFligh2BoardG(Dictionary<String, Flight> flighdict, Dictionary<string, BoardingGate> boardinggateDictionary, Dictionary<string,Airline> AirlineDict)
{
    Queue<Flight> UnassignedFlights = new Queue<Flight>();
    int UnassgBGCounter = 0;
    List<Flight> AssignedFlights = new List<Flight>();
    foreach (KeyValuePair<String, BoardingGate> kv in boardinggateDictionary)
    {
        if (kv.Value.Flights != null) { AssignedFlights.Add(kv.Value.Flights);}
        else { UnassgBGCounter += 1; }   //tell me which flights already have a boarding gate
    }
    foreach (KeyValuePair<String, Flight> fd in flighdict)
    {
        if (!AssignedFlights.Contains(fd.Value)) { UnassignedFlights.Enqueue(fd.Value); }   //if the flight in flighdict is not contained in Assigned flights means we can enqueue it into the UnassignedFlights Queue
    }
    Console.WriteLine($"{UnassignedFlights.Count()} Flights Are Not Assigned To A Boarding Gate. ");
    Console.WriteLine($"{UnassgBGCounter} Boarding Gates Do Not have a flight Number Assigned. ");

    int assgcounter = 0;
    foreach (Flight fligh in UnassignedFlights)
    {
        string flighcode = fligh.GetType().ToString();
        if (flighcode.Substring(flighcode.Length - 10) == "DDJBFlight") { flighcode = "DDJB"; }
        else if (flighcode.Substring(flighcode.Length - 10) == "CFFTFlight") { flighcode = "CFFT"; }
        else if (flighcode.Substring(flighcode.Length - 10) == "LWTTFlight") { flighcode = "LWTT"; }   //honestly this part can be slotted inside the if statements below but i feel it looks cleaner this way
        else { flighcode = "None"; }

        Dictionary<String, List<BoardingGate>> UnAssgBoardingGates = new Dictionary<String, List<BoardingGate>>(); // Code : List of BoardingGates under that Code
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
            if (Bgs.Value.SupportsCFFT == true) { CFFTlis.Add(Bgs.Value); UnAssgBoardingGates["CFFT"] = CFFTlis; }  //checking for their flight type.
            if (Bgs.Value.SupportsDDJB == true) { DDJBlis.Add(Bgs.Value); UnAssgBoardingGates["DDJB"] = DDJBlis; }
            if (Bgs.Value.SupportsLWTT == true) { LWTTlis.Add(Bgs.Value); UnAssgBoardingGates["LWTT"] = LWTTlis; }
        }
        assgcounter += 1;
        foreach (BoardingGate Bgs in UnAssgBoardingGates[flighcode])
        {
            if (Bgs.Flights == null)
            {
                boardinggateDictionary[Bgs.GateName].Flights = fligh;   //properly assigning them to the global boardinggatedictionary
                break;
            }
        }
    }
    SortedFlightInfo(flighdict,AirlineDict,boardinggateDictionary);
    Console.WriteLine($"{assgcounter} Flights and Boarding Gates were processed and assigned");

    float AutoVsBefore = 0;
    try { AutoVsBefore = (assgcounter / (flighdict.Count))*100; } catch { Console.WriteLine("There are no longer any flights left, but why, or how would you do this??? "); }
    Console.WriteLine($"{AutoVsBefore}% of Flights and Boarding Gates were processed automatically over those that were already assigned");

}





//Advance Q2 Seth
bool CheckBGFlights(Dictionary<String,Flight> flighdict, Dictionary<String, BoardingGate> boardinggateDictionary)
{
    int count = 0;
    int total = flighdict.Count;
    List < Flight > flightlist = [];
    foreach (string bg in boardinggateDictionary.Keys)
    {
        if (boardinggateDictionary[bg].Flights != null)
        {
            count += 1;
        }
    }
    if (count != total)
    {
        return false;
    }
    return true;
    
}

static double Discounts(Airline airline)
{
    //Discount 1
    double numflight = airline.Flights.Count();
    double firstdisc = (Math.Floor(numflight / Convert.ToDouble(3))) * 350;
    //Discount 2
    double seconddisc = 0;
    double thirddisc = 0;
    double fourthdisc = 0;
    foreach (Flight flight in airline.Flights.Values)
    {
        if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour >= 21)
        {
            seconddisc += 110;
        }
        //Discount 3
        if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (BKK)" || flight.Origin == "Tokyo (NRT)")
        {
            thirddisc += 25;
        }
        if (flight is NORMFlight)
        {
            fourthdisc += 50;
        }
    }
    double TotalDiscount = firstdisc + seconddisc + thirddisc + fourthdisc;
    return TotalDiscount;
}







static (double total, double totalFee) CalculateTotal(Airline airline)
{
    double total = 0;
    double totalfee = 0;
    foreach (Flight fl in airline.Flights.Values)
    {
        if (fl is CFFTFlight)
        {
            double inniamt = fl.CalculateFees();
            total += inniamt;
            double gatefee = 150;
            totalfee += gatefee;
        }
        else if (fl is DDJBFlight)
        {
            double inniamt = fl.CalculateFees();
            total += inniamt;
            double gatefee = 300;
            totalfee += gatefee;
        }
        else if (fl is LWTTFlight)
        {
            double inniamt = fl.CalculateFees();
            total += inniamt;
            double gatefee = 500;
            totalfee += gatefee;
        }
        else
        {
            double inniamt = fl.CalculateFees();
            total += inniamt;
            double gatefee = 0;
            totalfee += gatefee;
        }
    }
    return (total, totalfee);
}

void DisplayFees(Dictionary<String,Airline> airlinesDictionary,Dictionary<String,Flight> flighdict)
{
    if (CheckBGFlights(flighdict, BGDictionary) == false)
    {
        Console.WriteLine("Not all flights have been assigned to a boarding gate.");
        return;
        
    }
    double totalamt = 0;
    double totaldisc = 0;
    foreach (Airline airline in airlinesDictionary.Values)
    {
        (double total, double totalfees) = CalculateTotal(airline);
        double discount = Discounts(airline);
        if (airline.Flights.Values.Count > 5)
        {
            double fifthdisc = total * 0.03;
            discount += fifthdisc;
        }
        Console.WriteLine(airline.Name);
        Console.WriteLine("Total Fees: " + (total - discount));
        Console.WriteLine("Initial Subtotal Fees: " + (total));
        Console.WriteLine("Total Discount: " + (discount));
        Console.WriteLine("=============================================");
        totalamt += total;
        totaldisc += discount;
    }
    Console.WriteLine("Total Subtotal Fees: " + totalamt);
    Console.WriteLine("Total Discounts: " + totaldisc);
    Console.WriteLine("Total Final Fees: " + (totalamt - totaldisc));
    Console.WriteLine($"Percentage of Discounts: {(totaldisc / totalamt) * 100:F2}%");
}


static List<Flight> SearchFlights(Dictionary<string, Flight> flightDict, string origin, string destination, DateTime? expectedTime)
{
    return flightDict.Values
        .Where(f => (origin == null || f.Origin == origin) &&
                    (destination == null || f.Destination == destination) &&
                    (expectedTime == null || f.ExpectedTime.Date == expectedTime.Value.Date))
        .ToList();
}

void FlightSearching(Dictionary<string, Flight> FD)
{
    // Getting the origin and destination
    Console.Write("What is the Origin: ");
    string origin = Console.ReadLine()?.Trim();

    Console.Write("What is the Destination: ");
    string destination = Console.ReadLine()?.Trim();

    // Getting the expected time with validation
    DateTime? expected = null;
    while (true)
    {
        Console.Write("What is the Expected Time (YYYY-MM-DD HH:mm): ");
        string dateInput = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(dateInput)) // Allowing for empty input (no filter on date)
        {
            break;
        }

        if (DateTime.TryParse(dateInput, out DateTime parsedDate)) // Try to parse the entered date
        {
            expected = parsedDate;
            break;  // Exit the loop once we have a valid date
        }
        else
        {
            Console.WriteLine("Invalid date format. Please try again (format: YYYY-MM-DD HH:mm).");
        }
    }

    // Search for flights based on input
    var result = SearchFlights(FD, origin, destination, expected);

    // Display search results
    if (result.Count > 0)
    {
        Console.WriteLine("\nFound Flights:");
        foreach (var flight in result)
        {
            Console.WriteLine(flight.ToString()); // Assuming ToString() is implemented in the Flight class
        }
    }
    else
    {
        Console.WriteLine("\nNo flights found matching the search criteria.");
    }
}





void Final()
{
    Dictionary<string, Flight> FDictionary = ReadFligh();
    Terminal T5 = new Terminal("Terminal 5", ADictionary, FDictionary, BGDictionary);

    List<string> AllowableInputs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9","10", "0" };

    while (true)
    {
        // Main menu
        Console.WriteLine("\n\n\n=============================================");
        Console.WriteLine("Welcome to Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("1. List All Flights");
        Console.WriteLine("2. List Boarding Gates");
        Console.WriteLine("3. Assign a Boarding Gate to a Flight");
        Console.WriteLine("4. Create Flight");
        Console.WriteLine("5. Display Airline Flights");
        Console.WriteLine("6. Modify Flight Details");
        Console.WriteLine("7. Display Flight Schedule");
        Console.WriteLine("8. Process All Unassigned Flights To Boarding Gates In Bulk");
        Console.WriteLine("9. Display Total Fee Per Airline For The Day");
        Console.WriteLine("10. Flight Searching");
        Console.WriteLine("0. Exit");
        Console.Write("Please select your option: ");

        string? option = Console.ReadLine()?.Trim();

        // Validate input
        if (string.IsNullOrEmpty(option) || !AllowableInputs.Contains(option))
        {
            Console.WriteLine("Invalid Input, Try Again.");
            continue;  // Loops back to menu if input is invalid
        }

        switch (option)
        {
            case "1":
                AllFlightBasicInfo(T5.Flights, T5.Airlines);
                break;
            case "2":
                DisplayBG(T5.BoardingGates);
                break;
            case "3":
                Fligh2BoarWStatus(T5.Flights, T5.BoardingGates, T5.Airlines);
                break;
            case "4":
                CreateNewFlight(T5.Flights, T5.Airlines,T5);
                break;
            case "5":
                FullAirlineFlightDetails(T5.Flights, T5.Airlines);
                break;
            case "6":
                ModifyFlight(T5.Flights,T5.Airlines,T5.BoardingGates, T5);
                break;
            case "7":
                SortedFlightInfo(T5.Flights, T5.Airlines, T5.BoardingGates);
                break;
            case "8":
                BulkAssignFligh2BoardG(T5.Flights, T5.BoardingGates,T5.Airlines);
                break;
            case "9":
                T5.PrintAirlineFees(T5.Airlines,T5.Flights, T5.BoardingGates);
                break;
            case "10":
                FlightSearching(FDictionary);
                break;
            case "0":
                Console.WriteLine("Goodbye!");
                return;  // Exits the function, stopping the loop
        }
    }
}






Final();


CreateNewFlight(T5.Flights, T5.Airlines);

