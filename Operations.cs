using System;

namespace Tolmacheva_KPO_CW1
{
  public class Operation
  {
    private int _id;
    private string _type;
    private int _bankAccountId;
    private decimal _amount;
    private DateTime _date;
    private string _description;
    private int _categoryId;

    public Operation(int id, string type, int bankAccountId, decimal amount, DateTime date, string description, int categoryId)
    {
      _id = id;
      _type = type ?? throw new ArgumentNullException(nameof(type), "Type cannot be null");
      _bankAccountId = bankAccountId;
      _amount = amount;
      _date = date;
      _description = description;
      _categoryId = categoryId;
    }

    public int Id => _id;

    public string Type => _type;

    public int BankAccountId => _bankAccountId;

    public decimal Amount => _amount;

    public DateTime Date => _date;

    public string Description
    {
      get => _description;
      set => _description = value ?? throw new ArgumentNullException(nameof(value), "Description cannot be null");
    }

    public int CategoryId => _categoryId;
  }
}