using System;
using System.Collections.Generic;

namespace Tolmacheva_KPO_CW1
{
  class Program
  {
    static void Main(string[] args)
    {
      var facade = new FinancialFacade();
      var analytics = new FinancialAnalytics(facade.GetOperations());

      while (true)
      {
        Console.WriteLine("\nВыберите действие:");
        Console.WriteLine("1. Создать счет");
        Console.WriteLine("2. Создать категорию");
        Console.WriteLine("3. Создать операцию");
        Console.WriteLine("4. Показать баланс счета");
        Console.WriteLine("5. Подсчитать разницу доходов и расходов");
        Console.WriteLine("6. Группировка по категориям");
        Console.WriteLine("7. Экспорт данных в JSON");
        Console.WriteLine("8. Экспорт данных в CSV");
        Console.WriteLine("9. Импорт данных из JSON");
        Console.WriteLine("10. Импорт данных из CSV");
        Console.WriteLine("11. Выйти");
        Console.Write("Введите номер команды: ");

        var choice = Console.ReadLine();
        try
        {
          switch (choice)
          {
            case "1":
              CreateAccount(facade);
              break;
            case "2":
              CreateCategory(facade);
              break;
            case "3":
              CreateOperation(facade);
              break;
            case "4":
              ShowAccountBalance(facade);
              break;
            case "5":
              Console.WriteLine($"Разница доходов и расходов: {analytics.CalculateIncomeExpenseDifference()}");
              break;
            case "6":
              foreach (var entry in analytics.GroupByCategory())
                Console.WriteLine($"Категория {entry.Key}: {entry.Value}");
              break;
            case "7":
              DataExporter.ExportToJson(facade.GetOperations(), "test_data/export.json");
              Console.WriteLine("Данные экспортированы в JSON.");
              break;
            case "8":
              DataExporter.ExportToCsv(facade.GetOperations(), "test_data/export.csv");
              Console.WriteLine("Данные экспортированы в CSV.");
              break;
            case "9":
              ImportJson(facade);
              break;
            case "10":
              ImportCsv(facade);
              break;
            case "11":
              return;
            default:
              Console.WriteLine("Ошибка: Неверный ввод. Попробуйте снова.");
              break;
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Ошибка: {ex.Message}");
        }
      }
    }

    static void CreateAccount(FinancialFacade facade)
    {
      try
      {
        Console.Write("Введите ID счета: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) throw new Exception("ID должен быть числом.");

        Console.Write("Введите название счета: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) throw new Exception("Название счета не может быть пустым.");

        Console.Write("Введите начальный баланс: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal balance)) throw new Exception("Баланс должен быть числом.");

        facade.AddAccount(new BankAccount(id, name, balance));
        Console.WriteLine("Счет создан успешно.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка: {ex.Message}");
      }
    }

    static void CreateCategory(FinancialFacade facade)
    {
      try
      {
        Console.Write("Введите ID категории: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) throw new Exception("ID категории должен быть числом.");

        Console.Write("Введите тип категории (Income/Expense): ");
        var type = Console.ReadLine();
        if (type != "Income" && type != "Expense") throw new Exception("Тип категории должен быть 'Income' или 'Expense'.");

        Console.Write("Введите название категории: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) throw new Exception("Название категории не может быть пустым.");

        facade.AddCategory(new Category(id, type, name));
        Console.WriteLine("Категория создана успешно.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка: {ex.Message}");
      }
    }

    static void CreateOperation(FinancialFacade facade)
    {
      try
      {
        Console.Write("Введите ID операции: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) throw new Exception("ID операции должен быть числом.");

        Console.Write("Введите тип операции (Income/Expense): ");
        var type = Console.ReadLine();
        if (type != "Income" && type != "Expense") throw new Exception("Тип операции должен быть 'Income' или 'Expense'.");

        Console.Write("Введите ID счета: ");
        if (!int.TryParse(Console.ReadLine(), out int bankAccountId)) throw new Exception("ID счета должен быть числом.");

        Console.Write("Введите сумму операции: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount)) throw new Exception("Сумма операции должна быть числом.");

        Console.Write("Введите дату операции (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime date)) throw new Exception("Дата введена неверно.");

        Console.Write("Введите описание операции: ");
        var description = Console.ReadLine();

        Console.Write("Введите ID категории: ");
        if (!int.TryParse(Console.ReadLine(), out int categoryId)) throw new Exception("ID категории должен быть числом.");

        facade.AddOperation(FinancialFactory.CreateOperation(id, type, bankAccountId, amount, date, description, categoryId));
        Console.WriteLine("Операция создана успешно.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка: {ex.Message}");
      }
    }

    static void ShowAccountBalance(FinancialFacade facade)
    {
      try
      {
        Console.Write("Введите ID счета: ");
        if (!int.TryParse(Console.ReadLine(), out int accountId))
          throw new Exception("ID счета должен быть числом.");

        var balance = facade.GetAccountBalance(accountId);
        Console.WriteLine($"Баланс счета: {balance}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка: {ex.Message}");
      }
    }

    static void ImportJson(FinancialFacade facade)
    {
      Console.Write("Введите имя JSON-файла для загрузки (например, test_import.json): ");
      var jsonFile = Console.ReadLine();
      var importedJson = DataImporter.ImportFromJson($"test_data/{jsonFile}");
      if (importedJson.Count > 0)
      {
        foreach (var op in importedJson)
          facade.AddOperation(op);
        Console.WriteLine($"Данные импортированы из {jsonFile}");
      }
      else Console.WriteLine($"Файл {jsonFile} пуст или содержит ошибки.");
    }

    static void ImportCsv(FinancialFacade facade)
    {
      Console.Write("Введите имя CSV-файла для загрузки (например, test_import.csv): ");
      var csvFile = Console.ReadLine();
      var importedCsv = DataImporter.ImportFromCsv($"test_data/{csvFile}");
      if (importedCsv.Count > 0)
      {
        foreach (var op in importedCsv)
          facade.AddOperation(op);
        Console.WriteLine($"Данные импортированы из {csvFile}");
      }
      else Console.WriteLine($"Файл {csvFile} пуст или содержит ошибки.");
    }
  }
}
