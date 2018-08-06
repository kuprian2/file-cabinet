using FileCabinet.Bll.Contracts.Dtos.Base;
using System.Collections.Generic;

namespace FileCabinet.Bll.Contracts.Dtos
{
    public class UserDto : EntityDto<int>
    {
        public string Name { get; set; }

        public ICollection<FileDto> Favourites { get; set; }

        public ICollection<FileDto> Bookmarks { get; set; }
    }
}