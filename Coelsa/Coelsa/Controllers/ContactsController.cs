using Coelsa.Common.Common;
using Coelsa.Common.Interfaces;
using Coelsa.Common.Models;
using Coelsa.Service.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coelsa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController( IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // Crear un nuevo contacto
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            await _contactRepository.Add(contact);
            
            return Ok(contact);
        }

        // Actualizar un contacto
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Contact contact)
        {
            contact.IdContact = id;
            await _contactRepository.Update(contact);

            return Ok(contact);
        }

        // Eliminar un contacto
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.Delete(id);
            return Ok(id);
        }

        // Obtener lista de contactos por empresa(con paginación)
        [HttpGet]
        public async Task<IActionResult> GetByCompany(string company, int pageNumber = 1, int pageSize = 10)
        {
            var contacts = await _contactRepository.GetByCompany(company, pageNumber, pageSize);
            return Ok(contacts);
        }
    }
}
