using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.Common;
using Domain.Master.MasterModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Master
{
    [Route("Model")]
    public class ModelController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMstModelService _mstModelService;

        public ModelController(ICommonService commonSvc, IMstModelService mstModelService)
        {
            _commonSvc = commonSvc;
            _mstModelService = mstModelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(ViewPath.MasterModel);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _mstModelService.GetAll();

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
        public async Task<IActionResult> CreateAsync()
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Create New Model";

            var getLastCode = await _mstModelService.GetLastCode();

            var ret = new TMstModelCreatedDto
            {
                Code = getLastCode.Value,
                IsActive = true,
                Class = await _commonSvc.SLGetClass(),
                Brand = await _commonSvc.SLGetBrand(),
                Cap = await _commonSvc.SLGetCap(),
                LiftingHeight = await _commonSvc.SLGetLiftingHeight(),
                MastType = await _commonSvc.SLGetMastType(),
                Tire = await _commonSvc.SLGetTire()
            };

            return View(ViewPath.MasterModelProcess, ret);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TMstModelCreatedDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }

            var ret = await _mstModelService.Create(User.GetUserClaims(), data);

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
            ViewData[ViewDataType.ModalTitle] = "Edit Model";

            var ret = new TMstModelCreatedDto();

            var result = await _mstModelService.GetById(Id);

            ret.Code = result.Value.Code;
            ret.Model = result.Value.Model;
            ret.Type = result.Value.Type;
            ret.Distributor = result.Value.Distributor;
            ret.ClassCode = result.Value.ClassCode;
            ret.BrandCode = result.Value.BrandCode;
            ret.Country = result.Value.Country;
            ret.IsActive = result.Value.IsActive;
            ret.Class = await _commonSvc.SLGetClass();
            ret.Brand = await _commonSvc.SLGetBrand();
            ret.CapCode = result.Value.CapCode;
            ret.LiftingHeightCode = result.Value.LiftingHeightCode;
            ret.MastTypeCode = result.Value.MastTypeCode;
            ret.TireCode = result.Value.TireCode;
            ret.Cap = await _commonSvc.SLGetCap();
            ret.LiftingHeight = await _commonSvc.SLGetLiftingHeight();
            ret.MastType = await _commonSvc.SLGetMastType();
            ret.Tire = await _commonSvc.SLGetTire();

            return View(ViewPath.MasterModelProcess, ret);
        }


        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TMstModelCreatedDto data)
        {


            var ret = await _mstModelService.Update(User.GetUserClaims(), data);


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
            var ret = await _mstModelService.Delete(User.GetUserClaims(), id);

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
