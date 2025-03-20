using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;

namespace Tolmacheva_KPO_CW1
{
  public abstract class DataHandlerBase
  {
    public void ProcessFile(string filePath, List<Operation> operations)
    {
      if (string.IsNullOrWhiteSpace(filePath))
        throw new ArgumentException("Invalid file path");

      if (operations == null || operations.Count == 0)
      {
        Console.WriteLine("Нет данных для обработки.");
        return;
      }

      HandleFile(filePath, operations);
    }

    protected abstract void HandleFile(string filePath, List<Operation> operations);
  }

  public class JsonDataHandler : DataHandlerBase
  {
    protected override void HandleFile(string filePath, List<Operation> operations)
    {
      var json = JsonConvert.SerializeObject(operations, Formatting.Indented);
      File.WriteAllText(filePath, json);
      Console.WriteLine($"JSON-файл сохранён: {filePath}");
    }
  }

  public class CsvDataHandler : DataHandlerBase
  {
    protected override void HandleFile(string filePath, List<Operation> operations)
    {
      using (var writer = new StreamWriter(filePath))
      using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
      {
        csv.WriteRecords(operations);
      }
      Console.WriteLine($"CSV-файл сохранён: {filePath}");
    }
  }
}
