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

