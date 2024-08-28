using FlexiSourceCodingTest.Interfaces;
using FlexiSourceCodingTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexiSourceCodingTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "User")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet("GetUserProfile")]
        [Consumes("application/json")]
        public async Task<IEnumerable<GetUserProfileReturn>> GetUserProfile(string UserID)
        {
            return await _unitOfWork.User.GetUserProfile(UserID);
        }

        [HttpGet("GetUserList")]
        [Consumes("application/json")]
        public async Task<IEnumerable<GenericReturnStatusData<GetUserListReturn>>> GetUserList()
        {
            return await _unitOfWork.User.GetUserList();
        }

        [HttpGet("GetUserRunningActivity")]
        [Consumes("application/json")]
        public async Task<IEnumerable<GetUserRunningActivityReturn>> GetUserRunningActivity(string UserID)
        {
            return await _unitOfWork.User.GetUserRunningActivity(UserID);
        }

        [HttpPost("AddUpdateUser")]
        [Consumes("application/json")]
        public async Task<IEnumerable<GenericReturn>> AddUpdateStudent(UserProfileParam Value)
        {
            return await _unitOfWork.User.AddUpdateUser(Value);
        }

        [HttpPost("AddUserRunningActivity")]
        [Consumes("application/json")]
        public async Task<IEnumerable<GenericReturn>> AddUserRunningActivity(UserRunningActivityParam Value)

        {
            return await _unitOfWork.User.AddUserRunningActivity(Value);
        }

        [HttpPost("DeleteUser")]
        [Consumes("application/json")]
        public async Task<IEnumerable<GenericReturn>> DeleteUser(DeleteuserParam Value)

        {
            return await _unitOfWork.User.DeleteUser(Value);
        }

        [HttpPost("DeleteUserRunningActiviy")]
        [Consumes("application/json")]
        public async Task<IEnumerable<GenericReturn>> DeleteUserRunningActiviy(DeleteUserRunningActiviyParam Value)

        {
            return await _unitOfWork.User.DeleteUserRunningActiviy(Value);
        }
    }
}
