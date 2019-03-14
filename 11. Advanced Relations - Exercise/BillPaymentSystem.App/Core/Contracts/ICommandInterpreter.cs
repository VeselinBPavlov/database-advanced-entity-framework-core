namespace BillPaymentSystem.App.Core.Contracts
{
    using BillPaymentSystem.App.Core.Commands.Contracts;

    public interface ICommandInterpreter
    {
        ICommand InterpretCommand(string[] args);
    }
}
