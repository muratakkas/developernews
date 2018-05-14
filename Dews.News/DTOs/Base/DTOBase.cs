using AutoMapper;
using Dews.DataAccess.Interfaces;
using Dews.News.Entities.Base;
using Dews.News.Entities.Base.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.DTOs.Base
{
    public abstract class DTOBase
    {
        public TEntityInterfaceType MapToEntity<TEntityInterfaceType>(Type realEntityType)
        {
            return (TEntityInterfaceType)Mapper.Map(this, this.GetType(), realEntityType);
        }
        public TEntityInterfaceType ToEntity<TEntityInterfaceType>(IEntityManager entityManager)
        {
            return (TEntityInterfaceType)ToEntityObject<TEntityInterfaceType>(entityManager);
        }

        public virtual object ToEntityObject<TEntityInterfaceType>(IEntityManager entityManager)
        {
            var realEntitiyType = entityManager.GetRealType<TEntityInterfaceType>();
            return MapToEntity<TEntityInterfaceType>(realEntitiyType);
        }
    }
}
