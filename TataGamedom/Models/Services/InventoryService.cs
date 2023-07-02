using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.InventoryItems;

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

    }
}