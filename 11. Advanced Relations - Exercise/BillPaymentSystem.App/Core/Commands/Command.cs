namespace BillPaymentSystem.App.Core.Commands
{
    using Contracts;

    public abstract class Command : ICommand
    {
        public Command(string[] data)
        {
            this.Data = data;
        }

        public string[] Data { get; protected set; }

        public abstract string Execute();
    }

}
