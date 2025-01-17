﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TataGamedom.Models.Dtos.Orders;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Services;

namespace TataGamedom.Models.Interfaces
{
	public interface IOrderRepository
	{
		IEnumerable<OrderIndexDto> Search(Criteria criteria, SortInfo sortInfo);

		IEnumerable<OrderIndexDto> Search(Criteria criteria, SortInfo sortInfo, string sqlAddPage);

		void Create(OrderDto dto);

		int GetMaxIdInDb();

        IEnumerable<OrderInfoDto> GetOrderItemsInfo(string index);

		OrderDto GetByIndex(string index);
        void Update(OrderDto dto);
        //void Delete(string index);
    }
}
