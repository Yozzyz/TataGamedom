using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.InventoryItems;
using TataGamedom.Models.ViewModels.Products;

namespace TataGamedom.Models.Services
{
    public class InventoryService
    {
        private readonly IInventoryRepository _repo;
        public InventoryService(IInventoryRepository repo) 
        {
            _repo = repo;
        }
        public IEnumerable<InventoryVM> GetAll() => _repo.Search();

        public IEnumerable<InventoryItemVM> GetItemInfo(int? productId) => _repo.Info(productId);

        public Result Create(InventoryItemCreateDto dto) 
        {
			int maxId = _repo.GetMaxIdInDb();

			var indexGenerator = new IndexGenerator(maxId);
            string productIndex = _repo.GetProductIndex(dto.ProductId);
			dto.Index = indexGenerator.GetSKU(dto, productIndex);
			_repo.Create(dto);
			return Result.Success();
		}

        public Result Update(InventoryItemDto dto) 
        {
            _repo.Update(dto);
            return Result.Success();
        }

        public InventoryItemDto GetByIndex(string index) => _repo.GetByIndex(index);
		
	}
}