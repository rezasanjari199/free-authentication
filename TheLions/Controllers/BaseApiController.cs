using Microsoft.AspNetCore.Mvc;
using TheLions.Models;
using TheLions.UnitOfWork;

namespace TheLions.Controllers
{
    public class BaseApiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        protected BaseApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected IUnitOfWork UnitOfWork => _unitOfWork;
        protected IActionResult CustomResponse<T>(bool status, int httpStatus, string message, T value, int? errorCode = null)
        {
            if (status)
            {
                var responce = new SuccessResponce<T>() { success = true, message = message, data = value };
                return StatusCode(httpStatus, responce);
            }
            else
            {
                var responce = new ErrorResponce() { success = false, message = message, data = value, errorCode = errorCode };
                return StatusCode(httpStatus, responce);
            }
        }
    }
}
