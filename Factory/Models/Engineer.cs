namespace Factory.Models;

public class Engineer
{
    public int EngineerId { get; set; }
    public string EngineerName { get; set; }

    public List<EngineerMachine> Machines { get; set; }
}