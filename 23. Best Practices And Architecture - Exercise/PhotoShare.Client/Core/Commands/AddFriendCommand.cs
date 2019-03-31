namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class AddFriendCommand : ICommand
    {
        private IUserService userService;

        public AddFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            string username = data[0];
            string friendUsername = data[1];

            var usenameExists = this.userService.Exists(username);

            if (usenameExists == false)
            {
                throw new ArgumentException($"{username} not found!");
            }

            var friendExists = this.userService.Exists(friendUsername);

            if (friendExists == false)
            {
                throw new ArgumentException($"{friendUsername} not found!");
            }

            var user = this.userService.ByUsername<UserFriendsDto>(username);
            var friend = this.userService.ByUsername<UserFriendsDto>(friendUsername);

            bool isSendRequestFromUser = user.Friends.Any(x => x.Username == friend.Username);
            bool isSendRequestFromFriend = friend.Friends.Any(x => x.Username == user.Username);

            if (isSendRequestFromUser && isSendRequestFromFriend)
            {
                throw new InvalidOperationException($"{friendUsername} is already a friend to {username}");
            }
            else if (isSendRequestFromUser && !isSendRequestFromFriend)
            {
                throw new InvalidOperationException($"{friendUsername} has not added {username} as a friend");
            }
            else if (!isSendRequestFromUser && isSendRequestFromFriend)
            {
                throw new InvalidOperationException($"{username} has not added {friendUsername} as a friend");
            }

            this.userService.AddFriend(user.Id, friend.Id);

            return $"{username} accepted {friendUsername} as a friend";
        }
    }
}
