using System.Collections.Generic;

namespace FileCabinet.WebApi.Models
{
    public class UserUpdateModel
    {
        public string Name { get; set; }

        public ICollection<FileInfoModel> Bookmarks { get; set; }
    }
}