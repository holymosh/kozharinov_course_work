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
    public class SubjectController : ControllerBase
    {
        private readonly IRepository<EnergeticsSubject> _subjectRepository;
        private readonly IRepository<Holding> _holdingRepository;

        public SubjectController(IRepository<EnergeticsSubject> subjectRepository, IRepository<Holding> holdingRepository)
        {
            _subjectRepository = subjectRepository;
            _holdingRepository = holdingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_subjectRepository.DbSet.Include(subj => subj.Holding));
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetForObjectPage()
        {
            return Ok(_subjectRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] EnergeticsSubject energeticsSubject)
        {
            var newSubj = new EnergeticsSubject{Address = energeticsSubject.Address,Email = energeticsSubject.Email,
                Inn = energeticsSubject.Inn,Kpp = energeticsSubject.Kpp,
                Name = energeticsSubject.Name,Okved = energeticsSubject.Okved,PostAddress = energeticsSubject.PostAddress};
            var created = _subjectRepository.Create(newSubj);
            if (energeticsSubject.Holding!= null)
            {
                var holding = _holdingRepository.FindById(energeticsSubject.Holding.Id);
                created.Holding = holding;
                created.HoldingId = holding.Id;
                _subjectRepository.Update(created);
            }
            return Ok(created.Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] EnergeticsSubject energeticsSubject)
        {
            _subjectRepository.Update(energeticsSubject);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _subjectRepository.Remove(id);
            return Ok();
        }
    }
}