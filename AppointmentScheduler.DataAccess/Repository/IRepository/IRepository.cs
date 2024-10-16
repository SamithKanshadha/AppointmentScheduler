using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.DataAccess.Repository.IRepository
{

    public interface IRepository<T> where T : class
    {
        //Based on the ID we want to retrieve a category from the database
        T Get(int id);

        //This way use to stop repeating code and complexity of the code
        IEnumerable<T> GetAll(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          string includeProperties = null
          );

        //In this method it returns only one value so it is not needed to add IEnumerable .So just T is enough
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

        IEnumerable<T> GetClosestUpcomingAppointments(
            Expression<Func<T, DateTime>> orderBySelector,
            int count
        );

    }
}
