using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class Engineer
{
    public int EngineerId { get; set; }
    [Required(ErrorMessage = "Engineer requires name for identification.")]
    public string EngineerName { get; set; }

    public List<EngineerMachine> Machines { get; set; }
}