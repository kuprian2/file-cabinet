using FileCabinet.Dal.Contracts.Domain.Base;
using System;
using System.Collections.Generic;

namespace FileCabinet.Dal.Contracts.Domain
{
    public class File : Entity<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public DateTime UploadDate { get; set; }

        public long SizeInBytes { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<User> BookmarkedUsers { get; set; }
    }
}
