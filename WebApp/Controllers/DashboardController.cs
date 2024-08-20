using Core;
using Core.Helpers;
using Domain.Dashboard;
using Domain.Master;
using Domain.Master.MasterCategory;
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
        private readonly IMstCategoryService _mstCategoryService;
        private readonly IMstBrandService _mstBrandService;
        private string urlPowerBiServiceApiRoot;

        public DashboardController(IDashboardService dsSvc, IMstCategoryService mstCategoryService, IMstBrandService mstBrandService)
        {
            _mstCategoryService = mstCategoryService;
            _mstBrandService = mstBrandService;
            _dsSvc = dsSvc;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        public async Task<IActionResult> Index()
        {
            var result = await _dsSvc.GetDescriptionGroupsAsync();

            if (result.IsSuccess)
            {
                // Initialize Features for each brand
                foreach (var group in result.Value)
                {
                    foreach (var brand in group.Brands)
                    {
                        // Fetch brand-specific features (this logic can be adjusted as per your requirements)
                        brand.Features = await GetBrandFeaturesAsync(brand.Name, group.Code);
                    }
                }

                return View(ViewPath.Dashboard2, result.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(result.Reasons.First().Message);
                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        // Action to get brand-specific features
        public async Task<IActionResult> BrandDetails(string brandName, string codeCategory)
        {
            var brandFeatures = await GetBrandFeaturesAsync(brandName, codeCategory);

            if (brandFeatures == null)
            {
                return NotFound();
            }

            return View("BrandDetails", brandFeatures);
        }

        private async Task<List<FeatureDto>> GetBrandFeaturesAsync(string brandName, string codeCategory)
        {
            string encryptedType = EncryptionHelper.AesEncrypt(codeCategory);
            // Simulate fetching brand-specific features from a service or database
            // You can adjust this logic to actually fetch data from your database if needed
            var features = new List<FeatureDto>
        {
            //new FeatureDto { Name = "Product Specification", IconClass = "fa-list", Url = Url.Action("Specification", "Dashboard", new { brandName }) },
            new FeatureDto { Name = "Product Specification", IconClass = "fa-list", Url = Url.Action("Index", "ProductSpec", new { category = encryptedType , brandName = brandName}) },
            new FeatureDto { Name = "Product Comparison", IconClass = "fa-repeat", Url = Url.Action("Index", "Comparison") },
            //new FeatureDto { Name = "Product Specification", IconClass = "fa-list", Url = Url.Action("", "ProductSpec") },
            //new FeatureDto { Name = "Product Comparison", IconClass = "fa-repeat", Url = Url.Action("", "Comparison") },
            new FeatureDto { Name = "Application Handbook", IconClass = "fa-book-open-reader", Url = "http://10.0.10.74:9590/moodle/login/index.php" },
            new FeatureDto {Name = "Implement Compability", IconClass = "fa-folder-open", Url = Url.Action("", "ImplementCompability") },
            //new FeatureDto { Name = "Productivity Calculator", IconClass = "fa-calculator", Url = Url.Action("Calculator", "Dashboard", new { brandName }) },

        };

            // You can add logic here to customize features per brand
            // For example, removing certain features for specific brands
            if (brandName == "Cannycom")
            {
                features = features.Take(3).ToList(); // Take only the first two features for this specific brand
            }

            return await Task.FromResult(features);
        }

        // Placeholder actions for demonstration purposes
        //public IActionResult Calculator(string brandName) => Content($"Calculator for {brandName}");
        //public IActionResult Specification(string brandName) => Content($"Specification for {brandName}");
        //public IActionResult Comparison(string brandName) => Content($"Comparison for {brandName}");

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
