using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.FAQs;
using TataGamedom.Models.Interfaces;

namespace TataGamedom.Models.Services
{
    public class FAQService
    {
        private IFAQRepository _repo;
        public FAQService(IFAQRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<FAQIndexDto> Search()
        {
            return _repo.Search();
        }
    }
}