using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;

namespace Tolmacheva_KPO_CW1
{
  public class DataExporter
  {
    public static void ExportToJson(List<Operation> operations, string filePath)
    {
      if (operations == null || operations.Count == 0)
      {
        Console.WriteLine("Нет данных для экспорта.");
        return;
      }

      try
      {
        var json = JsonConvert.SerializeObject(operations, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine($"Данные экспортированы в {filePath}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка при экспорте JSON: {ex.Message}");
      }
    }

    public static void ExportToCsv(List<Operation> operations, string filePath)
    {
      if (operations == null || operations.Count == 0)
      {
        Console.WriteLine("Нет данных для экспорта.");
        return;
      }

      try
      {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
          csv.WriteRecords(operations);
        }
        Console.WriteLine($"Данные экспортированы в {filePath}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка при экспорте CSV: {ex.Message}");
      }
    }
  }

  public class DataImporter
  {
    public static List<Operation> ImportFromJson(string filePath)
    {
      if (!File.Exists(filePath))
      {
        Console.WriteLine($"Файл {filePath} не найден.");
        return new List<Operation>();
      }

      try
      {
        var json = File.ReadAllText(filePath);
        var operations = JsonConvert.DeserializeObject<List<Operation>>(json);
        return operations ?? new List<Operation>();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка при загрузке JSON: {ex.Message}");
        return new List<Operation>();
      }
    }

    public static List<Operation> ImportFromCsv(string filePath)
    {
      if (!File.Exists(filePath))
      {
        Console.WriteLine($"Файл {filePath} не найден.");
        return new List<Operation>();
      }

      try
      {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
          return new List<Operation>(csv.GetRecords<Operation>());
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка при загрузке CSV: {ex.Message}");
        return new List<Operation>();
      }
    }
  }
}
