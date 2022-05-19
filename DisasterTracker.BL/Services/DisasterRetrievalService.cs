using AutoMapper;
using DisasterTracker.BL.Constants;
using DisasterTracker.BL.HttpClients;
using DisasterTracker.Data.Country;
using DisasterTracker.Data.Disaster;
using DisasterTracker.DataServices.Repository;
using DisasterTracker.PdcApiModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.BL.Services
{
    internal class DisasterRetrievalService : IDisasterRetrievalService
    {
        private readonly IPdcClient _pdcClient;
        private readonly ILogger<DisasterRetrievalService> _logger;
        private readonly IDisasterRepository _disasterRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public DisasterRetrievalService(
            IPdcClient pdcClient,
            ILogger<DisasterRetrievalService> logger,
            IDisasterRepository eventRepository,
            IMapper mapper,
            IConfiguration configuration,
            ICountryRepository countryRepository)
        {
            _pdcClient = pdcClient;
            _logger = logger;
            _disasterRepository = eventRepository;
            _mapper = mapper;
            _configuration = configuration;
            _countryRepository = countryRepository;
        }

        public async Task<(List<Disaster>, List<Disaster>)> CreateOrEditDisasters(CancellationToken stoppingToken)
        {
            try
            {
                CheckIfCancellationTokenWasCancelled(stoppingToken);
                var hazardBeans = await _pdcClient.GetHazardBeans();
                if (hazardBeans != null)
                {
                    var disasters = _mapper.Map<List<Disaster>>(hazardBeans.HazardBean);
                    return await CreateOrEditDisasters(disasters, stoppingToken);
                }
                else
                {
                    return (new List<Disaster>(), new List<Disaster>());
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task<(List<Disaster>, List<Disaster>)> CreateOrEditDisasters(List<Disaster> pdcDisasters, CancellationToken stoppingToken)
        {
            try
            {
                List<Disaster> updatedDisasters = new();
                List<Disaster> createdDisasters = new();
                foreach (var disaster in pdcDisasters)
                {
                    var disasterEntity = _disasterRepository.GetDisasterByApiId(disaster.ApiId);
                    if (disasterEntity != null)
                    {
                        var updatedDisaster = await EditExistingDisaster(disaster, disasterEntity);
                        if (updatedDisaster != null)
                        {
                            updatedDisasters.Add(updatedDisaster);
                        }
                    }
                    else
                    {
                        var createdDisaster = await CreateNewDisaster(disaster);
                        createdDisasters.Add(createdDisaster);
                    }

                    CheckIfCancellationTokenWasCancelled(stoppingToken);
                }
                return (createdDisasters, updatedDisasters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task<Disaster> CreateNewDisaster(Disaster disaster)
        {
            try
            {
                disaster = await GetDisasterToSave(disaster);

                await _disasterRepository.Insert(disaster, true);

                return disaster;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task<Disaster?> EditExistingDisaster(Disaster disaster, Disaster disasterEntity)
        {
            try
            {
                if (disaster.LastUpdateDate == disasterEntity.LastUpdateDate)
                {
                    return null;
                }
                else
                {
                    disaster = await UpdateDisasterToSave(disaster, disasterEntity);

                    await _disasterRepository.Update(disaster, true);

                    return disaster;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task<Disaster> GetDisasterToSave(Disaster disaster)
        {
            var lastUpdateId = await GetDisasterLastUpdateId(disaster);
            disaster.Id = Guid.NewGuid();
            disaster.UpdateId = lastUpdateId;

            var (statistics, countries) = await GetDisasterStatisticsAndCountries(disaster, lastUpdateId);
            disaster.DisasterStatistics = statistics;
            disaster.Countries = countries;

            disaster.DisasterImage = BuildEventImage(disaster.ApiId, lastUpdateId);

            return disaster;
        }

        private async Task<Disaster> UpdateDisasterToSave(Disaster disaster, Disaster entity)
        {
            var lastUpdateId = await GetDisasterLastUpdateId(disaster);

            entity.UpdateId = lastUpdateId;

            var (statistics, countries) = await GetDisasterStatisticsAndCountries(disaster, lastUpdateId);
            entity.DisasterStatistics = statistics;
            entity.Countries = countries;

            entity.DisasterImage = BuildEventImage(disaster.ApiId, lastUpdateId);

            entity.StartDate = disaster.StartDate;
            entity.EndDate = disaster.EndDate;
            entity.LastUpdateDate = disaster.LastUpdateDate;
            entity.AutoExpire = disaster.AutoExpire;
            entity.Description = disaster.Description;
            entity.Latitude = disaster.Latitude;
            entity.Longitude = disaster.Longitude;
            entity.Severity = disaster.Severity;
            entity.Type = disaster.Type;
            entity.Name = disaster.Name;

            return entity;
        }

        private async Task<string?> GetDisasterLastUpdateId(Disaster disaster)
        {
            try
            {
                var history = await _pdcClient.GetEventHistory(disaster.ApiId);
                if (history == null || history.Versions == null)
                {
                    return null;
                }
                else
                {
                    return history.Versions.Last();
                }
            }
            catch(HttpRequestException ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        private async Task<(List<DisasterStatistics>, List<CountryDisaster>)> GetDisasterStatisticsAndCountries(Disaster disaster, string? updateId)
        {
            try
            {
                if (updateId == null)
                {
                    return (new List<DisasterStatistics>(), new List<CountryDisaster>());
                }
                var eventDetails = await _pdcClient.GetEventSpecifics(disaster.ApiId, updateId);
                return (BuildDisasterStatisctics(eventDetails), BuildCountryDisasters(eventDetails));
            }
            catch(HttpRequestException ex)
            {
                _logger.LogError(ex, ex.Message);
                return (new List<DisasterStatistics>(), new List<CountryDisaster>());
            }
        }

        private List<DisasterStatistics> BuildDisasterStatisctics(DisasterSpecifics? disasterSpecifics)
        {
            if (disasterSpecifics == null || disasterSpecifics.Aims == null || disasterSpecifics.Aims.Data == null)
            {
                return new List<DisasterStatistics>();
            }
            var disasterStatistics = _mapper.Map<List<DisasterStatistics>>
                (disasterSpecifics.Aims.Data.Select(d => d.Values));
            foreach(var disasterStatistic in disasterStatistics)
            {
                disasterStatistic.CreatedOn = DateTime.UtcNow;
                disasterStatistic.ModifiedOn = DateTime.UtcNow;
            }
            return disasterStatistics;
        }

        private List<CountryDisaster> BuildCountryDisasters(DisasterSpecifics? disasterSpecifics)
        {
            var result = new List<CountryDisaster>();
            var countryPopulationData = disasterSpecifics?.Populations?.Data;
            
            if (countryPopulationData != null)
            {
                foreach(var countryPopulation in countryPopulationData)
                {
                    var country = _countryRepository.GetByName(countryPopulation.Country);
                    if (country != null && result.All(cd => cd.Country.Id!.Value != country.Id))
                    {
                        var countryDisaster = new CountryDisaster
                        {
                            CountryId = country.Id!.Value,
                            AffectedPopulation = countryPopulation.Total,
                            CreatedOn = DateTime.UtcNow,
                            ModifiedOn = DateTime.UtcNow
                        };

                        result.Add(countryDisaster);
                    }
                }
            }

            return result;
        }

        private DisasterImage? BuildEventImage(Guid apiId, string? updateId)
        {
            if (updateId == null)
            {
                return null;
            }

            var overviewMapAddress = _configuration[AddressConfigurationKeys.PdcOverviewMapAddressKey];
            var generalMapAddress = _configuration[AddressConfigurationKeys.PdcGeneralMapAddressKey];
            var infrastructureMap = _configuration[AddressConfigurationKeys.PdcInfrastructureMapAddressKey];
            var image = new DisasterImage
            {
                InfrastructureMapImageAddress = string.Format(infrastructureMap, apiId, updateId),
                OverviewMapImageAddress = string.Format(overviewMapAddress, apiId, updateId),
                MapImageAddress = string.Format(generalMapAddress, apiId, updateId),
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
            return image;
        }

        private void CheckIfCancellationTokenWasCancelled(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogError("Task was cancelled.");
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}
