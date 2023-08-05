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
        EngineerMachine em = new EngineerMachine();
        em.EngineerId = id;
        em.Engineer = _db.Engineers
            .Include(engr => engr.Machines)
            .ThenInclude(join => join.Machine)
            .FirstOrDefault(engr => engr.EngineerId == id);
        if (_db.Machines.Count() != 0)
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
        return View(em);
    }

    [HttpPost]
    public ActionResult AddMachine(EngineerMachine em)
    {
        bool hasRelation = _db.EngineerMachines
            .Any(join => join.EngineerId == em.EngineerId
            && join.MachineId == em.MachineId);
        if (!hasRelation)
        {
            _db.EngineerMachines.Add(em);
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = em.EngineerId });
    }

    [HttpPost]
    public ActionResult RemoveMachine(int joinId)
    {
        EngineerMachine em = _db.EngineerMachines
            .FirstOrDefault(join => join.EngineerMachineId == joinId);
        int eId = em.EngineerId;
        _db.EngineerMachines.Remove(em);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = eId });
    }

    public ActionResult Edit(int id) 
    {
        Engineer engineer = _db.Engineers
            .FirstOrDefault(engr => engr.EngineerId == id);
        return View(engineer);
    }
    
    [HttpPost]
    public ActionResult Edit(Engineer engineer) 
    {
        _db.Engineers.Update(engineer);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Delete(int id)
    {
        Engineer engineer = _db.Engineers.FirstOrDefault(engr => engr.EngineerId == id);
        _db.Engineers.Remove(engineer);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}