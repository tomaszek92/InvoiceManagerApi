using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagerApi.Models;
using MediatR;
using System.Threading;

namespace InvoiceManagerApi.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients(CancellationToken cancellationToken)
        {
            var query = new Logic.Clients.GetAll.Query();
            var clients = await _mediator.Send(query, cancellationToken);

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id, CancellationToken cancellationToken)
        {
            var query = new Logic.Clients.GetById.Query(id);
            var client = await _mediator.Send(query, cancellationToken);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client, CancellationToken cancellationToken)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            var command = new Logic.Clients.Update.Command(client);
            var updatedClient = await _mediator.Send(command, cancellationToken);

            return updatedClient == null ? NotFound() : (IActionResult)NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client, CancellationToken cancellationToken)
        {
            var command = new Logic.Clients.Create.Command(client);
            var addedClient = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction("GetClient", new { id = addedClient.Id }, client);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClient(int id, CancellationToken cancellationToken)
        {
            var command = new Logic.Clients.Delete.Command(id);
            var client = await _mediator.Send(command, cancellationToken);
            if (client == null)
            {
                return NotFound();
            }

            return client;
        }
    }
}
