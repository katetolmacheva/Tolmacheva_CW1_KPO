using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace Tolmacheva_KPO_CW1
{
  public class FinancialAnalytics
  {
    private readonly List<Operation> _operations;

    public FinancialAnalytics(List<Operation> operations)
    {
      _operations = operations;
    }

    public decimal CalculateIncomeExpenseDifference()
    {
      var income = _operations.Where(o => o.Type == "Income").Sum(o => o.Amount);
      var expense = _operations.Where(o => o.Type == "Expense").Sum(o => o.Amount);
      return income - expense;
    }

    public Dictionary<string, decimal> GroupByCategory()
    {
      return _operations.GroupBy(o => o.CategoryId)
          .ToDictionary(g => g.Key.ToString(), g => g.Sum(o => o.Amount));
    }
  }
}
