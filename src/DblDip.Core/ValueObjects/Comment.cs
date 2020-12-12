using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DblDip.Core.ValueObjects
{
    public class Comment : ValueObject
    {
        public const int MaxLength = 250;
        public Email AuthorEmail { get; private set; }
        [JsonProperty]
        public string Body { get; private set; }

        protected Comment()
        {

        }

        private Comment(Email authorEmail, string body)
        {
            AuthorEmail = authorEmail;
            Body = body;
        }

        public static Result<Comment> Create(Email authorEmail, string body)
        {
            return Result.Success(new Comment(authorEmail, body));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AuthorEmail;
            yield return Body;
        }
    }
}
