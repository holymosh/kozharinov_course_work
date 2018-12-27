using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectController : ControllerBase
    {
        private readonly IRepository<EnergeticsObject> _repository;

        public ObjectController(IRepository<EnergeticsObject> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] EnergeticsObject energeticsObject)
        {
            var created = _repository.Create(energeticsObject);
            return Ok(created.Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] EnergeticsObject energeticsObject)
        {
            _repository.Update(energeticsObject);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Remove(id);
            return Ok();
        }
    }
}