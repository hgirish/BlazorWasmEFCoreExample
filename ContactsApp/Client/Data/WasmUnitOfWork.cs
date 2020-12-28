using ContactsApp.BaseRepository;
using ContactsApp.Model;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactsApp.Client.Data
{
    public class WasmUnitOfWork : IUnitOfWork<Contact>
    {
        private readonly WasmRepository _repo;

        public WasmUnitOfWork(IBasicRepository<Contact> repo)
        {
            _repo = repo as WasmRepository;
        }

        public Contact OriginalContact { get => _repo.OriginalContact; }

        public IBasicRepository<Contact> Repo => _repo;

        public Task CommitAsync()
        {
            return Repo.UpdateAsync(OriginalContact, null);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SetUser(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
