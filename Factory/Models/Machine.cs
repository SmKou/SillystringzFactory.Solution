namespace Factory.Models;

public class Machine
{
    public int MachineId { get; set; }
    public string MachineName { get; set; }
    public string MachineType { get; set; }

    public List<EngineerMachine> Engineers { get; set; }
}