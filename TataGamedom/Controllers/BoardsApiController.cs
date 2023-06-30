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
		public async Task<IEnumerable<BoardListDto>> GetBoards()
		{
			if (db.Boards == null)
			{
				return null;
			}
			return db.Boards.Select(bd => new BoardListDto
			{
				Cover = "/Files/Uploads/"+bd.BoardHeaderCoverImg,
				BoardName = bd.Name,
				GameName = bd.Game.ChiName+"|"+bd.Game.EngName,
				BoardAboud = bd.BoardAbout
			});
		}

		// GET: api/BoardsApi/
		[ResponseType(typeof(Board))]
		public IHttpActionResult GetBoard(int id)
		{
			Board board = db.Boards.Find(id);
			if (board == null)
			{
				return NotFound();
			}

			return Ok(board);
		}

		// PUT: api/BoardsApi/5
		[ResponseType(typeof(void))]
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

		// POST: api/BoardsApi
		[ResponseType(typeof(Board))]
		public IHttpActionResult PostBoard(Board board)
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