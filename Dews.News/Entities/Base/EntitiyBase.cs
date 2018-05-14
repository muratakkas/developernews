using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.Entities.Base
{
    public abstract class EntitiyBase
    {
        public virtual TDtoType ToDTO<TDtoType>()
        {  
            return (TDtoType)GetDTO<TDtoType>();
        }

        public virtual object GetDTO<TDtoType>()
        {
            TDtoType dtoType = Activator.CreateInstance<TDtoType>();
            return  Mapper.Map(this, dtoType, this.GetType(), dtoType.GetType());
        }
    }
}
