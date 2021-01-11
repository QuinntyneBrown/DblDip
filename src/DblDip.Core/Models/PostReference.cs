using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class PostReference
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }

        public PostReference(Guid postId, string title)
        {
            PostId = postId;
            Title = title;
        }

        public PostReference()
        {

        }
    }
}
