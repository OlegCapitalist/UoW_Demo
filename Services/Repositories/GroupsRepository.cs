using Services.Interfaces;
using Models;
using Infrastructure;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.Repositories
{
    public class GroupsRepository : Repository<Group>, IGroupsRepository
    {
        public GroupsRepository(IUnitOfWork<UniversityDbContext> unitOfWork) : base (unitOfWork)
        {
            beforeDelete += StudentsCheckExist;
        }

        public override List<Group> GetList()
        {
            return Table.Include(group => group.Course).OrderBy(group => group.Name).ToList();
        }

        public override List<Group> GetListByParrent(int parrentId)
        {
            return Table.Where(x => x.CourseId == parrentId).Include(x => x.Course).OrderBy(group => group.Name).ToList();
        }

        private void StudentsCheckExist(object sender, TransactionEventArgs args)
        {
            if (sender is Group group)
            {
                args.Cancel = (Context.Students.Where(student => student.GroupId == group.Id).Any());
            }
        }
    }
}
