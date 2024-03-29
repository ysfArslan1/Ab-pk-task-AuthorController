﻿using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.AuthorsOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int Id { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly PatikaDbContext _dbContext;
        public UpdateAuthorCommand(PatikaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            // Alınan bilgilerle aynı kayıtın database bulunma durumuna bakılır.
            var item = _dbContext.Authors.Where(x => x.Id == Id).SingleOrDefault();
            if (item is null)
                throw new InvalidOperationException("Bulunamadı");

            if(_dbContext.Authors.Any(x=> x.Name.ToLower() == item.Name.ToLower() && x.Surname != item.Surname.ToLower()))
                throw new InvalidOperationException("Aynı isim bulunmakta");

            item.Name = Model.Name != default ? Model.Name : item.Name;
            item.Surname = Model.Surname != default ? Model.Surname : item.Surname;
            item.Birthdate = Model.Birthdate != default ? Model.Birthdate : item.Birthdate;

            // database işlemleri yapılır.
            _dbContext.Authors.Update(item);
            _dbContext.SaveChanges();

        }
    }
    // Author sınıfı düzenlemek için gerekli verilerin alındıgı sınıf.
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
