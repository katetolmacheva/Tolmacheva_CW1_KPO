using System;
using System.Collections.Generic;

namespace Tolmacheva_KPO_CW1
{
  public interface IExportVisitor
  {
    void Export(JsonDataHandler jsonHandler, string filePath, List<Operation> operations);
    void Export(CsvDataHandler csvHandler, string filePath, List<Operation> operations);
  }

  public class ExportVisitor : IExportVisitor
  {
    public void Export(JsonDataHandler jsonHandler, string filePath, List<Operation> operations)
    {
      jsonHandler.ProcessFile(filePath, operations);
    }

    public void Export(CsvDataHandler csvHandler, string filePath, List<Operation> operations)
    {
      csvHandler.ProcessFile(filePath, operations);
    }
  }
}
