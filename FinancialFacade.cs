namespace Tolmacheva_KPO_CW1
{
  public class FinancialFacade
  {
    private readonly List<BankAccount> _accounts = new();
    private readonly List<Category> _categories = new();
    private readonly List<Operation> _operations = new();

    public void AddAccount(BankAccount account)
    {
      if (account == null)
        throw new ArgumentNullException(nameof(account), "Account cannot be null");
      _accounts.Add(account);
    }

    public void AddCategory(Category category)
    {
      if (category == null)
        throw new ArgumentNullException(nameof(category), "Category cannot be null");
      _categories.Add(category);
    }

    public void AddOperation(Operation operation)
    {
      if (operation == null)
        throw new ArgumentNullException(nameof(operation), "Operation cannot be null");

      var account = _accounts.FirstOrDefault(a => a.Id == operation.BankAccountId);
      if (account == null)
        throw new InvalidOperationException("Bank account not found");

      decimal amount = operation.Type == "Income" ? operation.Amount : -operation.Amount;
      account.UpdateBalance(amount);

      _operations.Add(operation);
    }

    public decimal GetAccountBalance(int accountId)
    {
      var account = _accounts.FirstOrDefault(a => a.Id == accountId);
      if (account == null)
        throw new InvalidOperationException("Account not found");
      return account.Balance;
    }

    public List<Operation> GetOperations()
    {
      return _operations;
    }

    public int GetAccountsCount()
    {
      return _accounts.Count;
    }

  }
}
