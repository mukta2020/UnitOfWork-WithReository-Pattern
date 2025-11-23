using DataAccessEF.TypeRepository;
using Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessEF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private PeopleContext context;
        public UnitOfWork(PeopleContext context)
        {
            this.context = context;
            Address = new AddressRepository(this.context);
            Email = new EmailRepository(this.context);
            Person = new PersonRepository(this.context);
        }

        public IAdressRepository Address { get; private set; }
        public IEmailRepository Email { get; private set; }
        public IPersonRepository Person { get; private set; }

        ///////////////////////////
        public async Task<int> CommitEntityAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        public void RollbackEntityAsync()
        {
            this.context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
