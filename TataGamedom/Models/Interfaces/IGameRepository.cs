using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.Dtos.Games;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Models.Interfaces
{
	public interface IGameRepository
	{
		IEnumerable<GameIndexDto> Search();

		Game GetGameById(int id);

		bool UpddateGame(GameEditDto dto);

		bool Create(GameCreateDto dto);

		GameCreateDto GetGameByName(string chi, string eng);

		List<GameClassificationsCode> GetGameClassifications();
		GameEditCoverImgDto GetGameById2(int id);

		bool EditGameCover(GameEditCoverImgDto dto);

	}
}
