using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPDBIntegrationDemo.Models;

namespace ASPDBIntegrationDemo.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly IConfiguration _configuration;

  public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
  {
    _logger = logger;
    _configuration = configuration;
  }

  public IActionResult Index()
  {
    Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
    return View(Database.Instance.GetItems());
  }

  public IActionResult Privacy()
  {
    return View();
  }

  public IActionResult CreateItemResult(Item item)
  {
    Database.Instance.AddItem(item);
    return View(item);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
