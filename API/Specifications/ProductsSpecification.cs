using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Entities;

namespace API.Specifications
{
    public class ProductsSpecification : Specification<Product>
    {
        public ProductsSpecification(ProductParams productParams) : base (x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))
        )
        {
            AddInclude(x => x.User);
            AddOrderBy(x => x.Name);
            ApplyPagination(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    
                    case "priceDsc":
                        AddOrderByDescending(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name.ToLower());
                        break;
                }

            }
        }

        public ProductsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.User);
        }
    }
}