using DisasterTracker.BL.Constants;
using DisasterTracker.PdcApiModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DisasterTracker.BL.HttpClients
{
    internal class PdcClient : IPdcClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PdcClient> _logger;
        private readonly IConfiguration _configuration;

        public PdcClient(
            HttpClient httpClient,
            ILogger<PdcClient> logger,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<HazardBeans?> GetHazardBeans()
        {
            try
            {
                var pdcAllHazardBeansAddress = _configuration[AddressConfigurationKeys.PdcAllHazardBeansAddressKey];

                var getHazards = await _httpClient.GetAsync(pdcAllHazardBeansAddress);

                if (getHazards.IsSuccessStatusCode)
                {
                    var serializer = new XmlSerializer(typeof(HazardBeans));
                    var result = getHazards.Content.ReadAsStream();
                    return (HazardBeans?)serializer.Deserialize(result);
                }
                else
                {
                    _logger.LogError("The request has returned non-successful response. StatusCode : {1}, Reason: {2}",
                        getHazards.StatusCode,
                        getHazards.ReasonPhrase);
                    throw new HttpRequestException("The request has returned non-successful response.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<DisasterHistory?> GetEventHistory(Guid eventId)
        {
            try
            {
                var pdcHazardHistoryAddress = _configuration[AddressConfigurationKeys.PdcHazardHistoryAddressKey];
                pdcHazardHistoryAddress = string.Format(pdcHazardHistoryAddress, eventId);

                var getHistory = await _httpClient.GetAsync(pdcHazardHistoryAddress);

                if (getHistory.IsSuccessStatusCode)
                {
                    var result = await getHistory.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DisasterHistory>(result);
                }
                else
                {
                    _logger.LogError("The request has returned non-successful response. StatusCode : {1}, Reason: {2}",
                        getHistory.StatusCode, 
                        getHistory.ReasonPhrase);
                    throw new HttpRequestException("The request has returned non-successful response.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<DisasterSpecifics?> GetEventSpecifics(Guid eventId, string updateId)
        {
            try
            {
                var pdcHazardSpecificsAddress = _configuration["PdcHazardDetailsAddress"];
                pdcHazardSpecificsAddress = string.Format(pdcHazardSpecificsAddress, eventId, updateId);

                var getEventSpecifics = await _httpClient.GetAsync(pdcHazardSpecificsAddress);

                if (getEventSpecifics.IsSuccessStatusCode)
                {
                    var result = await getEventSpecifics.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DisasterSpecifics>(result);
                }
                else
                {
                    _logger.LogError("The request has returned non-successful response. StatusCode : {0}, Reason: {1}",
                        getEventSpecifics.StatusCode,
                        getEventSpecifics.ReasonPhrase);
                    throw new HttpRequestException("The request has returned non-successful response.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}