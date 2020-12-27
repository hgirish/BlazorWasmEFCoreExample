using ContactsApp.BaseRepository;
using ContactsApp.Model;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactsApp.Client.Data
{
    public class WasmUnitOfWork : IUnitOfWork<Contact>
    {
        private readonly IBasicRepository<Contact> _repo;

        public WasmUnitOfWork(IBasicRepository<Contact> repo)
        {
            _repo = repo;
        }

        public IBasicRepository<Contact> Repo => _repo;

        public Task CommitAsync()
        {
            throw new NotImplementedException();
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
