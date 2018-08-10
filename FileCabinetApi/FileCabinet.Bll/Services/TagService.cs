﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileCabinet.Bll.Contracts.Dtos;
using FileCabinet.Bll.Contracts.Services;
using FileCabinet.Bll.Services.Base;
using FileCabinet.Dal.Contracts.Domain;
using FileCabinet.Dal.Contracts.Repositories;
using FileCabinet.Dal.Contracts.UoW;

namespace FileCabinet.Bll.Services
{
    public class TagService : PlainEntityService<TagDto, Tag>, ITagService
    {
        public TagService(IUnitOfWork unitOfWork, IRepository<Tag, int> repository, IMapper mapper)
            : base(unitOfWork, repository, mapper)
        {
        }
        
        public override int Create(TagDto tagDto)
        {
            if (tagDto == null) throw new ArgumentNullException(nameof(tagDto));

            var mappedTag = Mapper.Map<Tag>(tagDto);
            MakeValidTag(mappedTag);

            var duplicate = Repository.Find(tag => tag.Name == mappedTag.Name).FirstOrDefault();

            if (duplicate != null)
            {
                return duplicate.Id;
            }

            Repository.Create(mappedTag);
            UnitOfWork.SaveChanges();

            return mappedTag.Id;
        }

        public override async Task<int> CreateAsync(TagDto tagDto)
        {
            if (tagDto == null) throw new ArgumentNullException(nameof(tagDto));

            var mappedTag = Mapper.Map<Tag>(tagDto);
            MakeValidTag(mappedTag);

            var duplicate = (await Repository.FindAsync(tag => tag.Name == mappedTag.Name)).FirstOrDefault();

            if (duplicate != null)
            {
                return duplicate.Id;
            }

            await Repository.CreateAsync(mappedTag);
            await UnitOfWork.SaveChangesAsync();

            return mappedTag.Id;
        }

        private void MakeValidTag(Tag tag)
        {
            tag.Name = tag.Name.Trim(' ');
        }
    }
}