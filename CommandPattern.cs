namespace Tolmacheva_KPO_CW1
{
  public interface ICommand
  {
    void Execute();
  }

  public class AddOperationCommand : ICommand
  {
    private readonly FinancialFacade _facade;
    private readonly Operation _operation;

    public AddOperationCommand(FinancialFacade facade, Operation operation)
    {
      _facade = facade;
      _operation = operation;
    }

    public void Execute()
    {
      _facade.AddOperation(_operation);
    }
  }

  public class TimedCommandDecorator : ICommand
  {
    private readonly ICommand _command;

    public TimedCommandDecorator(ICommand command)
    {
      _command = command;
    }

    public void Execute()
    {
      var stopwatch = System.Diagnostics.Stopwatch.StartNew();
      _command.Execute();
      stopwatch.Stop();
      Console.WriteLine($"Command executed in {stopwatch.ElapsedMilliseconds} ms");
    }
  }
}
