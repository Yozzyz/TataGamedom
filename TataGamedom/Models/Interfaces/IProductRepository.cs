﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.Products;

namespace TataGamedom.Models.Interfaces
{
	public interface IProductRepository
	{
		IEnumerable<ProductIndexVM> Get();

		ProductEditVM GetGameById(int id);

		Product GetGameByIndex(ProductEditVM vm);
	}
}
