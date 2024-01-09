
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.GenresOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGenreCommand(PatikaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var item = _dbContext.Genres.Where(x => x.Name == Model.Name ).SingleOrDefault();
            if (item is not null)
                throw new InvalidOperationException("Zaten Mevcut");

            item = _mapper.Map<Genre>(Model);
            // database işlemleri yapılır.
            _dbContext.Genres.Add(item);
            _dbContext.SaveChanges();

        }
    }
    // genre sınıfı üretmek için gerekli verilerin alındıgı sınıf.
    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
