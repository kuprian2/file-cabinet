using FileCabinet.Dal.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace FileCabinet.Dal.EF
{
    public class FileCabinetContextInitializer : DropCreateDatabaseAlways<FileCabinetDbContext>
    {
        protected override void Seed(FileCabinetDbContext context)
        {
            var tags = new List<Tag>
            {
                new Tag {Name = "csharp"},
                new Tag {Name = "javascript"},
                new Tag {Name = "video"},
                new Tag {Name = "book"},
                new Tag {Name = "forBeginners"},
                new Tag {Name = "forAdvanced"}
            };

            context.Tags.AddRange(tags);
            context.SaveChanges();
        }
    }
}
