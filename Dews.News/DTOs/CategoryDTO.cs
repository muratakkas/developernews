﻿using Dews.News.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Dews.News.Entities.Base;
using Dews.News.Entities.NPoco;
using Dews.Shared.Resources.Extensions;

namespace Dews.News.DTOs
{
    public class CategoryDTO : DTOBase
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string Icon { get; set; }

        public Guid CreateUser { get; set; }

        public override object ToEntityObject<TEntityInterfaceType>(IEntityManager entityManager)
        {
            CategoryNPocoEntity cateoryNPocoEntity = (CategoryNPocoEntity)base.ToEntityObject<ICategoryEntity>(entityManager);
            if (!Icon.CheckIsNull())
                cateoryNPocoEntity.Icon = Convert.FromBase64String(Icon);

            return cateoryNPocoEntity;
        }
    }
}
