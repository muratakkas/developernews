using System;
using System.Collections.Generic;
using NPoco;
using System.Data.SqlClient;
using Dews.DataAccess.Interfaces;
using Dews.DataAccess.Types;
using System.Linq;
using System.Data.Common;
using System.Reflection;
using static NPoco.SqlBuilder;

namespace Dews.DataAccess.NPocoDB.Types
{
    public class NPocoDatabase : Dews.DataAccess.Interfaces.IDatabase
    {
        #region Fields

        private string ConnectionString;
        private NPoco.IDatabase DatabaseInstance;
        private const string SqlWhereParamFormat = "{0}=@{1}";
        private const string SqlWhereParamISNULLFormat = "{0} IS NULL";
        #endregion

        #region Constructor

        public NPocoDatabase(string connectionString)
        {
            ConnectionString = connectionString;

            if (ConnectionString == null || ConnectionString.Length <= 0) throw new ArgumentNullException(nameof(connectionString));
            DatabaseInstance = new Database(ConnectionString, DatabaseType.SqlServer2008, SqlClientFactory.Instance);
        }

        private NPoco.IDatabase GetDataBase()
        { 
            return DatabaseInstance;
        }
        #endregion

        #region Private

        private Template ConvertRequestParamToSqlTemplate(ListRequestParam requestParams)
        {
            var sqlBuilder = new SqlBuilder();
            int counter_parameters = 0;
            requestParams.Criterias.ForEach(cr =>
            {
                if(cr.Value == null) sqlBuilder.Where(string.Format(SqlWhereParamISNULLFormat, cr.Name), new List<object>() {   });
                else
                {
                    sqlBuilder.Where(string.Format(SqlWhereParamFormat, cr.Name, counter_parameters), new List<object>() { cr.Value });
                    counter_parameters++;
                } 
            });
            return sqlBuilder.AddTemplate("where  /**where**/", new List<object>());
        }

        #endregion

        #region IDatabase Implamantation

        public void Delete<TEntityType>(int Id)
        {
            using (NPoco.IDatabase db = GetDataBase())
            {
                db.Delete<TEntityType>(Id);
            }
        }
        public IEnumerable<TEntityType> Fetch<TEntityType>(ListRequestParam requestParams)
        {
            using (NPoco.IDatabase db = GetDataBase())
            { 
                return db.SkipTake<TEntityType>(requestParams.CurrentPage * requestParams.PageCount, requestParams.PageCount, ConvertRequestParamToSqlTemplate(requestParams));
            }
        }

        public void Save(IDatabaseEntity entity)
        {
            using (NPoco.IDatabase db = GetDataBase())
            {
                db.Save(entity);
            }
        }

        public TEntityType SingleById<TEntityType>(int Id)
        {
            using (NPoco.IDatabase db = GetDataBase())
            {
                return db.SingleById<TEntityType>(Id);
            }
        }

        public void Update(IDatabaseEntity entityInstance)
        {
            using (NPoco.IDatabase db = GetDataBase())
            {
                db.Save<IDatabaseEntity>(entityInstance);
            }

        }

        #endregion 
    }
}
