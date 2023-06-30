using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.News
{
	public class NewsCriteria
	{
		public string Title { get; set; }
		public string Name { get; set; }

		public int? GamesId { get; set; }

		public DateTime ScheduleDate { get; set; }

		public bool ActiveFlag { get; set; }



	}
}