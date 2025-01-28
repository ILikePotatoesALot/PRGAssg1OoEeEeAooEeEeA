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

