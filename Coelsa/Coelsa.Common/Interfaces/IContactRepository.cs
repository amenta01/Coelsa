using Coelsa.Common.Common;
using Coelsa.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coelsa.Common.Interfaces
{
    public interface IContactRepository
    {
        public Task Add(Contact contact);

        public Task<PaginationGeneric<Contact>> GetByCompany(string company, int pageNumber = 0, int pageSize = 10);

        public Task Delete(int id);

        public Task Update(Contact contact);
    }
}
