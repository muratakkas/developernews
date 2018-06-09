using Dews.News.DTOs;
using Dews.News.Entities.Base; 
using NPoco;
using System;

namespace Dews.News.Entities.NPoco
{
    [TableName("NEWS")]
    [PrimaryKey("ID")]
    public class NewsNPocoEntity : EntitiyBase, INewsEntity
    {
        
        [Column("ID")]
        public int Id { get; set; }

        [Column("SUBJECT")]
        public string Subject { get; set; }

        [Column("CONTENT")]
        public string Content { get; set; } 

        [Column("CATEGORYID")]
        public int? CategoryId { get; set; }

        [Column("CREATEDATE")]
        public DateTimeOffset CreateDate { get; set; }

        [Column("ICONNAME")]
        public string IconName { get; set; }

        [Column("CREATEUSER")]
        public Guid CreateUser { get; set; }

        public override object GetDTO<TDtoType>()
        { 
            return (NewsDTO)base.GetDTO<NewsDTO>(); ;
        }

    }
}
