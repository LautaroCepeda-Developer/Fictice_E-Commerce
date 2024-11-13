using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICommonService<T, TI, TU>
    {
        List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T?> GetById(int id);
        Task<T> Add(TI insertDTO);
        Task<T?> Update(int id, TU updateDTO);
        Task<T?> Delete(int id);
        bool Validate(TI DTO);
        bool Validate(TU DTO);
    }
}
