namespace Tolmacheva_KPO_CW1
{
  public class Category
  {
    private int _id;
    private string _type;
    private string _name;

    public Category(int id, string type, string name)
    {
      _id = id;
      _type = type ?? throw new ArgumentNullException(nameof(type), "Type cannot be null");
      _name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null");
    }

    public int Id => _id;

    public string Type => _type;

    public string Name
    {
      get => _name;
      set => _name = value ?? throw new ArgumentNullException(nameof(value), "Name cannot be null");
    }
  }
}
