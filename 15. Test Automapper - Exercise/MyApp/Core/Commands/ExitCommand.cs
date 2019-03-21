namespace MyApp.Core.Commands
{
    using Contracts;
    using System;

    public class ExitCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}
