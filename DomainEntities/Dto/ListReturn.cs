using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.Dto
{
    public class Pagination
    {
        public int TotalPages { get; set; }
        public int TotalData { get; set; }
        public int PageNumber { get; set; } = 1;
        public int RowsOfPage { get; set; } = 10;
    }
    public class ListReturn<T> : Pagination
    {
        public IEnumerable<T> Data { get; set; }
    }
}
