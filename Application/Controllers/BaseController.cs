using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegraHub.Application.Controllers
{
    public class BaseController : ControllerBase
    {
        public long LoginId
        {
            get
            {
                return Convert.ToInt64(HttpContext?.User?.FindFirst("loginId")?.Value);
            }
        }

        public long CompanyId
        {
            get
            {
                return Convert.ToInt64(HttpContext?.User?.FindFirst("companyId")?.Value);
            }
        }

        protected IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        protected async Task<IActionResult> ExecuteAsync(Func<Task<object>> func)
        {
            try
            {
                var result = await func();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
