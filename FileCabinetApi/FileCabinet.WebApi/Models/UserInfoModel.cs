using System.Collections.Generic;

namespace FileCabinet.WebApi.Models
{
    public class UserInfoModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<FileInfoModel> Bookmarks { get; set; }
    }
}