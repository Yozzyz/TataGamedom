using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.Dtos.FAQs;

namespace TataGamedom.Models.Interfaces
{
    public interface IFAQRepository
    {
        IEnumerable<FAQIndexDto> Search();
    }
}
