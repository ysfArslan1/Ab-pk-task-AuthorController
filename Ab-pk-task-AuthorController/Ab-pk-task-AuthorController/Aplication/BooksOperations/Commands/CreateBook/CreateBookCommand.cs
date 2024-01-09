
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.BooksOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(PatikaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var item = _dbContext.Books.Where(x => x.Title == Model.Title ).SingleOrDefault();
            if (item is not null)
                throw new InvalidOperationException("Zaten Mevcut");


            item = _mapper.Map<Book>(Model);
            // database işlemleri yapılır.
            _dbContext.Books.Add(item);
            _dbContext.SaveChanges();

        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
