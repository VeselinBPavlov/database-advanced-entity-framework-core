namespace BillPaymentSystem.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Commands.Contracts;
    using BillPaymentSystem.Data;
    using Contracts;
    using BillPaymentSystem.App.Core.Attributes;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Command";

        private IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider provider)
        {
            this.serviceProvider = provider;
        }

        public ICommand InterpretCommand(string[] args)
        {
            string commandType = args[0];
            string[] commandArgs = args.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandType + Suffix);

            if (type == null)
            {
                throw new ArgumentNullException("Command not found!");
            }

            if (!typeof(ICommand).IsAssignableFrom(type))
            {
                throw new ArgumentException(type + " is not a valid command!");
            }

            PropertyInfo[] propertiesToInject = type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttributes<InjectAttribute>().Any()).ToArray();

            var injectProps = propertiesToInject
                .Select(p => serviceProvider.GetService(p.PropertyType))
                .ToArray();

            var joinedParams = new object[] { commandArgs }.Concat(injectProps).ToArray();

            ICommand command = (ICommand)Activator.CreateInstance(type, joinedParams);
            return command;
        }
    }
}
