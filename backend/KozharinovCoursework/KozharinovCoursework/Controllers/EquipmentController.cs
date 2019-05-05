using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML;

namespace UiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IPredictionService _predictionService;
        private readonly IRepository<ProjectEquipment> _repository;

        public EquipmentController(IPredictionService predictionService, IRepository<ProjectEquipment> repository)
        {
            _predictionService = predictionService;
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetForObjectPage()
        {
            return Ok(_repository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProjectEquipment projectEquipment)
        {
            var prediction = _predictionService.PredictWorkTime(new PredictionInput
                {TypeId = projectEquipment.TypeId, VendorId = projectEquipment.VendorId, Power = projectEquipment.Power});
            projectEquipment.PredictedDate = prediction.ToDateTime();
            var created = _repository.Create(projectEquipment);
            return Ok(created);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProjectEquipment ProjectEquipment)
        {
            _repository.Update(ProjectEquipment);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.Remove(id);
            return Ok();
        }

    }
}