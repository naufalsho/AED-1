using Core;
using Core.Extensions;
using Core.Helpers;
using Data;
using Domain.AccessUser;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("access")]
    public class AccessUserController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IAccessUserService _auSvc;

        public AccessUserController(
            ICommonService commonSvc,
            IAccessUserService auSvc)
        {
            _commonSvc = commonSvc;
            _auSvc = auSvc;
        }

        [HttpGet("user")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _auSvc.GetList();

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

        [HttpGet("user/create")]
        public async Task<IActionResult> Create()
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Create User";

            var ret = new UserDto()
            {
                Roles = await _commonSvc.SLGetRole(),
                IsActive = true
            };

            return View(ViewPath.AccManagementUser, ret);
        }

        [HttpPost("user/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto data)
        {
            var ret = await _auSvc.Create(User.GetUserClaims(), data);

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

        [HttpGet("user/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var ret = await _auSvc.GetById(id);

            if (ret.IsSuccess)
            {
                ViewData[ViewDataType.ModalType] = ModalType.Edit;
                ViewData[ViewDataType.ModalTitle] = "Edit User";

                ret.Value.Roles = await _commonSvc.SLGetRole();

                return View(ViewPath.AccManagementUser, ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpPost("user/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDto data)
        {
            var ret = await _auSvc.Update(User.GetUserClaims(), data);

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

        [HttpDelete("user/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ret = await _auSvc.Delete(User.GetUserClaims(), id);

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
