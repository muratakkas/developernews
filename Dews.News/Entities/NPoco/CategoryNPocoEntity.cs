using Dews.News.DTOs;
using Dews.News.Entities.Base;
using NPoco;
using System;

namespace Dews.News.Entities.NPoco
{
    [TableName("CATEGORIES")]
    [PrimaryKey("ID")]
    public class CategoryNPocoEntity : EntitiyBase, ICategoryEntity
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("ICONNAME")]
        public string IconName  { get; set; }

        [Column("PARENTID")]
        public int? ParentId { get; set; }

        [Column("CREATEUSER")]
        public Guid CreateUser { get; set; }

        public override object GetDTO<TDtoType>()
        { 
            return (CategoryDTO)base.GetDTO<CategoryDTO>(); ;
        }
    }
}
