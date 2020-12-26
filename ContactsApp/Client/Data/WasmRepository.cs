using ContactsApp.BaseRepository;
using ContactsApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactsApp.Client.Data
{
    /// <summary>
    /// Client implementation of the <see cref="IBasicRepository{Contact}"/>.
    /// </summary>
    public class WasmRepository : IBasicRepository<Contact>
    {
        private readonly HttpClient _apiClient;
        private readonly IContactFilters _controls;

        private const string ApiPrefix = "/api/";
        private string ApiContacts => $"{ApiPrefix}contacts/";
        private string ApiQuery => $"{ApiPrefix}query/";
        private string ForUpdate => "?forUpdate=true";

        public WasmRepository(IHttpClientFactory clientFactory, IContactFilters controls)
        {
            _apiClient = clientFactory.CreateClient(Program.BaseClient);
            _controls = controls;
        }

        /// <summary>
        /// Contact as loaded then modified by the user.
        /// </summary>
        public Contact OriginalContact { get; set; }

        /// <summary>
        /// Contact on the database.
        /// </summary>
        public Contact DatabaseContact { get; set; }

        /// <summary>
        /// The row version of the last contact loaded.
        /// </summary>
        public byte[] RowVersion { get; set; }

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

        /// <summary>
        /// Gets a page of <see cref="Contact"/> items.
        /// </summary>
        /// <returns>The result <see cref="ICollection{Contact}"/>.</returns>
        public async  Task<ICollection<Contact>> GetListAsync()
        {
            Debug.WriteLine($"GetListAsync: ApiQuery: {ApiQuery}");
            var result = await _apiClient.PostAsJsonAsync(
                ApiQuery, _controls);
            Debug.WriteLine($"GetListAsync result: {result.StatusCode} {result.ReasonPhrase}");
            var queryInfo = await result.Content.ReadFromJsonAsync<QueryResult>();

            // transfer page information
            _controls.PageHelper.Refresh(queryInfo.PageInfo);
            return queryInfo.Contacts;
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

        /// <summary>
        /// This will serialize a response from JSON and return null
        /// if the status code is 404 - not found.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<TEntity> SafeGetFromJsonAsync<TEntity>(string url)
        {
            var result = await _apiClient.GetAsync(url);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadFromJsonAsync<TEntity>();
        }
    }
}
