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
    foreach (KeyValuePair<String,String> kv in airports)
        {
            airportsList.Add($"{kv.Value} ({kv.Key})");
        }
        string NewOrigin = "";
        string NewDestination = "";
        while (true)
        {
            Console.WriteLine("Enter Origin (e.g Bangkok (BKK))");
            NewOrigin  = Console.ReadLine();
            if (airportsList.Contains(NewOrigin) == false) { Console.WriteLine("Invalid Flight Origin."); continue; }
            Console.WriteLine("Enter Destination (e.g. Singapore (SIN))");
            NewDestination = Console.ReadLine();
            if ((airportsList.Contains(NewDestination) == false)) { Console.WriteLine("Invalid Flight Destination."); continue; }
            if (NewDestination == NewOrigin) { Console.WriteLine("Origin and Destination Cannot Be the Same. "); continue; };
            if (NewDestination != "Singapore (SIN)" && NewOrigin != "Singapore (SIN)") { Console.WriteLine("Origin Or Destination Is Not Singapore (SIN)");  continue; }
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
