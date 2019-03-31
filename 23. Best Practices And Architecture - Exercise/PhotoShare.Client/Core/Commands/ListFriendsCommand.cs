namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Text;

    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class ListFriendsCommand : ICommand
    {
        private IUserService userService;

        public ListFriendsCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // ListFriends <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            bool userExists = this.userService.Exists(username);

            if (userExists == false)
            {
                throw new ArgumentException($"User {username} not found");
            }

            var user = this.userService.ByUsername<UserFriendsDto>(username);

            if (user.Friends.Count == 0)
            {
                return "No friends for this user. :(";
            }
            
            var sb = new StringBuilder();
            sb.AppendLine("Friends:");

            foreach (var friend in user.Friends)
            {
                sb.AppendLine($"-{friend.Username}");
            }
            
            return sb.ToString().TrimEnd();
        }
    }
}
