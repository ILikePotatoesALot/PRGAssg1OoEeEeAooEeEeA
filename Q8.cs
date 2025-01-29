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
            Airline airline = T5.GetAirlineFromFlight(flighdict[i]);
            airline.RemoveFlight(flighdict[i]);
        }

    }
}
