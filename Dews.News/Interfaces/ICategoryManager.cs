using Dews.DataAccess.Types;
using Dews.News.DTOs;
using Dews.News.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.Interfaces
{
    public interface ICategoryManager
    {
        void Add(CategoryDTO dto);
        IEnumerable<CategoryDTO> GetAll(ListRequestParam RequestParams);
        CategoryDTO GetByID(int id);
        void Delete(int id);
        void Update(CategoryDTO dto);

        void CheckIsUserAuthonticatedToEditDelete(Guid userId, CategoryDTO dto);
    }
}
