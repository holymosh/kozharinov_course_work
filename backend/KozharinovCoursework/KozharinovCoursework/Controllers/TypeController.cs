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
    public class TypeController : ControllerBase
    {
        private readonly IRepository<EnergeticsType> _typeRepository;

        public TypeController(IRepository<EnergeticsType> typeRepository)
        {
            _typeRepository = typeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_typeRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] EnergeticsType energeticsType)
        {
            return Ok(_typeRepository.Create(new EnergeticsType() {Title = energeticsType.Title}).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] EnergeticsType energeticsType)
        {
            _typeRepository.Update(energeticsType);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _typeRepository.Remove(id);
            return Ok();
        }
    }
}