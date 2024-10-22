using System.Buffers.Text;
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

  private bool Authenticate()
  {
    string authString = Request.Headers["Authorization"];
    byte[] data = Convert.FromBase64String(authString.Substring(6));
    string decodedString = System.Text.Encoding.UTF8.GetString(data);
    string[] strings = decodedString.Split(":");
    string username = strings[0];
    string password = strings[1];
    if (username == "Bob" && password == "password")
    {
      return true;
    }
    return false;
  }


  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public ActionResult<List<Item>> GetItems()
  {
    foreach (string key in Request.Headers.Keys)
    {
      Console.WriteLine($"{key} : {Request.Headers[key]}");
    }
    if (!Authenticate())
    {
      return Unauthorized();
    }
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