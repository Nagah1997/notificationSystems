using Microsoft.AspNetCore.Authorization;

namespace _4EPlatform_API_Task_Controllers
{
  [Route("api/[controller]")]
 [ApiController]
//    [ApiVersion("1.0")]
   [Authorize]
//    [EnableRateLimiting("FixedWindowPolicy")]
    public class BaseController : ControllerBase
    {

    }
}
