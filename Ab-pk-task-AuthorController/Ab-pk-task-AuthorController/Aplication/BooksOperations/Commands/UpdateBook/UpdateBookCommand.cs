﻿using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.BooksOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public int Id { get; set; }
        public UpdateBookModel Model { get; set; }
        private readonly PatikaDbContext _dbContext;
        public UpdateBookCommand(PatikaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var item = _dbContext.Books.Where(x => x.Id == Id).SingleOrDefault();
            if (item is null)
                throw new InvalidOperationException(" Bulunamadı");

            if (_dbContext.Books.Any(x => x.Title.ToLower() == item.Title.ToLower() && x.Id != item.Id))
                throw new InvalidOperationException("Aynı isim bulunmakta");


            item.Title = Model.Title != default ? Model.Title : item.Title;
            item.GenreId = Model.GenreId != default ? Model.GenreId : item.GenreId;
            item.PageCount = Model.PageCount != default ? Model.PageCount : item.PageCount;
            item.PublishDate = Model.PublishDate != default ? Model.PublishDate : item.PublishDate;

            // database işlemleri yapılır.
            _dbContext.Books.Update(item);
            _dbContext.SaveChanges();

        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
