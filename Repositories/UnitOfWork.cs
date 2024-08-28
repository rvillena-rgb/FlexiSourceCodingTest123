using FlexiSourceCodingTest.Interfaces;

namespace FlexiSourceCodingTest.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork
        (
             IUserRepository user
        )
        {
            User = user;
        }

        public IUserRepository  User { get; }
    }
}
