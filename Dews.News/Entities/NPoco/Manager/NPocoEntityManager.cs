using AutoMapper;
using Dews.News.DTOs;
using Dews.News.Entities.Base;
using Dews.News.Entities.Base.Manager;
using Dews.News.Interfaces;
using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.Entities.NPoco.Manager
{
    public class NPocoEntityManager : EntityManager
    {
        public NPocoEntityManager() : base()
        {
        }

        public override void MapEntities()
        {
            //Map entity interfaces
            Map<INewsEntity, NewsNPocoEntity>();
            Map<ICategoryEntity, CategoryNPocoEntity>();

            //AutoMapper configuration
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<NewsNPocoEntity, NewsDTO>();
                cfg.CreateMap<CategoryNPocoEntity, CategoryDTO>();
            });
        }
    }
}
