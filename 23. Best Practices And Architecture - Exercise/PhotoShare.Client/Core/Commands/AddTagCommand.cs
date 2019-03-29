namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;
    using Dtos;
    using Utilities;
    using Services.Contracts;

    public class AddTagCommand : ICommand
    {
        private ITagService tagService;

        public AddTagCommand(ITagService tagService)
        {
            this.tagService = tagService;
        }

        // AddTag <tagName>
        public string Execute(string[] data)
        {
            var tagName = data[0];

            var tagExists = this.tagService.Exists(tagName);

            if (tagExists)
            {
                throw new ArgumentException($"Tag {tagName} exists!");
            }

            tagName = tagName.ValidateOrTransform();

            var tag = this.tagService.AddTag(tagName);

            return $"Tag {tagName} was added successfully!";
        }
    }
}
