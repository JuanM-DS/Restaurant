using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebAppi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
