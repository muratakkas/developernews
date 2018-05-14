using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.DataAccess.Interfaces
{
    public interface IDatabaseEntity
    {
        TDtoType ToDTO<TDtoType>();
    }
}
