using AutoMapper;
using Dews.DataAccess.Interfaces;
using Dews.DataAccess.Types;
using Dews.Logger.Interfaces;
using Dews.News.DTOs;
using Dews.News.Entities.Base;
using Dews.News.Entities.NPoco;
using Dews.News.Interfaces;
using Dews.Shared.Resources.Enums;
using Dews.Shared.Resources.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dews.News.Managers
{
    /// <summary>
    /// Manages  create , update , delete and list operations for News class
    /// </summary>
    public class CategoryManager : ICategoryManager
    {
        #region Fields

        /// <summary>
        /// Database accessor instance , should be set in constructor
        /// </summary>
        IDatabase DatabaseInstance;

        /// <summary>
        /// Logger accessor instance , should be set in constructor
        /// </summary>
        ILogger LoggerInstance;

        /// <summary>
        /// Creates entiities with using entiity interfaces
        /// </summary>
        IEntityManager EntityManager;

        #endregion

        #region Constructor

        public CategoryManager(IDatabase databaseInstance, IEntityManager entityManager, ILogger loggerInstance)
        {
            DatabaseInstance = databaseInstance;
            LoggerInstance = loggerInstance;
            EntityManager = entityManager;
            //Throw exception if parameters not assigned
            if (DatabaseInstance == null || LoggerInstance == null || EntityManager == null)
                throw new Exception(ResourcesEnum.DatabaseInstanceandLoggerMustBeProvided.Translate());
        }

        #endregion

        #region Private

        private void ValidateCategory(CategoryDTO dto)
        {
            //Check arguments
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            //Check Subject
            if (dto.Name.CheckIsNull()) throw new Exception(ResourcesEnum.NameMustBeEntered.Translate());

            //Check Content
            if (dto.Icon.CheckIsNull() && dto.IconName.CheckIsNull()) throw new Exception(ResourcesEnum.IconMustBeEntered.Translate());
        }

        #endregion

        #region Public

        public void Add(CategoryDTO category)
        {
            try
            {
                LoggerInstance.Log("Adding news");
                ValidateCategory(category);
                DatabaseInstance.Save(category.ToEntity<CategoryNPocoEntity>(EntityManager));
            }
            catch (Exception ex)
            {
                LoggerInstance.Log(ex);
                throw ex;
            }
        }

        public IEnumerable<CategoryDTO> GetAll(ListRequestParam RequestParams)
        {
            try
            {
                IEnumerable<ICategoryEntity> entitiyList = DatabaseInstance.Fetch<CategoryNPocoEntity>(RequestParams);
                List<CategoryDTO> dtoList = new List<CategoryDTO>();
                entitiyList.ToList().ForEach(itm =>
                {
                    dtoList.Add(itm.ToDTO<CategoryDTO>());
                });
                return dtoList;
            }
            catch (Exception ex)
            {
                LoggerInstance.Log(ex);
                throw ex;
            }
        }

        public CategoryDTO GetByID(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException(nameof(id));
                return DatabaseInstance.SingleById<CategoryNPocoEntity>(id).ToDTO<CategoryDTO>();
            }
            catch (Exception ex)
            {
                LoggerInstance.Log(ex);
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException(nameof(id));
                LoggerInstance.Log(string.Format("Deleting category id :", id));
                DatabaseInstance.Delete<CategoryNPocoEntity>(id);
            }
            catch (Exception ex)
            {
                LoggerInstance.Log(ex);
                if (ex.Message.Contains("FK_NEWS_CATEGORIES")) throw new Exception("This category includes news can not be deleted");
                else throw ex;
            }

        }

        public void Update(CategoryDTO dto)
        {
            try
            {
                LoggerInstance.Log("Updating category");
                ValidateCategory(dto);
                DatabaseInstance.Update(dto.ToEntity<ICategoryEntity>(EntityManager));
            }
            catch (Exception ex)
            {
                LoggerInstance.Log(ex);
                throw ex;
            }
        }

        public void CheckIsUserAuthonticatedToEditDelete(Guid userId, CategoryDTO dto)
        {
            if(!dto.Id.HasValue) dto.CreateUser = userId;
            else
            {
                CategoryDTO savedCategory = GetByID(dto.Id.Value); 
                if (savedCategory.CreateUser != userId) throw new UnauthorizedAccessException();
                dto.CreateUser = savedCategory.CreateUser;
            }  
        }

        #endregion
    }
}
