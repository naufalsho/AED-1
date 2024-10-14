using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.Master;
using Domain.Master.Cap;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Master
{
    [Route("Cap")]
    public class CapController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMstCapService _mstCapService;

        public CapController(ICommonService commonSvc, IMstCapService mstCapService)
        {
            _commonSvc = commonSvc;
            _mstCapService = mstCapService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(ViewPath.MasterCap);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _mstCapService.GetAll();

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
            ViewData[ViewDataType.ModalTitle] = "Create New Cap";

            var ret = new TMstCapCreatedDto();

            var getLastCode = await _mstCapService.GetLastCode();
            ret.Code = getLastCode.Value;
            ret.IsActive = true;

            return View(ViewPath.MasterCapProcess, ret);
        }


        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TMstCapCreatedDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }

            var ret = await _mstCapService.Create(User.GetUserClaims(), data);

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
            ViewData[ViewDataType.ModalTitle] = "Edit Cap";

            var ret = new TMstCapCreatedDto();

            var result = await _mstCapService.GetById(Id);

            ret.Code = result.Value.Code;
            ret.Name = result.Value.Name;
            ret.IsActive = result.Value.IsActive;

            return View(ViewPath.MasterCapProcess, ret);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TMstCapCreatedDto data)
        {


            var ret = await _mstCapService.Update(User.GetUserClaims(), data);


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
            var ret = await _mstCapService.Delete(User.GetUserClaims(), id);

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
