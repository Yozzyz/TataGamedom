using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.Dtos.Games;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.Games;

namespace TataGamedom.Models.Interfaces
{
	public interface IGameRepository
	{
		IEnumerable<GameIndexDto> Search();

		GameEditVM GetGameById(int id);

		bool UpddateGame(GameEditDto dto);

		bool Create(GameCreateDto dto);

		GameCreateDto GetGameByName(string chi, string eng);

		List<GameClassificationsCode> GetGameClassifications();
		GameEditCoverImgDto GetGameById2(int id);

		bool EditGameCover(GameEditCoverImgDto dto);

		Game GetGameByName2(string name);

		bool CreateBoard(Game game);

		bool CreateClassification(Game game, int gameClassificationId);

	}
}
