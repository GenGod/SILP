using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SILP.Model;

namespace SILP.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly Microsoft.Extensions.Options.IOptions<Settings> config;

        public ValuesController(Microsoft.Extensions.Options.IOptions<Settings> config)
        {
            this.config = config;
        }

        private IRepository<Employee> CreateRepository()
        {
            return new MongoRepositoryFactory(config.Value.ConnectionString, config.Value.Database).GetRepository<Employee>();
        }

        // GET api/values
        /// <summary>
        /// Returns Employee list (without times data)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {            
            return CreateRepository().Query().Select(e => new Employee { Id = e.Id, UserName = e.UserName }).ToList();
        }

        // GET api/values/5
        /// <summary>
        /// Return information about employee with specified identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Employee Get(string id)
        {
            return CreateRepository().Get(id);
        }

        // POST api/values
        /// <summary>
        /// Creates new employee record
        /// </summary>
        [HttpPost]
        public void Post([FromBody]Employee value)
        {
            CreateRepository().Save(value);
        }

        // PUT api/values/5
        /// <summary>
        /// Updates employee record
        /// </summary>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Employee value)
        {
            CreateRepository().Save(value);
        }

        // DELETE api/values/5
        /// <summary>
        /// Deletes information about employee with specified identifier
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            CreateRepository().Delete(id);
        }
    }
}
