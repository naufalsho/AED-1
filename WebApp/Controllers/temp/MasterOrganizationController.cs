using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.Common;
using Domain.MasterGeneral;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("master/organization")]
    public class MasterOrganizationController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMasterGeneralService _mdSvc;

        private string mdTypeChange = MasterDataType.Company;
        private string mdView = ViewPath.MDGeneralParent;
        private string mdText = "Company";

        public MasterOrganizationController(
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
                case "branch" or "branches":
                    mdTypeChange = MasterDataType.Branch;
                    mdText = "Branch";

                    if (isModal)
                    {
                        mdView = ViewPath.MDGeneral;
                    }

                    break;
                case "division" or "divisions":
                    mdTypeChange = MasterDataType.Division;
                    mdText = "Division";

                    if (isModal)
                    {
                        mdView = ViewPath.MDGeneralParent;
                    }

                    break;
                case "department" or "departments":
                    mdTypeChange = MasterDataType.Department;
                    mdText = "Department";

                    if (isModal)
                    {
                        mdView = ViewPath.MDGeneral;
                    }

                    break;
                case "jobgroup" or "jobgroups":
                    mdTypeChange = MasterDataType.JobGroup;
                    mdText = "Job Group";

                    if (isModal)
                    {
                        mdView = ViewPath.MDGeneral;
                    }

                    break;
                case "jobtitle" or "jobtitles":
                    mdTypeChange = MasterDataType.JobTitle;
                    mdText = "Job Title";

                    if (isModal)
                    {
                        mdView = ViewPath.MDGeneral;
                    }

                    break;
                default:
                    mdTypeChange = MasterDataType.Company;
                    mdText = "Company";

                    if (isModal)
                    {
                        mdView = ViewPath.MDGeneralParent;
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


            if (mdTypeChange == MasterDataType.Branch)
            {
                ret.IsNeedAbbr = true;
            }

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


                if (mdTypeChange == MasterDataType.Branch)
                {
                    ret.Value.IsNeedAbbr = true;
                }

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
