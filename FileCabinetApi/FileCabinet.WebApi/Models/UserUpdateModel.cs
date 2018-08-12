using System.Collections.Generic;

namespace FileCabinet.WebApi.Models
{
    public class UserUpdateModel
    {
        public int Id { get; set; }

        public ICollection<FileInfoModel> Bookmarks { get; set; }
    }
}