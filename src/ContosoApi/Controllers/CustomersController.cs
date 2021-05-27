using Contoso.Model;
using Contoso.Mongo.Repositories;
using Contoso.SqlServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMongoCustomersRepository _mongoCustomersRepository;
        private readonly ISqlCustomersRepository _sqlCustomersRepository;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            IMongoCustomersRepository mongoCustomersRepository,
            ISqlCustomersRepository sqlCustomersRepository,
            ILogger<CustomersController> logger)
        {
            _mongoCustomersRepository = mongoCustomersRepository;
            _sqlCustomersRepository = sqlCustomersRepository;
            _logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<Customer>> GetAsync(bool sql = true)
        {
            return sql ? _sqlCustomersRepository.FindAsync() : _mongoCustomersRepository.FindAsync();
        }

        [HttpPost]
        public async Task<ActionResult> InitAsync()
        {
            var items = await _sqlCustomersRepository.FindAsync();
            await _mongoCustomersRepository.InsertAllAsync(items);
            return NoContent();
        }
    }
}
