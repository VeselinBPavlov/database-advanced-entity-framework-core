namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class ShareAlbumCommand : ICommand
    {
        private IAlbumRoleService albumRoleService;
        private IAlbumService albumService;
        private IUserService userService;

        public ShareAlbumCommand(IAlbumRoleService albumRoleService, IAlbumService albumService, IUserService userService)
        {
            this.albumRoleService = albumRoleService;
            this.albumService = albumService;
            this.userService = userService;
        }

        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permission = data[2];

            bool albumExists = this.albumService.Exists(albumId);

            if (albumExists == false)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            bool userExists = this.userService.Exists(username);

            if (userExists == false)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (permission != "Owner" && permission != "Viewer")
            {
                throw new ArgumentException(@"Permission must be either ""Owner"" or ""Viewer""!");
            }

            var user = this.userService.ByUsername<UserDto>(username);
            var album = this.userService.ById<AlbumDto>(albumId);


            this.albumRoleService.PublishAlbumRole(albumId, user.Id, permission);

            return $"Username {user.Username} added to album {album.Name} (${permission})";
        }
    }
}
