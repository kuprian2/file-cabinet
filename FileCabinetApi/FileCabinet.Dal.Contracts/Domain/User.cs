using FileCabinet.Dal.Contracts.Domain.Base;
using System.Collections.Generic;

namespace FileCabinet.Dal.Contracts.Domain
{
    public class User : Entity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<File> Bookmarks { get; set; }
    }
}
