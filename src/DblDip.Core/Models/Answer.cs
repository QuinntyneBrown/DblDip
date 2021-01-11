using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class Answer
    {
        public Guid QuestionId { get; set; }
        public int Value { get; set; }
        public Answer()
        {

        }

        public Answer(Guid questionId, int value)
        {
            QuestionId = questionId;
            Value = value;
        }
    }
}
