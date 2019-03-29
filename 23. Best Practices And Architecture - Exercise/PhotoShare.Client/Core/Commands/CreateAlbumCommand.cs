namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Contracts;
    using Utilities;
    using Models.Enums;
    using Services.Contracts;
    using Dtos;

    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            string username = data[0];
            string albumTitle = data[1];
            string color = data[2];
            string[] tags = data.Skip(3).ToArray();

            var userExists = this.userService.Exists(username);

            if (userExists == false)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var albumExists = this.albumService.Exists(albumTitle);

            if (albumExists)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            bool isValidColor = Enum.TryParse<Color>(color, out Color result);

            if (isValidColor == false)
            {
                throw new ArgumentException($"Color {color} not found!");
            }
            
            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = tags[i].ValidateOrTransform();

                var currentTag = this.tagService.Exists(tags[i]);

                if (currentTag == false)
                {
                    throw new ArgumentException($"Invalid tag!");
                }
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;

            var album = this.albumService.Create(userId, albumTitle, color, tags);

            return $"Album {albumTitle} successfully created!";
        }
    }
}
