using System;
using TaskProActive.Models;
using TaskProActive.DTO;

namespace TaskProActive.Mapper
{
    public static class TagMapper
    {
        public static TagDto ToDto(Tag tag)
        {
            if (tag == null) return new TagDto();

            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name,
                UserId = tag.UserId,
                CreatedOn = tag.CreatedOn,
                CreatedBy = tag.CreatedBy ?? 0,
                ModifiedOn = tag.ModifiedOn ?? DateTime.MinValue,
                ModifiedBy = tag.ModifiedBy ?? 0
            };
        }

        public static Tag ToModel(TagDto dto)
        {
            if (dto == null) return new Tag();

            return new Tag
            {
                Id = dto.Id,
                Name = dto.Name,
                UserId = dto.UserId,
                CreatedOn = dto.CreatedOn,
                CreatedBy = dto.CreatedBy,
                ModifiedOn = dto.ModifiedOn,
                ModifiedBy = dto.ModifiedBy
            };
        }
    }
}
