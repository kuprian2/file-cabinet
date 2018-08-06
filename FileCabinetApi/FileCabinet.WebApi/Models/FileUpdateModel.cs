using System.Collections.Generic;

namespace FileCabinet.WebApi.Models
{
    public class FileUpdateModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<TagModel> Tags { get; set; }
    }
}