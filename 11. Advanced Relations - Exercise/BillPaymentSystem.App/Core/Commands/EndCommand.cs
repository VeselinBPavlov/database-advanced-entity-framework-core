namespace BillPaymentSystem.App.Core.Commands
{
    using System;

    public class EndCommand : Command
    {
        public EndCommand(string[] data)
            : base(data)
        {
        }

        public override string Execute()
        {
            Environment.Exit(0);
            return "Have a nice day!";
        }
    }
}
