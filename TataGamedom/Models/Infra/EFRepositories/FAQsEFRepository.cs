using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.FAQs;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models;
using System.Data.Entity.Spatial;


namespace TataGamedom.Models.Infra.EFRepositories
{
    public class FAQsEFRepository : IFAQRepository
    {

        private AppDbContext _db;
        public  FAQsEFRepository()
        {
            _db = new AppDbContext();
        }
        public IEnumerable<FAQIndexDto> Search()
        {
            return _db.FAQs
            .AsNoTracking()
            //.Include(p => )
            //.OrderBy(p => p.)
            .Select(p => new FAQIndexDto
            {
                Id = p.Id,
                Question = p.Question,
                Answer = p.Answer,
                IssueTypeId = p.IssueTypeId,
                CreateAt = (DateTime)p.CreatedAt
            });
        }
    }
}