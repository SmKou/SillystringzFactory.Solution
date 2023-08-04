namespace Factory.Models;

public class FactoryContext : DbContext
{
    DbSet<Engineer> Engineers { get; set; }
    DbSet<Machine> Machines { get; set; }
    DbSet<EngineerMachine> EngineerMachines { get; set; }
    
    public FactoryContext(DbContextOptions options) : base(options)
    {}
}