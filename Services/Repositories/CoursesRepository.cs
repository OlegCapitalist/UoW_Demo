using Services.Interfaces;
using Models;
using Infrastructure;

namespace Services.Repositories
{
    public class CoursesRepository : Repository<Course>, ICoursesRepository
    {
        
        public CoursesRepository(IUnitOfWork<UniversityDbContext> unitOfWork) : base (unitOfWork)
        {
            beforeDelete += GroupsCheckExist;
        }

        public override List<Course> GetList()
        {
            return Table.OrderBy(course => course.Name).ToList();
        }

        public override List<Course> GetListByParrent(int parrentId)
        {
            throw new NotImplementedException("Table has no parent");
        }

        private void GroupsCheckExist(object sender, TransactionEventArgs args)
        {
            if (sender is Course course)
            {
                args.Cancel = (Context.Groups.Where(group => group.CourseId == course.Id).Any());
            }
        }

    }
}
