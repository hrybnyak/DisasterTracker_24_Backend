using DisasterTracker.PdcApiModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DisasterTracker.BL.HttpClients
{
    public class PdcClient : IPdcClient
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
                var pdcAllHazardBeansAddress = _configuration["PdcAllHazardsAddress"];

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

        public async Task<EventHistory?> GetEventHistory(Guid eventId)
        {
            try
            {
                var pdcHazardHistoryAddress = _configuration["PdcHazardHistoryAddress"];
                pdcHazardHistoryAddress = string.Format(pdcHazardHistoryAddress, eventId);

                var getHistory = await _httpClient.GetAsync(pdcHazardHistoryAddress);

                if (getHistory.IsSuccessStatusCode)
                {
                    var result = await getHistory.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EventHistory>(result);
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

        public async Task<EventSpecifics?> GetEventSpecifics(Guid eventId, string updateId)
        {
            try
            {
                var pdcHazardSpecificsAddress = _configuration["PdcHazardDetailsAddress"];
                pdcHazardSpecificsAddress = string.Format(pdcHazardSpecificsAddress, eventId, updateId);

                var getEventSpecifics = await _httpClient.GetAsync(pdcHazardSpecificsAddress);

                if (getEventSpecifics.IsSuccessStatusCode)
                {
                    var result = await getEventSpecifics.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EventSpecifics>(result);
                }
                else
                {
                    _logger.LogError("The request has returned non-successful response. StatusCode : {1}, Reason: {2}",
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