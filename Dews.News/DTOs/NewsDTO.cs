using Dews.News.DTOs.Base;
using Dews.News.Entities.Base;
using Dews.News.Entities.NPoco;
using Dews.Shared.Resources.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.DTOs
{
    public class NewsDTO : DTOBase
    {
        public NewsDTO() : base()
        {
            CreateDate = DateTimeOffset.Now;
        }
        public int? Id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string Icon { get; set; }

        public int CategoryId { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public Guid CreateUser { get; set; }

        public override object ToEntityObject<TEntityInterfaceType>(IEntityManager entityManager)
        {
            NewsNPocoEntity newsNPocoEntity = (NewsNPocoEntity)base.ToEntityObject<INewsEntity>(entityManager);
            if (!Icon.CheckIsNull())
                newsNPocoEntity.Icon = Convert.FromBase64String(Icon);

            return newsNPocoEntity;
        }
    }
}
