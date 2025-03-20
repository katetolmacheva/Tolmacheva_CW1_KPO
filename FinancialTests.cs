using Xunit;

namespace Tolmacheva_KPO_CW1
{
  public class FinancialTests
  {
    [Fact]
    public void TestOperationCreation()
    {
      var operation = FinancialFactory.CreateOperation(1, "Expense", 1, 50, DateTime.Now, "Coffee", 1);
      Assert.NotNull(operation);
    }

    [Fact]
    public void TestNegativeAmount()
    {
      Assert.Throws<ArgumentException>(() => FinancialFactory.CreateOperation(1, "Expense", 1, -50, DateTime.Now, "Coffee", 1));
    }

    [Fact]
    public void TestAccountBalanceUpdate()
    {
      var account = new BankAccount(1, "Main Account", 1000);
      account.UpdateBalance(500);
      Assert.Equal(1500, account.Balance);

      Assert.Throws<InvalidOperationException>(() => account.UpdateBalance(-2000));
    }

    [Fact]
    public void TestCategoryNameUpdate()
    {
      var category = new Category(1, "Expense", "Cafe");
      category.Name = "Restaurant";
      Assert.Equal("Restaurant", category.Name);

      Assert.Throws<ArgumentNullException>(() => category.Name = null);
    }
  }
}
