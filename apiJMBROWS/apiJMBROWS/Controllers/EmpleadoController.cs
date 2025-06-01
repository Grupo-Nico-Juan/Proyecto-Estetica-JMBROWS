using Microsoft.AspNetCore.Mvc;

namespace apiJMBROWS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IRepositorioUsuarios _repo;

        public EmpleadoController(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.GetEmpleados());
    }

}
