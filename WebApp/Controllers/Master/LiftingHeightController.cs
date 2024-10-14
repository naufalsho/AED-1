using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.Common;
using Domain.Master.Cap;
using Domain.Master.LiftingHeight;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Master
{
    [Route("LiftingHeight")]
    public class LiftingHeightController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMstLiftingHeightService _mstLiftingHeightService;

        public LiftingHeightController(ICommonService commonSvc, IMstLiftingHeightService mstLiftingHeightService)
        {
            _commonSvc = commonSvc;
            _mstLiftingHeightService = mstLiftingHeightService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(ViewPath.MasterLiftingHeight);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _mstLiftingHeightService.GetAll();

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
        public async Task<IActionResult> CreateAsync(int? id)
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Create New Lifting Height";

            var ret = new TMstLiftingHeightCreatedDto();

            var getLastCode = await _mstLiftingHeightService.GetLastCode();
            ret.Code = getLastCode.Value;
            ret.IsActive = true;

            return View(ViewPath.MasterLiftingHeightProcess, ret);
        }


        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TMstLiftingHeightCreatedDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }

            var ret = await _mstLiftingHeightService.Create(User.GetUserClaims(), data);

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
            ViewData[ViewDataType.ModalTitle] = "Edit Lifting Height";

            var ret = new TMstLiftingHeightCreatedDto();

            var result = await _mstLiftingHeightService.GetById(Id);

            ret.Code = result.Value.Code;
            ret.Name = result.Value.Name;
            ret.IsActive = result.Value.IsActive;

            return View(ViewPath.MasterLiftingHeightProcess, ret);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TMstLiftingHeightCreatedDto data)
        {


            var ret = await _mstLiftingHeightService.Update(User.GetUserClaims(), data);


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
            var ret = await _mstLiftingHeightService.Delete(User.GetUserClaims(), id);

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
