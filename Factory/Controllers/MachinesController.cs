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

    // public ActionResult Details(int id) {}
    // public ActionResult AddEngineer(Machine machine, int engineerId) {}
    // public ActionResult Edit(int id) {}
    // [HttpPost] public ActionResult Edit(Machine machine) {}

    public ActionResult Delete(int id)
    {
        Machine machine = _db.Machines.FirstOrDefault(mach => mach.MachineId == id);
        _db.Machines.Remove(machine);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}