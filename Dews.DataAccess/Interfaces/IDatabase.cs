using Dews.DataAccess.Interfaces;
using Dews.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.DataAccess.Interfaces
{
    public interface IDatabase
    {
        void Save(IDatabaseEntity entity);

        IEnumerable<TDatabaseEntityRealType> Fetch<TDatabaseEntityRealType>(ListRequestParam RequestParams);

        TDatabaseEntityRealType SingleById<TDatabaseEntityRealType>(int Id);

        void Delete<TDatabaseEntityRealType>(int Id);

        void Update(IDatabaseEntity entityInstance);
         

    }
}
