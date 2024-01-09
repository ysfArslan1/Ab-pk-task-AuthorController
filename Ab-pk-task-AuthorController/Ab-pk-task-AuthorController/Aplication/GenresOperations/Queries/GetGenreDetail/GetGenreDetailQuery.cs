using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;

namespace Ab_pk_task3.Aplication.GenresOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;
        public int Id { get; set; }
        public GetGenreDetailQuery(PatikaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var item = _dbContext.Genres.Where(x => x.Id == Id).SingleOrDefault();
            if (item is null)
                throw new InvalidOperationException("Bulunamadı");

            GenreDetailViewModel itemDetail = _mapper.Map<GenreDetailViewModel>(item);

            return itemDetail;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
