using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class SearchParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string searchTerm { get; set; }
        public string sortColumn { get; set; }
        public string sortDirection { get; set; }
        public int parentId { get; set; } = 0;
        public int totalCount { get; set; } = 0;
        public int startIndex
        {
            get
            {
                return ((this.Page - 1) * this.PageSize);
            }
        }
        public int endIndex
        {
            get
            {
                return ((this.Page - 1) * this.PageSize + this.PageSize);
            }
        }
        public int organizationId { get; set; } = 1;
    }
}
