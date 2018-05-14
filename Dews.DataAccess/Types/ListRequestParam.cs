using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.DataAccess.Types
{
    public class ListRequestParam
    {
        public ListRequestParam()
        {
            CurrentPage = 0;
            PageCount = 10;
            Criterias = new List<RequestCriteria>();
        }
        /// <summary>
        /// Starts with 0 and show the current select page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Shows max row count in a page , default 10
        /// </summary>
        public int PageCount { get; set; }

        public List<RequestCriteria> Criterias { get; set; }
    }
}
