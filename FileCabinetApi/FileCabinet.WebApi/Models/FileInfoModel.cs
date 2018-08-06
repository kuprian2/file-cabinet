using System;
using System.Collections.Generic;

namespace FileCabinet.WebApi.Models
{
    public class FileInfoModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public DateTime UploadDate { get; set; }

        public uint SizeInBytes { get; set; }

        public string Description { get; set; }

        public ICollection<TagModel> Tags { get; set; }
    }
}