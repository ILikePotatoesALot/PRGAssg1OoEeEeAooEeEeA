//Q1)
//==========================================================
//Student Number : S10265753
//Student Name : Seth Lee
//Partner Name : Austin Goh
//==========================================================



using AssgCode;

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
