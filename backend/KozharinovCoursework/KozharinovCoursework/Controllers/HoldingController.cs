using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoldingController : ControllerBase
    {
        private readonly IRepository<Holding> _repository;

        public HoldingController(IRepository<Holding> repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.DbSet.Include(ent => ent.Parent).ToList());
        }

        [HttpPost]
        public IActionResult CreateHolding([FromBody] Holding holding)
        {
            var newHolding = new Holding {Name = holding.Name};
            var created = _repository.Create(newHolding);
            if (holding.Parent != null)
            {
                created.Parent = _repository.FindById(holding.Parent.Id);
                created.ParentId = created.Parent.Id;
                _repository.Update(created);
            }
            
            return Ok(created.Id);
        }

        [HttpPut]
        public IActionResult UpdateHolding([FromBody] Holding holding)
        {
            _repository.Update(holding);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteHolding(int id)
        {
            _repository.Remove(id);
            return Ok();
        }
    }
}