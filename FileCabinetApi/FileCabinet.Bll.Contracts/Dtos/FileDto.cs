using FileCabinet.Bll.Contracts.Dtos.Base;
using System;
using System.Collections.Generic;

namespace FileCabinet.Bll.Contracts.Dtos
{
    public class FileDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public DateTime UploadDate { get; set; }

        public long SizeInBytes { get; set; }

        public string Description { get; set; }

        public ICollection<TagDto> Tags { get; set; }

        public ICollection<UserDto> BookmarkedUsers { get; set; }

        public ICollection<UserDto> FavouritedUsers { get; set; }
    }
}
