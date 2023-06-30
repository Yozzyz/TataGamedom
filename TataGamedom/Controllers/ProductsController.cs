using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels.Games;
using TataGamedom.Models.ViewModels.Products;

namespace TataGamedom.Controllers
{
    public class ProductsController : Controller
    {
		private AppDbContext db = new AppDbContext();
		[Authorize]
		// GET: Products
		public ActionResult Index()
        {
			IEnumerable<ProductIndexVM> product = GetProducts();
			return View(product);
		}

		private IEnumerable<ProductIndexVM> GetProducts()
		{
			IProductRepository repo = new ProductDapperRepository();
			ProductService service = new ProductService(repo);
			return service.Get();
			
		}
	}
}