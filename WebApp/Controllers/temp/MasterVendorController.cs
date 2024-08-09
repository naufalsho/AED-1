using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.MasterGeneral;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("master")]
    public class MasterVendorController : BasePageController
    {
        private readonly IMasterGeneralService _mdSvc;

        public MasterVendorController(
            IMasterGeneralService mdSvc)
        {
            _mdSvc = mdSvc;
        }

        [HttpGet("vendor")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("vendors")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _mdSvc.GetList(MasterDataType.Vendor);

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

        [HttpGet("vendor/create")]
        public IActionResult Create()
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Create Vendor";

            var ret = new MstGeneralDto()
            {
                IsActive = true
            };

            return View(ViewPath.MDGeneralParent, ret);
        }

        [HttpPost("vendor/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MstGeneralDto data)
        {
            var ret = await _mdSvc.Create(User.GetUserClaims(), MasterDataType.Vendor, data);

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

        [HttpGet("vendor/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var ret = await _mdSvc.GetById(id);

            if (ret.IsSuccess)
            {
                ViewData[ViewDataType.ModalType] = ModalType.Edit;
                ViewData[ViewDataType.ModalTitle] = "Edit Vendor";

                return View(ViewPath.MDGeneralParent, ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpPost("vendor/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MstGeneralUpdateDto data)
        {
            var ret = await _mdSvc.Update(User.GetUserClaims(), data);

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

        [HttpDelete("vendor/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ret = await _mdSvc.Delete(User.GetUserClaims(), id);

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
