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

namespace TataGamedom.Controllers
{
	public class BoardsApiController : ApiController
	{
		private AppDbContext db = new AppDbContext();

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
				Cover = "/Files/Uploads/" + board.BoardHeaderCoverImg,
				BoardName = board.Name,
				GameName = board.Game.ChiName + "|" + board.Game.EngName,
				BoardAboud = board.BoardAbout,
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
		public IHttpActionResult GetBoard(string boardName)
		{
			Board board = db.Boards.Find(boardName);
			if (board == null)
			{
				return NotFound();
			}
			var boardListDto = new BoardListVM
			{
				Id = board.Id,
				Cover = "/Files/Uploads/" + board.BoardHeaderCoverImg,
				BoardName = board.Name,
				GameName = board.Game.ChiName + "|" + board.Game.EngName,
				BoardAboud = board.BoardAbout,
				FollowersCount = db.MembersBoards.Count(mb => mb.BoardId == board.Id),
				LastPostedAt = db.Posts.Where(p => p.BoardId == board.Id)
						  .OrderByDescending(p => p.Datetime)
						  .Select(p => p.Datetime)
						  .DefaultIfEmpty(DateTime.MinValue)
						  .FirstOrDefault()
			};

			return Ok(boardListDto);
		}

		// PUT: api/BoardsApi/5
		[ResponseType(typeof(string))]
		public IHttpActionResult PutBoard(int id, Board board)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != board.Id)
			{
				return BadRequest();
			}

			db.Entry(board).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BoardExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}


		//TODO
		// POST: api/BoardsApi
		[ResponseType(typeof(string))]
		public IHttpActionResult PostBoard(BoardAddVm boardAddVm)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			
			db.Boards.Add(board);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = board.Id }, board);
		}

		// DELETE: api/BoardsApi/5
		[ResponseType(typeof(Board))]
		public IHttpActionResult DeleteBoard(int id)
		{
			Board board = db.Boards.Find(id);
			if (board == null)
			{
				return NotFound();
			}

			db.Boards.Remove(board);
			db.SaveChanges();

			return Ok(board);
		}

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
			return db.Boards.Count(e => e.Id == id) > 0;
		}
	}
}