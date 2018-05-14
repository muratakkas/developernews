using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.Entities.Base
{
    public interface IEntityManager
    {
        TEntityInterfaceType Create<TEntityInterfaceType>();

        Type GetRealType<TEntityInterfaceType>();
    }
}
