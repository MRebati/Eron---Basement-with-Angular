using System;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Base
{
    public class UserMessage : Entity<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string EmailAddress { get; set; }

        public bool Seen { get; set; }
    }
}