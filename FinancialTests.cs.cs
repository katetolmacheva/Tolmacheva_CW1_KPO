using Xunit;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tolmacheva_KPO_CW1
{
  public class FinancialTests
  {
    [Fact]
    public void AddAccount_ShouldIncreaseAccountsCount()
    {
      var facade = new FinancialFacade();
      facade.AddAccount(new BankAccount(1, "Main Account", 1000));
      Assert.Equal(1, facade.GetAccountsCount());
    }

    [Fact]
    public void AddOperation_ShouldUpdateAccountBalance()
    {
      var facade = new FinancialFacade();
      var account = new BankAccount(1, "Savings", 1000);
      facade.AddAccount(account);

      var operation = new Operation(1, "Expense", 1, 200, DateTime.Now, "Groceries", 1);
      facade.AddOperation(operation);

      Assert.Equal(800, facade.GetAccountBalance(1));
    }

    [Fact]
    public void AddOperation_ShouldThrowException_IfAccountNotFound()
    {
      var facade = new FinancialFacade();
      var operation = new Operation(1, "Income", 1, 500, DateTime.Now, "Salary", 1);

      Assert.Throws<InvalidOperationException>(() => facade.AddOperation(operation));
    }

    [Fact]
    public void GetAccountBalance_ShouldThrowException_IfAccountDoesNotExist()
    {
      var facade = new FinancialFacade();
      Assert.Throws<InvalidOperationException>(() => facade.GetAccountBalance(999));
    }

    [Fact]
    public void CreateOperation_ShouldReturnValidOperation()
    {
      var operation = FinancialFactory.CreateOperation(1, "Income", 1, 100, DateTime.Now, "Bonus", 1);

      Assert.NotNull(operation);
      Assert.Equal(1, operation.Id);
      Assert.Equal("Income", operation.Type);
      Assert.Equal(100, operation.Amount);
    }

    [Fact]
    public void CreateOperation_ShouldThrowException_IfNegativeAmount()
    {
      Assert.Throws<ArgumentException>(() =>
          FinancialFactory.CreateOperation(2, "Expense", 1, -50, DateTime.Now, "Fine", 1));
    }

    [Fact]
    public void ExportJson_ShouldCreateFile()
    {
      var visitor = new ExportVisitor();
      var jsonHandler = new JsonDataHandler();
      var operations = new List<Operation>
            {
                new Operation(1, "Income", 1, 100, DateTime.Now, "Freelance", 1)
            };

      visitor.Export(jsonHandler, "test_data/test_export.json", operations);
      Assert.True(File.Exists("test_data/test_export.json"));

      File.Delete("test_data/test_export.json");
    }

    [Fact]
    public void ExportCsv_ShouldCreateFile()
    {
      var visitor = new ExportVisitor();
      var csvHandler = new CsvDataHandler();
      var operations = new List<Operation>
            {
                new Operation(1, "Expense", 1, 50, DateTime.Now, "Dinner", 1)
            };

      visitor.Export(csvHandler, "test_data/test_export.csv", operations);
      Assert.True(File.Exists("test_data/test_export.csv"));

      File.Delete("test_data/test_export.csv");
    }

    [Fact]
    public void ImportJson_ShouldLoadData()
    {
      var jsonContent = "[{\"Id\":1,\"Type\":\"Income\",\"BankAccountId\":1,\"Amount\":500,\"Date\":\"2025-03-20T00:00:00\",\"Description\":\"Salary\",\"CategoryId\":1}]";
      File.WriteAllText("test_data/test_import.json", jsonContent);

      var operations = DataImporter.ImportFromJson("test_data/test_import.json");
      Assert.Single(operations);
      Assert.Equal(500, operations[0].Amount);

      File.Delete("test_data/test_import.json");
    }

  }
}
