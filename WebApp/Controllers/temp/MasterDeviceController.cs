using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.Common;
using Domain.MasterGeneral;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("master/device")]
    public class MasterDeviceController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMasterGeneralService _mdSvc;

        private string mdTypeChange = MasterDataType.DeviceCat;
        private string mdView = ViewPath.MDGeneralParent;
        private string mdText = "Device Category";

        public MasterDeviceController(
            ICommonService commonSvc,
            IMasterGeneralService mdSvc)
        {
            _commonSvc = commonSvc;
            _mdSvc = mdSvc;
        }

        private void SwitchController(string type, bool isModal = false)
        {
            switch (type)
            {
                case "type" or "types":
                    mdTypeChange = MasterDataType.DeviceType;
                    mdText = "Device Type";

                    if (isModal)
                    {
                        mdView = ViewPath.MDGeneralParent;
                    }

                    break;
                default:
                    mdTypeChange = MasterDataType.DeviceCat;
                    mdText = "Device Category";

                    if (isModal)
                    {
                        mdView = ViewPath.MDGeneral;
                    }

                    break;
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{mdType}")]
        public async Task<IActionResult> GetList(string mdType)
        {
            SwitchController(mdType);

            var ret = await _mdSvc.GetList(mdTypeChange);

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

        [HttpGet("{mdType}/create")]
        public async Task<IActionResult> Create(string mdType)
        {
            SwitchController(mdType, true);

            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = $"Create {mdText}";

            var ret = new MstGeneralDto()
            {
                IsNeedColor = true,
                IsActive = true
            };


            return View(mdView, ret);
        }

        [HttpPost("{mdType}/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string mdType, MstGeneralDto data)
        {
            SwitchController(mdType);

            var ret = await _mdSvc.Create(User.GetUserClaims(), mdTypeChange, data);

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

        [HttpGet("{mdType}/{id}")]
        public async Task<IActionResult> Edit(string mdType, int id)
        {
            SwitchController(mdType, true);

            var ret = await _mdSvc.GetById(id);

            if (ret.IsSuccess)
            {
                ViewData[ViewDataType.ModalType] = ModalType.Edit;
                ViewData[ViewDataType.ModalTitle] = $"Edit {mdText}";

                ret.Value.IsNeedColor = true;


                return View(mdView, ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpPost("{mdType}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string mdType, MstGeneralUpdateDto data)
        {
            SwitchController(mdType);

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

        [HttpDelete("{mdType}/{id}")]
        public async Task<IActionResult> Delete(string mdType, int id)
        {
            SwitchController(mdType);

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
