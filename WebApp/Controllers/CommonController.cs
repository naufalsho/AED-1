using Core.Helpers;
using Domain.Common;
using Domain.Master;
using Domain.Master.Cap;
using Domain.Master.LiftingHeight;
using Domain.Master.MasterCategory;
using Domain.Master.MasterModel;
using Domain.Master.MastType;
using Domain.Master.Tire;
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
        private readonly IMstCapService _mstCapService;
        private readonly IMstLiftingHeightService _mstLiftingHeightService;
        private readonly IMstMastTypeService _mstMastTypeService;
        private readonly IMstTireService _mstTireService;

        public CommonController(
            ICommonService commonSvc,
            IMasterEmployeeService empSvc,
            IMstModelService mstModelService,
            IMstBrandService mstBrandService,
            IMstCapService mstCapService,
            IMstLiftingHeightService mstLiftingHeightService,
            IMstMastTypeService mstMastTypeService,
            IMstTireService mstTireService
            )
        {
            _commonSvc = commonSvc;
            _empSvc = empSvc;
            _mstModelService = mstModelService;
            _mstBrandService = mstBrandService;
            _mstCapService = mstCapService;
            _mstLiftingHeightService = mstLiftingHeightService;
            _mstMastTypeService = mstMastTypeService;
            _mstTireService = mstTireService;
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

        [HttpGet("get/brandByClass/{classCode}/{distributor}")]
        public async Task<IActionResult> GetBrandByClass(string classCode, string distributor)
        {

            var ret = await _mstBrandService.GetByClass(classCode, distributor);
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
        public async Task<IActionResult> GetModelByParam(
                                                     string brandCode,
                                                     string distributor,
                                                     string classCode,
                                                     string? capCode = null,  // Parameter opsional untuk MHB
                                                     string? mastTypeCode = null,
                                                     string? liftingHeightCode = null,
                                                     string? tireCode = null)
        {
            var ret = await _mstModelService.GetAll();

            if (ret.IsSuccess)
            {

                var filteredData = ret.Value
                    .Where(m => (string.IsNullOrEmpty(brandCode) || m.BrandCode == brandCode)
                                && (string.IsNullOrEmpty(distributor) || m.Distributor == distributor)
                                && (string.IsNullOrEmpty(classCode) || m.ClassCode == classCode)
                                && m.IsActive
                                )
                    .ToList();

                // filter jika untuk MHB
                if (capCode != null)
                {
                    filteredData = ret.Value
                    .Where(m => (string.IsNullOrEmpty(brandCode) || m.BrandCode == brandCode)
                                && (string.IsNullOrEmpty(distributor) || m.Distributor == distributor)
                                && (string.IsNullOrEmpty(classCode) || m.ClassCode == classCode)
                                // Filter untuk MHB: hanya diterapkan jika capCode dan parameter lain tidak null
                                && (string.IsNullOrEmpty(capCode) || m.CapCode == capCode)
                                && (string.IsNullOrEmpty(mastTypeCode) || m.MastTypeCode == mastTypeCode)
                                && (string.IsNullOrEmpty(liftingHeightCode) || m.LiftingHeightCode == liftingHeightCode)
                                && (string.IsNullOrEmpty(tireCode) || m.TireCode == tireCode)
                                && m.IsActive
                                )
                                // Grouping berdasarkan kolom tertentu yang diinginkan
                                .GroupBy(m => new
                                {
                                    m.Model,
                                    m.Type,
                                    m.Distributor,
                                    m.Country,
                                    m.BrandCode,
                                    m.ClassCode,
                                    m.ModelImage,
                                    m.CapCode
                                })
                                // Mengambil item pertama dalam setiap group
                                .Select(g => g.First()) // Mengambil perwakilan dari setiap grup
                    .ToList();
                }

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


        [HttpGet("get/getCapByBrand/{brandCode}/{classCode}/{distributor}")]
        public async Task<IActionResult> GetCapByBrand(string brandCode, string classCode, string distributor)
        {

            var ret = await _mstCapService.GetByBrand(brandCode, classCode, distributor);
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

        [HttpGet("get/getMastTypeByCap/{brandCode}/{classCode}/{distributor}/{capCode}")]
        public async Task<IActionResult> GetMastTypeByCap(string brandCode, string classCode, string distributor, string capCode)
        {

            var ret = await _mstMastTypeService.GetByCap(brandCode, classCode, distributor, capCode);
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

        [HttpGet("get/getLiftingHeightByMastType/{brandCode}/{classCode}/{distributor}/{capCode}/{mastTypeCode}")]
        public async Task<IActionResult> GetLiftingHeightByMastType(string brandCode, string classCode, string distributor, string capCode, string mastTypeCode)
        {

            var ret = await _mstLiftingHeightService.GetByMastType(brandCode, classCode, distributor, capCode, mastTypeCode);
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

        [HttpGet("get/getTireByLiftingHeight/{brandCode}/{classCode}/{distributor}/{capCode}/{mastTypeCode}/{liftingHeightCode}")]
        public async Task<IActionResult> GetTireByLiftingHeight(string brandCode, string classCode, string distributor, string capCode, string mastTypeCode, string liftingHeightCode)
        {

            var ret = await _mstTireService.GetByLiftingHeight(brandCode, classCode, distributor, capCode, mastTypeCode,liftingHeightCode);
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
