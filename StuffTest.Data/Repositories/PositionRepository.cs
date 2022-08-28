using StuffTest.Data.Abstract;
using StuffTest.Model;

namespace StuffTest.Data.Repositories;

public class PositionRepository : EntityBaseRepository<Position>, IPositionRepository
{
    public PositionRepository(StuffContent context) : base(context) { }

    
}