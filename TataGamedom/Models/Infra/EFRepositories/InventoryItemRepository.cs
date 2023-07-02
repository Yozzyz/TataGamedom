using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels.InventoryItems;
using System.Data.Entity;

namespace TataGamedom.Models.Infra.EFRepositories
{
    public class InventoryItemRepository /*: IInventoryItemRepository*/
    {
        private readonly AppDbContext _db;
        public InventoryItemRepository()
        {
            _db = new AppDbContext();
        }
        //public IEnumerable<InventoryItemVM> Info(int? productId)
        //{
        //    InventoryItemVM vm = _db.InventoryItems.Include(i => i.StockInSheet)
        //                                           .Include(i => i.Product)
        //                                           .Include(i => i.)
        //}

        public void Delete(string index)
        {
            throw new NotImplementedException();
        }

        public void Update(InventoryItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}