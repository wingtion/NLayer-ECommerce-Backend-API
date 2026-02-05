using ECommerce.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        // Bu metod, gelen CustomResponseDto'nun içindeki StatusCode'a bakar
        // ve ona uygun (200, 201, 400, 404 vb.) HTTP cevabını otomatik üretir.
        [NonAction] // Bu bir endpoint değil, yardımcı metoddur.
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204) // No Content
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}