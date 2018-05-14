using Dews.DataAccess.Types;
using Dews.News.DTOs;
using Dews.News.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.Interfaces
{
    public interface INewsManager
    { 
        IEnumerable<NewsDTO> GetAll(ListRequestParam RequestParams);
        NewsDTO GetByID(int id);
        void Delete(int id);
        void Save(NewsDTO dto);

        void CheckIsUserAuthonticatedToEditDelete(Guid userId, NewsDTO dto);
    }
}
