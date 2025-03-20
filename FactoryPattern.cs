namespace Tolmacheva_KPO_CW1
{
  public static class FinancialFactory
  {
    public static Operation CreateOperation(int id, string type, int bankAccountId, decimal amount, DateTime date, string description, int categoryId)
    {
      if (amount < 0)
        throw new ArgumentException("Amount cannot be negative");

      return new Operation(id, type, bankAccountId, amount, date, description, categoryId);
    }
  }
}
