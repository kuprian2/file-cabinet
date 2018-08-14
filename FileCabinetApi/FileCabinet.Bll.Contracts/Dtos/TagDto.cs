using FileCabinet.Bll.Contracts.Dtos.Base;
using System.Collections.Generic;

namespace FileCabinet.Bll.Contracts.Dtos
{
    public class TagDto : EntityDto<int>
    {
        public string Name { get; set; }

        public ICollection<FileDto> Files { get; set; }
    }
}