using System;
using System.Collections.Generic;
using System.Text;

namespace Coelsa.Common.Common
{
    public class PaginationGeneric<T> where T: class
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Company { get; set; }
        public IEnumerable<T> Result { get; set; }

    }
}
