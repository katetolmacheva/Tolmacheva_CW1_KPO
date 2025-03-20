namespace Tolmacheva_KPO_CW1
{
  public class BankAccount
  {
    private int _id;
    private string _name;
    private decimal _balance;

    public BankAccount(int id, string name, decimal balance)
    {
      _id = id;
      _name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null");
      _balance = balance;
    }

    public int Id => _id;

    public string Name
    {
      get => _name;
      set => _name = value ?? throw new ArgumentNullException(nameof(value), "Name cannot be null");
    }

    public decimal Balance => _balance;

    public void UpdateBalance(decimal amount)
    {
      if (_balance + amount < 0)
        throw new InvalidOperationException("Insufficient funds");

      _balance += amount;
    }
  }
}
