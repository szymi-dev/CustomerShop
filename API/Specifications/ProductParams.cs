using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Specifications
{
    public class ProductParams
    {
        private const int MaxPageSize = 25;
        public int PageIndex {get; set;} = 1;
        private int _pageSize = 20;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? brandId { get; set; }
        public int? typeId { get; set; }
         public string sort { get; set; }
         private string _search;
         public string Search
         {
            get => _search;
            set => _search = value.ToLower();
         } 

    }
}