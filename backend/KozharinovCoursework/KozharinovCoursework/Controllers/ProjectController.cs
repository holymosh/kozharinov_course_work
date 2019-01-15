using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IRepository<InvestProject> _projectRepository;
        private readonly IRepository<EnergeticsObject> _objectRepository;

        public ProjectController(IRepository<InvestProject> projectRepository, IRepository<EnergeticsObject> objectRepository)
        {
            _projectRepository = projectRepository;
            _objectRepository = objectRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_projectRepository.DbSet.Include(project => project.EnergeticsObjects));
        }

        [HttpPost]
        public IActionResult Create([FromBody] InvestProject project)
        {

            project.EnergeticsObject = _objectRepository.FindById(project.EnergeticsObject.Id);
            var created = _projectRepository.Create(project);
            return Ok(created.Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] InvestProject project)
        {
            project.EnergeticsObject = _objectRepository.FindById(project.EnergeticsObject.Id);
            _projectRepository.Update(project);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromBody] int id)
        {
            _projectRepository.Remove(id);
            return Ok();
        }
    }
}
