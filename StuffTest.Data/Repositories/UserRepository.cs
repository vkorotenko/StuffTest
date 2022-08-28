using StuffTest.Data;
using StuffTest.Data.Abstract;
using StuffTest.Model;

namespace StuffTest.Data.Repositories;

public class UserRepository : EntityBaseRepository<User>, IUserRepository
{
    public UserRepository(StuffContent context) : base(context) { }

    

   
}