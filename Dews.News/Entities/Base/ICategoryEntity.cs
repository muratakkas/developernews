using Dews.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.Entities.Base
{
    public interface ICategoryEntity : IDatabaseEntity
    {
        int Id { get; set; }

        string Name { get; set; }

        byte[] Icon { get; set; }

        int? ParentId { get; set; }

        Guid CreateUser { get; set; }
    }
}
