using FlexiSourceCodingTest.Models;

namespace FlexiSourceCodingTest.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<GetUserProfileReturn>> GetUserProfile(string UserID);
        Task<IEnumerable<GenericReturnStatusData<GetUserListReturn>>> GetUserList();
        Task<IEnumerable<GetUserRunningActivityReturn>> GetUserRunningActivity(string UserID);
        Task<IEnumerable<GenericReturn>> AddUpdateUser(UserProfileParam Value);
        Task<IEnumerable<GenericReturn>> AddUserRunningActivity(UserRunningActivityParam Value);
        Task<IEnumerable<GenericReturn>> DeleteUser(DeleteuserParam Value);
        Task<IEnumerable<GenericReturn>> DeleteUserRunningActiviy(DeleteUserRunningActiviyParam Value);
    }
}
