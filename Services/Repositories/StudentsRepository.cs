using Services.Interfaces;
using Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Services.Repositories
{
    public class StudentsRepository : Repository<Student>, IStudentsRepository
    {
        public StudentsRepository(IUnitOfWork<UniversityDbContext> unitOfWork) : base(unitOfWork)
        {

        }

        public override List<Student> GetList()
        {
            return Table.Include(student => student.Group).ThenInclude(group => group.Course).OrderBy(student => student.LastName).ToList();
        }

        public override List<Student> GetListByParrent(int parrentId)
        {
            return Table.Where(student => student.GroupId == parrentId).OrderBy(student => student.LastName).ToList();
        }
    }
}
