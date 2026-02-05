using ECommerce.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.API.Filters
{
    // ActionFilterAttribute: Controller metoduna girmeden önce veya sonra çalışır.
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // ModelState: Validasyon hatası var mı diye bakar.
            if (!context.ModelState.IsValid)
            {
                // Hataları topla
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                // (CustomResponseDto) cevabı hazırla
                var response = CustomResponseDto<NoContentDto>.Fail(400, errors);

                // Cevabı geri dön (Controller'a hiç girmeden geri postalar)
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}