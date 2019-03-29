namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class RegisterUserCommand : ICommand
    {
        private IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            var registerUserDto = new RegisterUserDto()
            {
                Username = username,
                Password = password,
                Email = email
            };

            if (!IsValid(registerUserDto))
            {
                throw new ArgumentException("Invalid data");
            }

            var userExists = this.userService.Exists(username);

            if (userExists)
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Password do not match");
            }

            var user = this.userService.Register(username, password, email);

            return $"User {username} was registered successfully!";
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
