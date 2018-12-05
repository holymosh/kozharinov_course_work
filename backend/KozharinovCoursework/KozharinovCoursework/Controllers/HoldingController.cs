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
        public IActionResult GetAllHoldings() => Ok(_repository.DbSet.Include(holding => holding.Parent));

        [HttpPost]
        public IActionResult CreateHolding([FromBody] Holding holding)
        {
            //if (holding.ParentId.HasValue)
            //{
            //    holding.Parent = _repository.FindById(holding.ParentId.Value);
            //}
            return Ok(_repository.Create(holding));
        }
    }
}