using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class PageWrapper<T> where T : class
    {
        public List<T> Items { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
    public class PaginationInfo
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Count { get; set; }
        public int? Next { get; set; }
        public int? Prev { get; set; }
    }
}
