using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICommonPublicService<T, TP>
    {
        Task<IEnumerable<TP>> GetPublic();
        Task<TP?> GetPublicById(int id);
    }
}
