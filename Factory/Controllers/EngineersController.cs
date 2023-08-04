using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers;

public class EngineersController : Controller
{
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
        _db = db;
    }

    public ActionResult Create() 
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
        if (!ModelState.IsValid)
            return View();

        _db.Engineers.Add(engineer);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    public ActionResult Details(int id) 
    {
        EngineerMachine join = new EngineerMachine();
        join.EngineerId = id;
        join.Engineer = _db.Engineers
            .Include(engr => engr.Machines)
            .ThenInclude(join => join.Machine)
            .FirstOrDefault(engr => engr.EngineerId == id);
        ViewBag.Machines = new SelectList(_db.Machines, "MachineId", "MachineName");
        return View(engineer);
    }

    public ActionResult AddMachine(EngineerMachine join)
    {
        bool hasRelation = _db.EngineerMachines
            .Any(join => join.EngineerId == join.EngineerId
            && join.MachineId == join.MachineId);
        if (!hasRelation)
        {
            _db.EngineerMachines.Add(join);
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = engineerMachine.EngineerId });
    }

    public ActionResult RemoveMachine(int engineerId, int machineId)
    {
        EngineerMachine join = _db.EngineerMachines
            .FirstOrDefault(join => join.EngineerId == engineerId
            && join.MachineId == machineId);
        _db.EngineerMachines.Remove(join);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = engineerId });
    }

    // public ActionResult Edit(int id) {}
    // [HttpPost] public ActionResult Edit(Engineer engineer) {}

    public ActionResult Delete(int id)
    {
        Engineer engineer = _db.Engineers.FirstOrDefault(engr => engr.EngineerId == id);
        _db.Engineers.Remove(engineer);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}