using Dews.DataAccess.Interfaces;
using Dews.Shared.Resources.Enums;
using Dews.Shared.Resources.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dews.News.Entities.Base.Manager
{
    public abstract class EntityManager : IEntityManager
    {
        #region Fields

        private readonly Dictionary<Type, Type> entitiyMap = new Dictionary<Type, Type>();

        #endregion

        #region Constructor

        public EntityManager()
        {
            MapEntities();
        }

        #endregion


        #region Public

        public EntityManager Map<entityInterfaceType, entitiyType>()
        {
            if (!entitiyMap.ContainsKey(typeof(entityInterfaceType)))
                entitiyMap.Add(typeof(entityInterfaceType), typeof(entitiyType));
            return this;
        }

        public abstract void MapEntities();


        #endregion

        #region IEntityManager İmplements

        public TEntityInterfaceType Create<TEntityInterfaceType>()
        {
            Type realType = GetRealType<TEntityInterfaceType>();
            //Create an instance of target type
            object newEntity = Activator.CreateInstance(realType);
            return (TEntityInterfaceType)newEntity;
        }

        public Type GetRealType<TEntityInterfaceType>()
        {
            Type interfacetype = typeof(TEntityInterfaceType);
            //Check dictionary contains given type
            Type entityType = entitiyMap.Where(itm => itm.Key.FullName == interfacetype.FullName).Select(itm => itm.Value).FirstOrDefault();
            if (entityType == null) throw new Exception(string.Format(ResourcesEnum.TypeNotFoundInEntitiyManager.Translate(), interfacetype.FullName));

            return entityType;
        }

        #endregion

    }
}
