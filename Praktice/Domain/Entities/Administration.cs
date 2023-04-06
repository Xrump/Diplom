using System;
using System.Collections.Generic;

namespace Praktice.Domain.Entities
{
    public partial class Administration
    {
        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public int? Account { get; set; }

        public virtual Account? AccountNavigation { get; set; }
    }
}
