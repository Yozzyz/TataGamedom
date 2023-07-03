using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.Products;

namespace TataGamedom.Models.Services
{
	public class ProductService
	{
		private AppDbContext db = new AppDbContext();

		private IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProductIndexVM> Get()
        {
            return _repo.Get();
        }
        public ProductEditVM Get(int id)
        {
			
			var product = _repo.GetGameById(id);
            if(product == null) { return null; }
			return product;
        }

    }
}