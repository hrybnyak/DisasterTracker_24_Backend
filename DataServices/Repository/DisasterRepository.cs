﻿using DisasterTracker.Data.Disaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.DataServices.Repository
{
    public class DisasterRepository : GenericRepository<Disaster>, IDisasterRepository
    {
        private readonly ILogger<DisasterRepository> _logger;

        public DisasterRepository(
            ApplicationDbContext context, 
            ILogger<DisasterRepository> logger) 
            : base(context)
        {
            _logger = logger;
        }

        public Disaster? GetDisasterByApiId(Guid apiId)
        {
            try
            {
                var result = _entities
                    .Include(d => d.DisasterImage)
                    .Include(d => d.DisasterStatistics)
                    .FirstOrDefault(d => d.ApiId == apiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public List<Disaster> GetRelevantDisasters()
        {
            try
            {
                var now = DateTime.UtcNow;
                var weekAgo = now.AddDays(-7);

                var result = _entities.Where(d => d.EndDate >= now || d.LastUpdateDate >= weekAgo)
                    .ToList();
                
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public Disaster? GetFullDisaster(Guid id)
        {
            try
            {
                var result = _entities
                    .Include(d => d.DisasterStatistics)
                    .Include(d => d.DisasterImage)
                    .FirstOrDefault(d => d.Id == id);

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}