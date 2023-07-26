using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheLions.Helpers;
using TheLions.Helpers.Jwt;
using TheLions.Models;
using TheLions.Models.User;
using TheLions.UnitOfWork;
using TheLions.ViewModel.LoginDto;
using TheLions.ViewModel.Register;

namespace TheLions.Controllers
{
    
    public class UserController : BaseApiController
    {
        private readonly IJwtService _jwtService;
        
        public UserController(IUnitOfWork unitOfWork,IJwtService jwtService) : base(unitOfWork)
        {
            _jwtService = jwtService;
        }


        /// <summary>
        /// لاگین کردن کاربر
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("/Auth/Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto userDto)
        {
            var user = await UnitOfWork.UserRepository.Include(x => x.Roles).Where(x => x.Email == userDto.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return CustomResponse<ErrorResponce>(false, 402, "نام کاربری و کلمه عبور وارد شده صحیح نمی باشد", null);
            }
            var hash = SecurePasswordHasher.Hash(userDto.Password);
            var passCheck = SecurePasswordHasher.Verify(userDto.Password, user.Password);
            if (!passCheck)
            {
                return CustomResponse<ErrorResponce>(false, 402, "نام کاربری و کلمه عبور وارد شده صحیح نمی باشد", null);
            }
            var token = _jwtService.GenerateToken(user);
            var data = new
            {
                token = token,
            };
            return CustomResponse(true, 200, null, data);
        }


        /// <summary>
        /// لاگین کردن کاربر
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("/Auth/register")]
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> register([FromBody] RegisterDto registerDto)
        {
            registerDto.Password = SecurePasswordHasher.Hash(registerDto.Password);
            var user = new User
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
                UserName = registerDto.UserName,
            };
            await UnitOfWork.UserRepository.AddAsync(user);
             UnitOfWork.CompleteAsync();
            return CustomResponse(true, 200, null, user);
        }
    }

}
