using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class Machine
{
    public int MachineId { get; set; }
    [Required(ErrorMessage = "Provide name of machine.")]
    public string MachineName { get; set; }
    [Required(ErrorMessage = "Provide type of machine.")]
    public string MachineType { get; set; }

    public List<EngineerMachine> Engineers { get; set; }
}