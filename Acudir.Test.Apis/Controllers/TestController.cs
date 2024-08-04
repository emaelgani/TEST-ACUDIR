using Acudir.Test.Data.Entities;
using Acudir.Test.Service.Interfaces;
using Acudir.Test.Service.Model.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;


namespace Acudir.Test.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public TestController(IPersonaService personaService)
        {
            _personaService = personaService;
        }
 
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return new JsonResult(await _personaService.GetAll());
            }
            catch (Exception ex)
            {
                return new JsonResult(Result.ExceptionFailure(ex));

            }
        }

        //Post Guardar una Persona o mas
        [HttpPost("Add")]
        public async Task<ActionResult> Add(List<Persona> persona)
        {
            try
            {
                return new JsonResult(await _personaService.AddRange(persona));
            }
            catch (Exception ex)
            {
                return new JsonResult(Result.ExceptionFailure(ex));
            }
        }


        //Put Modificarlas
        [HttpPut("Update")]
        public async Task<ActionResult> Update(Persona persona)
        {
            try
            {
                return new JsonResult(await _personaService.Update(persona));
            }
            catch (Exception ex)
            {
                return new JsonResult(Result.ExceptionFailure(ex));
            }
        }

        //Put Modificarlas
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(string nombreCompleto)
        {
            try
            {
                return new JsonResult(await _personaService.Delete(nombreCompleto));
            }
            catch (Exception ex)
            {
                return new JsonResult(Result.ExceptionFailure(ex));
            }
        }

        [HttpGet("GetAllByProfesion")]
        public async Task<ActionResult> GetAllByProfesion(string profesion)
        {
            try
            {
                return new JsonResult(await _personaService.GetAllByProfesion(profesion));
            }
            catch (Exception ex)
            {
                return new JsonResult(Result.ExceptionFailure(ex));

            }
        }
    }
}
