namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Dtos;
    using Utilities;
    using Services.Contracts;

    public class AddTagToCommand : ICommand
    {
        private IAlbumService albumService;
        private ITagService tagService;
        private IAlbumTagService albumTagService;

        public AddTagToCommand(IAlbumService albumService, ITagService tagService, IAlbumTagService albumTagService)
        {
            this.albumService = albumService;
            this.tagService = tagService;
            this.albumTagService = albumTagService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            string albumTitle = data[0];
            string tagName = data[1].ValidateOrTransform();

            var albumExists = this.albumService.Exists(albumTitle);
            var tagExists = this.tagService.Exists(tagName);

            if (!albumExists || !tagExists)
            {
                throw new ArgumentException("Either tag or album do not exists!");
            }

            var albumId = this.albumService.ByName<AlbumDto>(albumTitle).Id;
            var tagId = this.tagService.ByName<TagDto>(tagName).Id;

            var albumTag = this.albumTagService.AddTagTo(albumId, tagId);

            return $"Tag {tagName} added to {albumTitle}!";
        }
    }
}
