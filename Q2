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
