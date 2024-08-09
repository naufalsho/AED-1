using Core;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Json;

namespace Domain.ExtServices
{
    public interface IIoTService
    {
        Task<IoTResponse> AddDevice(IoTAddRequest request);
        Task<IoTResponse> DeleteDevice(IoTDeleteRequest request);
    }

    public class IoTService : IIoTService
    {
        private readonly IUnitOfWork _uow;
        private readonly ExtConfigs _extCfg;

        private readonly IHttpClientFactory _httpClientFactory;

        public IoTService(
            IUnitOfWork uow,
            IOptions<ExtConfigs> extCfg,
            IHttpClientFactory httpClientFactory)
        {
            _uow = uow;
            _extCfg = extCfg.Value;
            _httpClientFactory = httpClientFactory;
        }

        public Task<IoTResponse> AddDevice(IoTAddRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IoTResponse> DeleteDevice(IoTDeleteRequest request)
        {
            throw new NotImplementedException();
        }

        // public async Task<IoTResponse> AddDevice(IoTAddRequest request)
        // {
        //     var url = $"{_extCfg.IoTUrl}add-device.php";

        //     var logData = new TLogIntegrationIoT()
        //     {
        //         LogDate = DateTime.Now,
        //         Url = url,
        //         Request = JsonConvert.SerializeObject(request)
        //     };

        //     try
        //     {
        //         request.access_token = _extCfg.IoTAccessToken;

        //         var client = _httpClientFactory.CreateClient(HttpClientName.CommonClient);
        //         var response = await client.PostAsJsonAsync(url, request);

        //         var resResult = await response.Content.ReadAsStringAsync();
        //         var result = JsonConvert.DeserializeObject<IoTResponse>(resResult);

        //         logData.Result = resResult;
        //         await _uow.LogIntegrationIoT.Add(logData);
        //         await _uow.CompleteAsync();

        //         return result;
        //     }
        //     catch (Exception ex)
        //     {
        //         logData.Result = ex.GetMessage();
        //         await _uow.LogIntegrationIoT.Add(logData);
        //         await _uow.CompleteAsync();

        //         Log.Error("AddDevice to IoT Error! Message: {msg}", ex.GetMessage(), ex);
        //         throw new ArgumentException("Error", ex);
        //     }
        // }

        // public async Task<IoTResponse> DeleteDevice(IoTDeleteRequest request)
        // {
        //     var url = $"{_extCfg.IoTUrl}delete-device.php";

        //     var logData = new TLogIntegrationIoT()
        //     {
        //         LogDate = DateTime.Now,
        //         Url = url,
        //         Request = JsonConvert.SerializeObject(request)
        //     };

        //     try
        //     {
        //         request.access_token = _extCfg.IoTAccessToken;

        //         var client = _httpClientFactory.CreateClient(HttpClientName.CommonClient);
        //         var response = await client.PostAsJsonAsync(url, request);

        //         var resResult = await response.Content.ReadAsStringAsync();
        //         var result = JsonConvert.DeserializeObject<IoTResponse>(resResult);

        //         logData.Result = resResult;
        //         await _uow.LogIntegrationIoT.Add(logData);
        //         await _uow.CompleteAsync();

        //         return result;
        //     }
        //     catch (Exception ex)
        //     {
        //         logData.Result = ex.GetMessage();
        //         await _uow.LogIntegrationIoT.Add(logData);
        //         await _uow.CompleteAsync();

        //         Log.Error("AddDevice to IoT Error! Message: {msg}", ex.GetMessage(), ex);
        //         throw new ArgumentException("Error", ex);
        //     }
        // }
    }
}
