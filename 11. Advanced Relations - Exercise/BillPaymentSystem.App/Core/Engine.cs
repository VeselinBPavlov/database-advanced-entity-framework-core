namespace BillPaymentSystem.App.Core
{
    using System;

    using BillPaymentSystem.Data;
    using Contracts;

    public class Engine : IEngine
    {
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string[] inputParams = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    using (BillPaymentSystemContext context = new BillPaymentSystemContext())
                    {
                        var command = this.commandInterpreter.InterpretCommand(inputParams);
                        var result = command.Execute();

                        Console.WriteLine(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }              
            }            
        }
    }
}
