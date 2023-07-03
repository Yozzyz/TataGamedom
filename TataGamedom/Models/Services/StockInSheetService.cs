using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.StockInSheets;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Interfaces;

namespace TataGamedom.Models.Services
{
	public class StockInSheetService
	{
		private readonly IStockInSheetRepository _repo;
        public StockInSheetService(IStockInSheetRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<StockInSheetIndexDto> Search() => _repo.Search();

        public Result Create(StockInSheetDto dto) 
        {
            int maxId =_repo.GetMaxIdInDb();
            var indexGenerator = new IndexGenerator(maxId);
			dto.Index = indexGenerator.GetStockInSheetIndex(dto);
            _repo.Create(dto);
            return Result.Success();
        }

        public StockInSheetDto GetById(int? id) => _repo.GetById(id);
        
        public Result Update(StockInSheetDto dto) 
        {
            _repo.Update(dto);
            return Result.Success();
        }
    }
}