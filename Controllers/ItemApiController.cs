using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ItemApiController : ControllerBase
{
  private readonly ILogger<ItemApiController> _logger;
  private readonly IConfiguration _configuration;

  public ItemApiController(ILogger<ItemApiController> logger, IConfiguration configuration)
  {
    _logger = logger;
    _configuration = configuration;
  }

  [HttpGet]
  [Route("/items")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public ActionResult<List<Item>> Items()
  {
    List<Item> items = Database.Instance.GetItems();
    return items;
  }
}