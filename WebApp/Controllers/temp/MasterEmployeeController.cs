using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.Common;
using Domain.MasterEmployee;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("master")]
    public class MasterEmployeeController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMasterEmployeeService _empSvc;

        public MasterEmployeeController(
            ICommonService commonSvc,
            IMasterEmployeeService empSvc)
        {
            _commonSvc = commonSvc;
            _empSvc = empSvc;
        }

        [HttpGet("employee")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _empSvc.GetList();

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

        [HttpGet("employee/create")]
        public async Task<IActionResult> Create()
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Create Employee";

            var ret = new MstEmployeeDto()
            {
                Statuses = _commonSvc.SLGetEmployeeStatus()
            };

            return View(ViewPath.MasterEmployee, ret);
        }

        [HttpPost("employee/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MstEmployeeDto data)
        {
            var ret = await _empSvc.Create(User.GetUserClaims(), data);

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

        [HttpGet("employee/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var ret = await _empSvc.GetById(id);

            if (ret.IsSuccess)
            {
                ViewData[ViewDataType.ModalType] = ModalType.Edit;
                ViewData[ViewDataType.ModalTitle] = "Edit Employee";

                ret.Value.Statuses = _commonSvc.SLGetEmployeeStatus();

                return View(ViewPath.MasterEmployee, ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpPost("employee/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MstEmployeeUpdateDto data, int id)
        {
            var ret = await _empSvc.Update(User.GetUserClaims(), data);

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

        [HttpDelete("employee/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ret = await _empSvc.Delete(User.GetUserClaims(), id);

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
