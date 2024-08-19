using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.Master;
using Domain.MasterComparisonType;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ZXing.QrCode.Internal;

namespace WebApp.Controllers.Master
{
    [Route("Brand")]
    public class BrandController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMstBrandService _mstBrandService;

        public BrandController(ICommonService commonSvc, IMstBrandService brandService)
        {
            _commonSvc = commonSvc;
            _mstBrandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(ViewPath.MasterBrand);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _mstBrandService.GetAll();

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

        [HttpGet("Create")]
        public async Task<IActionResult> CreateAsync(string Id = "", bool IsUpdate = false)
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] ="Create New Brand";

            var ret = new TMstBrandCreateDto();

            var getLastCode = await _mstBrandService.GetLastCode();

            ret.Code = getLastCode.Value;

            return View(ViewPath.MasterBrandProcess, ret);
        }


        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TMstBrandCreateDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }
    
            var ret = await _mstBrandService.Create(User.GetUserClaims(), data);

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
            ViewData[ViewDataType.ModalTitle] = "Edit Brand";

            var ret = new TMstBrandCreateDto();

            var result = await _mstBrandService.GetById(Id);

            ret.Code = result.Value.Code;
            ret.Name = result.Value.Name;
            ret.Country = result.Value.Country;
            ret.IsActive = result.Value.IsActive;

            return View(ViewPath.MasterBrandProcess, ret);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TMstBrandCreateDto data)
        {


            var ret = await _mstBrandService.Update(User.GetUserClaims(), data);


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
            var ret = await _mstBrandService.Delete(User.GetUserClaims(), id);

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
