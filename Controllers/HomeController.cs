using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDos.Data;
using ToDos.Models;

namespace ToDos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ListContext _db;

    public HomeController(ILogger<HomeController> logger, ListContext db)
    {
        _logger = logger;
        _db = db;
    }
    

      
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Index(string email, string password)
    {
        var user = _db.User.FirstOrDefault(x => x.email == email && x.password == password);
        if( user != null)
        {
            return RedirectToAction("Privacy","Home");

        }
        else
        {
            ViewBag.Error = "Email veya şifre hatalı!";
            return View();
        }
    }
[HttpPost]
public IActionResult DeactivateJob(int id)
{
    var job = _db.Jobs.FirstOrDefault(x => x.id == id);
    if (job != null)
    {
        job.finishTime = DateTime.Now;
        job.active = false;
        _db.SaveChanges();
    }
    return RedirectToAction("Privacy"); 
}

[HttpPost]
public IActionResult DeleteJob(int id)
{
    var job = _db.Jobs.FirstOrDefault(x => x.id == id);
    if (job != null)
    {
        job.deleted = true;
        _db.SaveChanges();
    }
    return RedirectToAction("Privacy"); 
}

[HttpPost]
public IActionResult RestartJob(int id)
{
    var job = _db.Jobs.FirstOrDefault(x => x.id == id);
    if (job != null)
    {
        job.active = true;
        job.finishTime = null;
        _db.SaveChanges();
    }
    return RedirectToAction("Privacy"); 
}
    public IActionResult Privacy()
    {
        var jobs = _db.Jobs.Where(x => x.deleted != true ).ToList();
        return View(jobs);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
