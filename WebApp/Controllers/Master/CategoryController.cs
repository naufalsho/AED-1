using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.Master;
using Domain.Master.MasterCategory;
using Domain.MasterComparisonType;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ZXing.QrCode.Internal;

namespace WebApp.Controllers.Master
{
    [Route("Category")]
    public class CategoryController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMstCategoryService _mstCategoryService;
        private readonly IMstBrandService _mstBrandService;
        private readonly ICommonService _commonService;

        public CategoryController(ICommonService commonSvc, IMstCategoryService mstCategoryService, IMstBrandService mstBrandService, ICommonService commonService)
        {
            _commonSvc = commonSvc;
            _mstCategoryService = mstCategoryService;
            _mstBrandService = mstBrandService;
            _commonService = commonService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(ViewPath.MasterCategory);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _mstCategoryService.GetAll();

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

		[HttpGet("GetList/{type}")]
		public async Task<IActionResult> GetList(string type)
		{

			var ret = await _mstCategoryService.GetAll(type);

			if (ret.IsSuccess)
			{
				return Ok(ret.Value);
			}
			else
			{
				var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

				return StatusCode(int.Parse(resp.StatusCode), resp.Message);
			}

			return StatusCode(400, ":Data Not Found");

		}

		[HttpGet("Create")]
        public async Task<IActionResult> CreateAsync()
        {
           ViewData[ViewDataType.ModalType] = ModalType.Create;
           ViewData[ViewDataType.ModalTitle] = "Create New Category";


            var ret = new TMstCategoryCreatedDto();
            
            var getLastCode = await _mstCategoryService.GetLastCode();

            ret.Code = getLastCode.Value;
            ret.IsActive = true;


            ret.Brands = await _commonService.SLGetBrand();


            
            return View(ViewPath.MasterCategoryProcess, ret);
        }



        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TMstCategoryCreatedDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }



            var ret = await _mstCategoryService.Create(User.GetUserClaims(), data);


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

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(string Id)
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Edit Category";

            var ret = new TMstCategoryCreatedDto();


            var result = await _mstCategoryService.GetById(Id);


            ret.Code = result.Value.Code;
            ret.Description = result.Value.Description;
            ret.Tag = result.Value.Tag;
            ret.IsActive = result.Value.IsActive;
            ret.Brands = await _commonService.SLGetBrand();
            ret.BrandCode = result.Value.CategoryDetails.Select(cd => cd.BrandCode).ToList();


            return View(ViewPath.MasterCategoryProcess, ret);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TMstCategoryUpdatedDto data)
        {


            var ret = await _mstCategoryService.Update(User.GetUserClaims(), data);


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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ret = await _mstCategoryService.Delete(User.GetUserClaims(), id);

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
