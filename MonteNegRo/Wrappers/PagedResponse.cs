using System;
using System.Collections.Generic;

namespace MonteNegRo.Wrappers
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        //public Uri FirstPage { get; set; }
        //public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        //public Uri NextPage { get; set; }
        //public Uri PreviousPage { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize, int totalRecords)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.TotalRecords = totalRecords;
            if (totalRecords == 0)
            {
                this.TotalPages = 1;
            }
            else if (totalRecords % pageSize != 0)
            {
                this.TotalPages = totalRecords / pageSize + 1;
            }
            else
            {
                this.TotalPages = totalRecords / pageSize;
            }

            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
