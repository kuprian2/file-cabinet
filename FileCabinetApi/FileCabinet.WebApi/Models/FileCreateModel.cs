using System.Collections.Generic;

namespace FileCabinet.WebApi.Models
{
    public class FileCreateModel
    {
        public string Name { get; set; }

        public long SizeInBytes { get; set; }

        public string Description { get; set; }

        public ICollection<TagInfoModel> Tags { get; set; }
    }
}