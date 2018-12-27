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
    public class ObjectController : ControllerBase
    {
        private readonly IRepository<EnergeticsObject> _objectRepository;
        private readonly IRepository<EnergeticsSubject> _subjectRepository;

        public ObjectController(IRepository<EnergeticsObject> objectRepository, IRepository<EnergeticsSubject> subjectRepository)
        {
            _objectRepository = objectRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_objectRepository.DbSet.Include(o => o.Subject).Include(o => o.Parent));
        }

        [HttpPost]
        public IActionResult Create([FromBody] EnergeticsObject energeticsObject)
        {
            var newObj = new EnergeticsObject
            {
                Address = energeticsObject.Address,Name = energeticsObject.Name,ObjType = energeticsObject.ObjType
            };
            var created =_objectRepository.Create(newObj);
            if (energeticsObject.Subject != null)
            {
                var subj = _subjectRepository.FindById(energeticsObject.Subject.Id);
                created.Subject = subj;
                created.SubjectId = subj.Id;
                _objectRepository.Update(created);

            }

            if (energeticsObject.Parent != null)
            {
                var parent = _objectRepository.FindById(energeticsObject.Parent.Id);
                created.Parent = parent;
                created.ParentId = parent.Id;
                _objectRepository.Update(parent);
            }
            return Ok(newObj.Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] EnergeticsObject energeticsObject)
        {
            _objectRepository.Update(energeticsObject);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            _objectRepository.Remove(id);
            return Ok();
        }
    }
}