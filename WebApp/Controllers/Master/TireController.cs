using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.Master.Tire;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Master
{
    [Route("Tire")]
    public class TireController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMstTireService _mstTireService;

        public TireController(ICommonService commonSvc, IMstTireService mstTireService)
        {
            _commonSvc = commonSvc;
            _mstTireService = mstTireService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(ViewPath.MasterTire);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _mstTireService.GetAll();

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
            ViewData[ViewDataType.ModalTitle] = "Create New Tire";

            var ret = new TMstTireCreatedDto();

            var getLastCode = await _mstTireService.GetLastCode();
            ret.Code = getLastCode.Value;
            ret.IsActive = true;

            return View(ViewPath.MasterTireProcess, ret);
        }


        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TMstTireCreatedDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }

            var ret = await _mstTireService.Create(User.GetUserClaims(), data);

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
            ViewData[ViewDataType.ModalTitle] = "Edit Tire";

            var ret = new TMstTireCreatedDto();

            var result = await _mstTireService.GetById(Id);

            ret.Code = result.Value.Code;
            ret.Name = result.Value.Name;
            ret.IsActive = result.Value.IsActive;

            return View(ViewPath.MasterTireProcess, ret);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TMstTireCreatedDto data)
        {


            var ret = await _mstTireService.Update(User.GetUserClaims(), data);


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
            var ret = await _mstTireService.Delete(User.GetUserClaims(), id);

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
