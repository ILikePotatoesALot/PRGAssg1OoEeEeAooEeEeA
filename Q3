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
