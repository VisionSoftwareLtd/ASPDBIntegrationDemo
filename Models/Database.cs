using MySql.Data.MySqlClient;

public class Database
{
  private Database() { }

  public static Database Instance { get; } = new Database();

  private string? connectionString;

  public void SetConnectionString(string? connectionString)
  {
    this.connectionString = connectionString;
  }

  private List<Item> ErrorList(string message)
  {
    Item item = new Item();
    item.ItemName = message;
    return new List<Item>() { item };
  }

  private MySqlConnection GetOpenConnection()
  {
    if (connectionString == null)
    {
      throw new InvalidOperationException("Connection string not defined");
    }
    MySqlConnection conn = new MySqlConnection(connectionString);
    conn.Open();
    return conn;
  }

  public List<Item> GetItems()
  {
    try
    {
      MySqlConnection conn = GetOpenConnection();

      string sql = "SELECT * FROM Item;";
      MySqlCommand command = new MySqlCommand(sql, conn);
      MySqlDataReader reader = command.ExecuteReader();

      List<Item> items = new List<Item>();
      while (reader.Read())
      {
        Item item = new Item();
        item.ItemName = reader[0].ToString();
        items.Add(item);
      }
      reader.Close();
      conn.Close();
      return items;
    }
    catch (Exception exception)
    {
      return ErrorList(exception.Message);
    }
  }

  public void AddItem(Item item)
  {
    try
    {
      MySqlConnection conn = GetOpenConnection();

      string sql = "INSERT INTO Item (ItemName) VALUES (@itemName);";
      MySqlCommand command = new MySqlCommand(sql, conn);
      command.Parameters.AddWithValue("@itemName", item.ItemName);
      command.ExecuteNonQuery();

      conn.Close();
    }
    catch (Exception exception)
    {
      Console.Error.WriteLine(exception.Message);
    }
  }

  public void DeleteItem(Item item)
  {
    try
    {
      MySqlConnection conn = GetOpenConnection();

      string sql = "DELETE FROM Item WHERE ItemName=@itemName;";
      MySqlCommand command = new MySqlCommand(sql, conn);
      command.Parameters.AddWithValue("@itemName", item.ItemName);
      command.ExecuteNonQuery();

      conn.Close();
    }
    catch (Exception exception)
    {
      Console.Error.WriteLine(exception.Message);
    }
  }

  public void UpdateItem(string nameFrom, string nameTo)
  {
    try
    {
      MySqlConnection conn = GetOpenConnection();

      string sql = "UPDATE Item SET ItemName=@nameTo WHERE ItemName=@nameFrom;";
      MySqlCommand command = new MySqlCommand(sql, conn);
      command.Parameters.AddWithValue("@nameFrom", nameFrom);
      command.Parameters.AddWithValue("@nameTo", nameTo);
      command.ExecuteNonQuery();

      conn.Close();
    }
    catch (Exception exception)
    {
      Console.Error.WriteLine(exception.Message);
    }
  }
}