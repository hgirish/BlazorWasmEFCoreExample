using ContactsApp.DataAccess;
using ContactsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactsApp.Repository
{
    public class ContactRepository : IRepository<Contact, ContactContext>
    {
        public ContactContext PersistedContext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<Contact> AddAsync(Contact item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public void Attach(Contact item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Contact>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TPropertyType> GetPropertyValueAsync<TPropertyType>(Contact item, string propertyName)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> LoadAsync(int id, ClaimsPrincipal user, bool forUpdate = false)
        {
            throw new NotImplementedException();
        }

        public Task QueryAsync(Func<IQueryable<Contact>, Task> query)
        {
            throw new NotImplementedException();
        }

        public Task SetOriginalValueForConcurrencyAsync<TPropertyType>(Contact item, string propertyName, TPropertyType value)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> UpdateAsync(Contact item, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
