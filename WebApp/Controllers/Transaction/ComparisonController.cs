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
        public async Task<IActionResult> Index(string type)
        {
            var categoryResult = await _mstCategoryService.GetAll();
            int typeNum = 0;
            if (type == "Acgon-agri")
            {
                typeNum = 1;
            }
            else if (type == "Acgon-construction")
            {
                typeNum = 2;
            }
            else if (type == "MHD")
            {
                typeNum = 3;
            }
            else if (type == "Power")
            {
                typeNum = 4;
            }

            var ret = new ComparisonDto
            {
                SLClass = await _commonSvc.SLGetClass(),
                Category = categoryResult.Value
                                        .Where(r => r.Type == typeNum)
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
