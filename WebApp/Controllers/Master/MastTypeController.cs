using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.Common;
using Domain.Master.MastType;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Master
{
    [Route("MastType")]
    public class MastTypeController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMstMastTypeService _mstMastTypeService;

        public MastTypeController(ICommonService commonSvc, IMstMastTypeService mstMastTypeService)
        {
            _commonSvc = commonSvc;
            _mstMastTypeService = mstMastTypeService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(ViewPath.MasterMastType);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _mstMastTypeService.GetAll();

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
            ViewData[ViewDataType.ModalTitle] = "Create New Mast Type";

            var ret = new TMstMastTypeCreatedDto();

            var getLastCode = await _mstMastTypeService.GetLastCode();
            ret.Code = getLastCode.Value;
            ret.IsActive = true;

            return View(ViewPath.MasterMastTypeProcess, ret);
        }


        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TMstMastTypeCreatedDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }

            var ret = await _mstMastTypeService.Create(User.GetUserClaims(), data);

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
            ViewData[ViewDataType.ModalTitle] = "Edit Mast Type";

            var ret = new TMstMastTypeCreatedDto();

            var result = await _mstMastTypeService.GetById(Id);

            ret.Code = result.Value.Code;
            ret.Name = result.Value.Name;
            ret.IsActive = result.Value.IsActive;

            return View(ViewPath.MasterMastTypeProcess, ret);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TMstMastTypeCreatedDto data)
        {


            var ret = await _mstMastTypeService.Update(User.GetUserClaims(), data);


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
            var ret = await _mstMastTypeService.Delete(User.GetUserClaims(), id);

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
