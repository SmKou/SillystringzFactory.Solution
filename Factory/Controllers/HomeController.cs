namespace Factory.Controllers;

public class HomeController : Controller
{
    private readonly FactoryContext _db;

    public HomeController(FactoryContext db)
    {
        _db = db;
    }

    public ActionResult Index()
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("Machines", _db.Machines.ToList());
        model.Add("Engineers", _db.Engineers.ToList());
        return View(model);
    }
}