using System;
using System.Collections.Generic;

namespace Praktice.Domain.Entities
{
    public partial class Announcement
    {
        public int Id { get; set; }
        public int? Author { get; set; }
        public string? Text { get; set; }

        public virtual Account? AuthorNavigation { get; set; }
    }
}
