using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDos.Data;
using ToDos.Entity;
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
            return RedirectToAction("gorevlerim");

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
        return RedirectToAction("gorevlerim"); 
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
        return RedirectToAction("gorevlerim"); 
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
        return RedirectToAction("gorevlerim"); 
    }
    [Route("home/gorevlerim")]
    public IActionResult Privacy()
    {
        var viewModel = new ViewModel();  
        viewModel.Jobs = _db.Jobs.Where(x => x.deleted != true ).ToList();
        viewModel.isuser = _db.User.FirstOrDefault();
        return View(viewModel);
    }
    [HttpGet]
[Route("gorevekle")]
public IActionResult AddTask()
{
    var model = new ViewModel();
    model.isuser = _db.User.FirstOrDefault();
    return View(model);
}
    [HttpPost]
[Route("gorevekle")]
public IActionResult AddTask(string jobName, string jobTitle, string jobDescription, DateTime createTime, int priorityId)
{
    var newTask = new Jobs
    {
        jobName = jobName,
        jobTitle = jobTitle,
        jobDescription = jobDescription,
        createTime = DateTime.Now,
        priorityId = priorityId,
        active = true
    };

    _db.Jobs.Add(newTask);
    _db.SaveChanges();

    return RedirectToAction("Gorevlerim", "Home");
}

    
}
