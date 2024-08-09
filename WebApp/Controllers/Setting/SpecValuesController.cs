using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.Master.MasterCategory;
using Domain.MasterComparisonType;
using Domain.MasterYardArea;
using Domain.Transaction.SpecValues;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ZXing.QrCode.Internal;

namespace WebApp.Controllers.Setting
{
    [Route("SpecValues")]
    public class SpecValuesController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly ITrnSpecValuesService _trnSpecValuesService;
        private readonly IMasterComparisonService _mstComparisonService;

        public SpecValuesController(ICommonService commonSvc, IMasterComparisonService mstComparisonService, ITrnSpecValuesService trnSpecValuesService)
        {
            _commonSvc = commonSvc;
            _mstComparisonService = mstComparisonService;
            _trnSpecValuesService = trnSpecValuesService;

		}

        [HttpGet]
        public async Task<IActionResult> Index()
        {

			var ret = new TTrnSpecValuesCreatedDto();
			ret.Category = await _commonSvc.SLGetCategory();

			return View(ViewPath.SpecValuesConf, ret);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(TTrnSpecValuesFilterDto param)
        {

            var result = await _trnSpecValuesService.GetListByParam(param);

            var specValues = result.Value.Select(r => new SpecItemValueDto
            {
				Items = r.Items,
				SubItems = r.SubItems,
                Value = r.Values,
                SpecItemCode = r.SpecItemCode
            }).ToList();

            TTrnSpecValuesDto ret = new TTrnSpecValuesDto
            {
				CategoryCode = param.CategoryCode,
				ModelCode = param.ModelCode,
                SpecValues = specValues
            };

			return PartialView(ViewPath.SpecValuesProcess, ret);

        }

        [HttpGet("Create")]
        public async Task<IActionResult> CreateAsync(int? id)
        {
           ViewData[ViewDataType.ModalType] = ModalType.Create;
           ViewData[ViewDataType.ModalTitle] = id.HasValue ? "Edit Spec Item" : "Create New Spec Item";



            return View(ViewPath.SpecValuesConf);
        }

		[HttpPost("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TTrnSpecValuesCreatedDto data)
		{

			if (!ModelState.IsValid)
			{
				string errors = GetModelStateErrors(data);
				return StatusCode(400, errors);
			}



			var ret = await _trnSpecValuesService.Create(User.GetUserClaims(), data);


			if (ret.IsSuccess)
			{
				return Ok();
			}
			else
			{
				var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

				return StatusCode(int.Parse(resp.StatusCode), resp.Message);
			}
		}
	}
}
