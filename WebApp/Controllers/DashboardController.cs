using Core;
using Core.Helpers;
using Domain.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;

namespace WebApp.Controllers
{
    public class DashboardController : BasePageController
    {
        private readonly IDashboardService _dsSvc;
        private string urlPowerBiServiceApiRoot;

        public DashboardController(IDashboardService dsSvc)
        {
            _dsSvc = dsSvc;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        public async Task<IActionResult> Index()
        {
            
            // var accessToken = await GetAccessToken();
            // var embedToken = await GetEmbedToken(accessToken, Guid.Empty, Guid.Empty);

            // ViewBag.EmbedUrl = $"https://app.powerbi.com/reportEmbed?reportId=YOUR_REPORT_ID&groupId=YOUR_GROUP_ID";
            // ViewBag.EmbedToken = embedToken.Token;
            // ViewBag.ReportId = "YOUR_REPORT_ID";            
            return View(ViewPath.Dashboard2);
        }

        private async Task<EmbedToken> GetEmbedToken(string accessToken, Guid groupId, Guid reportId)
        {
            var tokenCredentials = new TokenCredentials(accessToken, "Bearer");
            using (var client = new PowerBIClient(new Uri("https://api.powerbi.com/"), tokenCredentials))
            {
                var reports = await client.Reports.GetReportInGroupAsync(groupId, reportId);
                var datasetId = reports.DatasetId;

                var tokenRequest = new GenerateTokenRequestV2
                {
                    Reports = new List<GenerateTokenRequestV2Report> { new GenerateTokenRequestV2Report(reportId) },
                    Datasets = new List<GenerateTokenRequestV2Dataset> { new GenerateTokenRequestV2Dataset(datasetId) }
                };

                var embedToken = await client.EmbedToken.GenerateTokenAsync(tokenRequest);
                return embedToken;
            }
        }

        private async Task<string> GetAccessToken()
        {
            var tenantId = "YOUR_TENANT_ID";
            var clientId = "YOUR_CLIENT_ID";
            var clientSecret = "YOUR_CLIENT_SECRET";
            var authority = $"https://login.microsoftonline.com/{tenantId}";

            var app = ConfidentialClientApplicationBuilder.Create(clientId)
                                                        .WithClientSecret(clientSecret)
                                                        .WithAuthority(new Uri(authority))
                                                        .Build();

            string[] scopes = { "https://analysis.windows.net/powerbi/api/.default" };
            var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

            return result.AccessToken;
        }



        [HttpGet("Dashboard/DeviceStockStatus")]
        public async Task<IActionResult> DeviceStockStatus()
        {
            var ret = await _dsSvc.GetDsDeviceStockStatus();

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpGet("Dashboard/DeviceAllocated")]
        public async Task<IActionResult> DeviceAllocated()
        {
            var ret = await _dsSvc.GetDsDeviceAllocated();

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpGet("Dashboard/DeviceStockCategory")]
        public async Task<IActionResult> DeviceStockCategory()
        {
            var ret = await _dsSvc.GetDsDeviceStockCategory();

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpGet("Dashboard/DeviceStockBrand")]
        public async Task<IActionResult> DeviceStockBrand()
        {
            var ret = await _dsSvc.GetDsDeviceStockBrand();

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpGet("Dashboard/DeviceYoY")]
        public async Task<IActionResult> DeviceYoY()
        {
            var ret = await _dsSvc.GetDsDeviceYoY();

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpGet("Dashboard/DeviceEndPeriod")]
        public async Task<IActionResult> DeviceEndPeriod()
        {
            var ret = await _dsSvc.GetDsDeviceEndPeriod();

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpGet("Dashboard/DeviceEndPeriodYear")]
        public async Task<IActionResult> DeviceEndPeriodYear()
        {
            var ret = await _dsSvc.GetDsDeviceEndPeriodYear();

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }
    }
}
