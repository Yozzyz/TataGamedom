using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.ViewModels.InventoryItems;

namespace TataGamedom.Models.Interfaces
{
    public interface IInventoryItemRepository
    {
        IEnumerable<InventoryItemVM> Info(int? productId);

        void Update(InventoryItemDto dto);

        void Delete(string index);

    }
}
