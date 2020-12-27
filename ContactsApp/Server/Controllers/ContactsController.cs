using ContactsApp.BaseRepository;
using ContactsApp.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContactsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IBasicRepository<Contact> _repo;
        private readonly IServiceProvider _serviceProvider;

        public ContactsController(IBasicRepository<Contact> repo, IServiceProvider serviceProvider)
        {
            _repo = repo;
            _serviceProvider = serviceProvider;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, bool forUpdate = false)
        {
            if (id < 1)
            {
                return NotFound();
            }
            if (forUpdate)
            {
                /*
                var unitOfWork = _serviceProvider.GetService<IUnitOfWork<Contact>>();
                HttpContext.Response.RegisterForDispose(unitOfWork);
                var result = await unitOfWork.Repo.LoadAsync(id, User, true);

                // return version for tracking on client. It is not
                // part of the C# class so it is tracked as a "shadow property"
                var concurrencyResult = new ContactConcurrencyResolver
                {
                    OriginalContact = result,
                    RowVersion = result == null ? null :
                    await unitOfWork.Repo.GetPropertyValueAsync<byte[]>(
                        result, ContactContext.RowVersion)
                };
                return Ok(concurrencyResult);
                */
                return Ok();
            }
                
            else
            {
                var result = await _repo.LoadAsync(id, User);
                return result == null ? NotFound() : Ok(result);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _repo.DeleteAsync(id, User);
                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
