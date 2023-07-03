using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.Dtos.StockInSheets;

namespace TataGamedom.Models.Interfaces
{
	public interface IStockInSheetRepository
	{
		IEnumerable<StockInSheetIndexDto> Search();

		void Create(StockInSheetDto dto);

		StockInSheetDto GetById(int? id);
		
		void Update(StockInSheetDto dto);
		
		int GetMaxIdInDb();

	}
}
