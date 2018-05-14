using Dews.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.News.Entities.Base
{
    public interface INewsEntity : IDatabaseEntity
    {
        int Id { get; set; }

        string Subject { get; set; }

        string Content { get; set; }

        byte[] Icon { get; set; }

        int? CategoryId { get; set; }

        DateTimeOffset CreateDate { get; set; }

        Guid CreateUser { get; set; }
    }
}
