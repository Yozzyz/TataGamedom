using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.Products;

namespace TataGamedom.Models.Services
{
	public class ProductService
	{
		private IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProductIndexVM> Get()
        {
            return _repo.Get();
        }

    }
}