using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TataGamedom.Models.EFModels;
using System.Web.Mvc;
using System.Threading.Tasks;
using TataGamedom.Models.Dtos.Boards;
using TataGamedom.Models.ViewModels.Boards;
using TataGamedom.Models.Infra;

namespace TataGamedom.Controllers
{
	public class BoardsApiController : ApiController
	{
		private AppDbContext db = new AppDbContext();
		private SimpleHelper simpleHelper = new SimpleHelper();

		// GET: api/BoardsApi
		public async Task<IEnumerable<BoardListVM>> GetBoards()
		{
			if (db.Boards == null)
			{
				return null;
			}
			return db.Boards.Select(board=> new BoardListVM
			{
				Id= board.Id,
				BoardHeaderCoverImg = "/Files/Uploads/" + board.BoardHeaderCoverImg,
				Name = board.Name,
				GameName = board.GameId != null ? (board.Game.ChiName + " | " + board.Game.EngName) : "",
				BoardAbout = board.BoardAbout,
				FollowersCount = db.MembersBoards.Count(mb => mb.BoardId == board.Id),
				LastPostedAt = db.Posts.Where(p => p.BoardId == board.Id)
					  .OrderByDescending(p => p.Datetime)
					  .Select(p => p.Datetime)
					  .DefaultIfEmpty(DateTime.MinValue)
					  .FirstOrDefault()
			});
		}

		// GET: api/BoardsApi/5
		[ResponseType(typeof(BoardListVM))]
		public IHttpActionResult GetBoard(int id)
		{
			Board board = db.Boards.Find(id);
			if (board == null)
			{
				return NotFound();
			}
			var vm = new BoardListVM
			{
				Id = board.Id,
				BoardHeaderCoverImg = "/Files/Uploads/" + board.BoardHeaderCoverImg,
				Name = board.Name,
				GameName = board.Game.ChiName + " | " + board.Game.EngName,
				BoardAbout = board.BoardAbout,
				FollowersCount = db.MembersBoards.Count(mb => mb.BoardId == board.Id),
				LastPostedAt = db.Posts.Where(p => p.BoardId == board.Id)
						  .OrderByDescending(p => p.Datetime)
						  .Select(p => p.Datetime)
						  .DefaultIfEmpty(DateTime.MinValue)
						  .FirstOrDefault()
			};

			return Ok(vm);
		}

		// PUT: api/BoardsApi/5
		[ResponseType(typeof(ApiResult))]
		public ApiResult PutBoard(int id, BoardCreateEditorVM vm)
		{
			if (!ModelState.IsValid)
			{
				return ApiResult.Fail("驗證失敗");
			}

			BoardEditDto dto = new BoardEditDto
			{
				Id = vm.Id ?? 0,
				Name= vm.Name,
				BoardAbout = vm.BoardAbout,
			};

			Board existingEntity = db.Boards.Find(id);

			if (id != dto.Id)
			{
				return ApiResult.Fail("修改失敗");
			}

			if (BoardNameExists(dto.Name) && existingEntity.Name != dto.Name)
			{
				return ApiResult.Fail("已經有同名看板");
			}

			try
			{
				existingEntity.Name = dto.Name;
				existingEntity.BoardAbout = dto.BoardAbout;
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BoardExists(id))
				{
					return ApiResult.Fail("修改失敗");
				}
				else
				{
					throw;
				}
			}

			return ApiResult.Success("修改成功！");
		}


		// POST: api/BoardsApi
		[ResponseType(typeof(ApiResult))]
		public ApiResult PostBoard(BoardCreateEditorVM vm)
		{
			if (vm == null)
			{
				return ApiResult.Fail("沒VM好新增失敗");
			}
			if (!ModelState.IsValid)
			{
				return ApiResult.Fail("驗證新增失敗");
			}

			var BackendMemberAccount = User.Identity.Name;
			int BackendMemberId = simpleHelper.FindBackendmemberIdByAccount(BackendMemberAccount);

			BoardCreateDto dto = new BoardCreateDto
			{
				Name = vm.Name,
				BoardAbout = vm.BoardAbout,
				BoardHeaderCoverImg = vm.BoardHeaderCoverImg,
				CreatedBackendMemberId = BackendMemberId,
				CreatedTime = DateTime.Now,
			};

			if (dto.CreatedBackendMemberId == 0) { return ApiResult.Fail("沒人啊，新增失敗"); }
			if (BoardNameExists(dto.Name) ) { return ApiResult.Fail("版名重複，新增失敗"); }

			Board entity = new Board
			{
				Name = dto.Name,
				GameId = null,
				BoardAbout = dto.BoardAbout,
				BoardHeaderCoverImg = dto.BoardHeaderCoverImg,
				CreatedBackendMemberId = dto.CreatedBackendMemberId,
				CreatedTime = dto.CreatedTime
			};

			db.Boards.Add(entity);
			db.SaveChanges();

			return ApiResult.Success("新增成功");
		}


		// DELETE: api/BoardsApi/5
		//[ResponseType(typeof(Board))]
		//public IHttpActionResult DeleteBoard(int id)
		//{
		//	Board board = db.Boards.Find(id);
		//	if (board == null)
		//	{
		//		return NotFound();
		//	}

		//	db.Boards.Remove(board);
		//	db.SaveChanges();

		//	return Ok(board);
		//}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool BoardExists(int id)
		{
			return db.Boards.Any(e => e.Id == id);
		}
		private bool BoardNameExists(string name)
		{
			return db.Boards.Any(e => e.Name == name);
		}
	}

}