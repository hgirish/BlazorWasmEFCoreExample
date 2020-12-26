using ContactsApp.BaseRepository;
using ContactsApp.DataAccess;
using ContactsApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IBasicRepository<Contact> _repo;
        private readonly IServiceProvider _provider;

        public QueryController(IBasicRepository<Contact> repo, IServiceProvider provider)
        {
            _repo = repo;
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var seed = _provider.GetService<SeedContacts>();
            await seed.CheckAndSeedDatabaseAsync(User);
            //var contacts = await _repo.GetListAsync();
            //return Ok(contacts);
            return Ok("Seeding done!");
        }
    }
}
