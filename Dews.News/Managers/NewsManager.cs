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
    public class NewsManager : INewsManager
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

        public NewsManager(IDatabase databaseInstance, IEntityManager entityManager, ILogger loggerInstance)
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

        private void ValidateNews(NewsDTO newsDTO)
        {
            //Check arguments
            if (newsDTO == null) throw new ArgumentNullException(nameof(newsDTO));

            //Check Subject
            if (newsDTO.Subject.CheckIsNull()) throw new Exception(ResourcesEnum.SubjectMustBeEntered.Translate());

            //Check Content
            if (newsDTO.Content.CheckIsNull()) throw new Exception(ResourcesEnum.ContentMustBeEntered.Translate());
        }

        #endregion

        #region Public

         
        public IEnumerable<NewsDTO> GetAll(ListRequestParam RequestParams)
        {
            try
            {
                IEnumerable<INewsEntity> entitiyList = DatabaseInstance.Fetch<NewsNPocoEntity>(RequestParams);
                List<NewsDTO> dtoList = new List<NewsDTO>();
                entitiyList.ToList().ForEach(itm =>
                {
                    dtoList.Add(itm.ToDTO<NewsDTO>());
                });
                return dtoList;
            }
            catch (Exception ex)
            {
                LoggerInstance.Log(ex);
                throw ex;
            }
        }

        public NewsDTO GetByID(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException(nameof(id));
                return DatabaseInstance.SingleById<NewsNPocoEntity>(id).ToDTO<NewsDTO>();
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
                LoggerInstance.Log(string.Format("Deleting news id :", id));
                DatabaseInstance.Delete<NewsNPocoEntity>(id);
            }
            catch (Exception ex)
            {
                LoggerInstance.Log(ex);
                throw ex;
            }

        }

        public void Save(NewsDTO newsDTO)
        {
            try
            {
                LoggerInstance.Log("Saving news");
                ValidateNews(newsDTO); 
                DatabaseInstance.Save(newsDTO.ToEntity<INewsEntity>(EntityManager));
            }
            catch (Exception ex)
            {
                LoggerInstance.Log(ex);
                throw ex;
            }
        }

        
        public void CheckIsUserAuthonticatedToEditDelete(Guid userId, NewsDTO dto)
        {
            if (!dto.Id.HasValue) dto.CreateUser = userId;
            else
            {
                NewsDTO savedNews = GetByID(dto.Id.Value);
                if (savedNews.CreateUser != userId) throw new UnauthorizedAccessException();
                dto.CreateUser = savedNews.CreateUser;
            }
        }

        #endregion
    }
}
