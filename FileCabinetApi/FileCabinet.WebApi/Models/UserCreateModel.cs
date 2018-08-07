using System.Collections.Generic;

namespace FileCabinet.WebApi.Models
{
    public class UserCreateModel
    {
        public string Name { get; set; }

        public ICollection<TagInfoModel> Tags { get; set; }
    }
}