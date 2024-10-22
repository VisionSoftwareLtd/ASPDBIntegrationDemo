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
  [ProducesResponseType(StatusCodes.Status200OK)]
  public ActionResult<List<Item>> Items()
  {
    List<Item> items = Database.Instance.GetItems();
    return items;
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public ActionResult Item(Item item)
  {
    Database.Instance.AddItem(item);
    return Ok();
  }

  [HttpDelete]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public ActionResult DeleteItem(Item item)
  {
    Database.Instance.DeleteItem(item);
    return Ok();
  }

  [HttpPut]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public ActionResult UpdateItem(UpdateItemRequest updateItemRequest)
  {
    Database.Instance.UpdateItem(updateItemRequest.NameFrom, updateItemRequest.NameTo);
    return Ok();
  }
}

public record UpdateItemRequest
{
  public required string NameFrom { get; set; }
  public required string NameTo { get; set; }
}