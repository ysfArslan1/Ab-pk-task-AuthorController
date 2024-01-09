using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.DBOperations;

namespace Ab_pk_task3.Aplication.AuthorsOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int Id { get; set; }
        private readonly PatikaDbContext _dbContext;
        public DeleteAuthorCommand(PatikaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            // id üzerinden database sorgusu yapılır
            var item = _dbContext.Authors.Where(x => x.Id == Id).SingleOrDefault();
            if (item is null)
                throw new InvalidOperationException("Bulunamadı");
            // database işlemleri yapılır.
            _dbContext.Authors.Remove(item);
            _dbContext.SaveChanges();

        }
    }
}

