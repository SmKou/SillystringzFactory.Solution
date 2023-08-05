using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers;

public class MachinesController : Controller
{
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
        _db = db;
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
        if (!ModelState.IsValid)
            return View();

        _db.Machines.Add(machine);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    public ActionResult Details(int id) 
    {
        EngineerMachine em = new EngineerMachine();
        em.MachineId = id;
        em.Machine = _db.Machines
            .Include(mach => mach.Engineers)
            .ThenInclude(join => join.Engineer)
            .FirstOrDefault(mach => mach.MachineId == id);
        if (_db.Engineers.Count() != 0)
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
        return View(em);
    }

    [HttpPost]
    public ActionResult AddEngineer(EngineerMachine em) 
    {
        bool hasRelation = _db.EngineerMachines
            .Any(join => join.MachineId == em.MachineId
            && join.EngineerId == em.EngineerId);
        if (!hasRelation)
        {
            _db.EngineerMachines.Add(em);
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = em.MachineId });
    }

    [HttpPost]
    public ActionResult RemoveEngineer(int joinId)
    {
        EngineerMachine em = _db.EngineerMachines
            .FirstOrDefault(join => join.EngineerMachineId == joinId);
        int mId = em.MachineId;
        _db.EngineerMachines.Remove(em);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = mId });
    }

    public ActionResult Edit(int id) 
    {
        Machine machine = _db.Machines
            .FirstOrDefault(mach => mach.MachineId == id);
        return View(machine);
    }
    
    [HttpPost]
    public ActionResult Edit(Machine machine) 
    {
        _db.Machines.Update(machine);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult Delete(int id)
    {
        Machine machine = _db.Machines.FirstOrDefault(mach => mach.MachineId == id);
        _db.Machines.Remove(machine);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}