
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Repositories
{
    public class VergaderruimteRepository : IVergaderruimteRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Vergaderruimte> _ruimtes;

        public VergaderruimteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _ruimtes = _dbContext.Vergaderruimtes;
        }

        public IEnumerable<Vergaderruimte> GetAll()
        {
            return _ruimtes.OrderBy(v => v.Naam)
                .ThenBy(v => v.MaximumAantalPersonen)
                .AsNoTracking()
                .ToList();
        }

        public Vergaderruimte GetById(int id)
        {
            return _ruimtes.Include(v => v.Reservaties).SingleOrDefault(v => v.Id == id);
        }

        public IEnumerable<Vergaderruimte> GetByMaxAantalPersonen(int maxAantalPersonen)
        {
            return _ruimtes.Where(v => v.MaximumAantalPersonen == maxAantalPersonen)
                .OrderBy(v => v.Naam)
                .ThenBy(v => v.MaximumAantalPersonen).ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
