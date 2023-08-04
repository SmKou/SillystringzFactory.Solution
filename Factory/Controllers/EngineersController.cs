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

    // public ActionResult Details(int id) {}
    // public ActionResult AddMachine(Engineer engineer, int machineId) {}
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