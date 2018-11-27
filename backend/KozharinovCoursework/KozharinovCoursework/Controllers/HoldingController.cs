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
    public class HoldingController : ControllerBase
    {
        private readonly IRepository<Holding> _repository;

        public HoldingController(IRepository<Holding> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetHoldings()
        {
            return Ok(_repository.GetAll());
        }
    }
}