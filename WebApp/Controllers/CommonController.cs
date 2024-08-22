using Core.Helpers;
using Domain.Common;
using Domain.Master;
using Domain.Master.MasterCategory;
using Domain.Master.MasterModel;
using Domain.MasterEmployee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CommonController : Controller
    {
        private readonly ICommonService _commonSvc;
        private readonly IMasterEmployeeService _empSvc;
        private readonly IMstModelService _mstModelService;
        private readonly IMstBrandService _mstBrandService;

        public CommonController(
            ICommonService commonSvc,
            IMasterEmployeeService empSvc,
            IMstModelService mstModelService,
            IMstBrandService mstBrandService
            )
        {
            _commonSvc = commonSvc;
            _empSvc = empSvc;
            _mstModelService = mstModelService;
            _mstBrandService = mstBrandService;
        }


        [HttpGet("get/employee/{nrp}")]
        public async Task<IActionResult> GetEmployeeByNRP(string nrp)
        {
            var ret = await _empSvc.GetByNRP(nrp);
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



        [HttpGet("get/modelByCategory/{categoryCode}")]
        public async Task<IActionResult> GetModelByCategoryId(string categoryCode)
        {
            
            var ret = await _mstModelService.GetByCategory(categoryCode);
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

            


        [HttpGet("get/modelByBrand/{brandCode}")]
        public async Task<IActionResult> GetModelByBrandId(string brandCode)
        {
            
            var ret = await _mstModelService.GetByBrand(brandCode);
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
        
        [HttpGet("get/modelByBrandTN/{brandCode}")]
        public async Task<IActionResult> GetModelByBrandTNId(string brandCode)
        {
            
            var ret = await _mstModelService.GetByBrandTN(brandCode);
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



        [HttpGet("get/brandByCategory/{categoryCode}")]
        public async Task<IActionResult> GetBrandByCategoryId(string categoryCode)
        {
            
            var ret = await _mstBrandService.GetByCategory(categoryCode);
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

        [HttpGet("get/brandByClass/{classCode}")]
        public async Task<IActionResult> GetBrandByClass(string classCode)
        {

            var ret = await _mstBrandService.GetByClass(classCode);
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

        [HttpGet("get/getDistributorByBrand/{brandCode}")]
        public async Task<IActionResult> GetDistributorByBrand(string brandCode)
        {

            var ret = await _mstModelService.GetAll();

                
            if (ret.IsSuccess)
            {
                var filteredData = ret.Value
                    .Where(m => m.BrandCode == brandCode && !string.IsNullOrEmpty(m.Distributor))
                    .Select(m => m.Distributor)
                    .Distinct() 
                    .ToList();
                return Ok(filteredData);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpGet("get/getModelByParam/{brandCode}/{distributor}/{classCode}")]
        public async Task<IActionResult> GetModelByParam(string brandCode, string distributor, string classCode)
        {

            var ret = await _mstModelService.GetAll();


            if (ret.IsSuccess)
            {
                var filteredData = ret.Value
                   .Where(m => (string.IsNullOrEmpty(brandCode) || m.BrandCode == brandCode) &&
                               (string.IsNullOrEmpty(distributor) || m.Distributor == distributor) &&
                               (string.IsNullOrEmpty(classCode) || m.ClassCode == classCode))
                   .ToList();
                return Ok(filteredData);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpGet("get/getDecryptedCategory/{encrypt}")]
        public IActionResult GetDecryptedCategory(string encrypt)
        {
            try
            {
                if (string.IsNullOrEmpty(encrypt))
                {
                    return BadRequest("The input is invalid or empty.");
                }

                // Attempt to decrypt the input
                var decryptedCategory = EncryptionHelper.AesDecrypt(encrypt);

                // Return the decrypted category as JSON
                return Json(decryptedCategory);
            }
            catch (Exception ex)
            {
                // Log the error if necessary
                Console.WriteLine($"Error during decryption: {ex.Message}");

                // Return a BadRequest with the error message
                return BadRequest("Decryption failed. Please check the input and try again.");
            }
        }


    }
}
