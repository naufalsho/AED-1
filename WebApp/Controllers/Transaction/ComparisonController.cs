using Core;
using Core.Helpers;
using Domain.Common;
using Domain.Comparison;
using Domain.Master.MasterCategory;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Transaction
{
    [Route("Comparison")]
    public class ComparisonController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IComparisonService _comparisonService;
        private readonly IMstCategoryService _mstCategoryService;

        public ComparisonController(ICommonService commonSvc, IComparisonService comparisonService, IMstCategoryService mstCategoryService)
        {
            _commonSvc = commonSvc;
            _comparisonService = comparisonService;
            _mstCategoryService = mstCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string brandName, string category)
        {
            // Decrypt the 'encryptedType' parameter
            //string decryptedType = EncryptionHelper.AesDecrypt(category);


            //// Convert the decryptedType to the necessary type (e.g., integer)
            //string codeCategory = decryptedType;

            // Fetch the categories
            var categoryResult = await _mstCategoryService.GetAll();

            var ret = new ComparisonDto
            {
                SLClass = await _commonSvc.SLGetClassByBrand(brandName, Distributor.ProductTN),
                Category = categoryResult.Value
                                        //.Where(r => r.Type == 1 && r.Tag =="TN")
                                        .Where(r => r.Type == 1)
                                        .Select(r => new TMstCategoryDto
                                        {
                                            Code = r.Code,
                                            Description = r.Description,
                                            Type = r.Type
                                        })
                                        .ToList()
            };


            return View(ret);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(ComparisonFilterDto param)
        {
            var ret = await _comparisonService.Generate(param);
            if(ret.IsFailed)
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }

            return PartialView(ViewPath.ComparisonProcess, ret.Value);

        }
    }
}
