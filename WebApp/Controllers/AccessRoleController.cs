using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.AccessRole;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("access")]
    public class AccessRoleController : BasePageController
    {
        private readonly IAccessRoleService _arSvc;

        public AccessRoleController(
            IAccessRoleService arSvc)
        {
            _arSvc = arSvc;
        }

        [HttpGet("role")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _arSvc.GetList();

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

        [HttpGet("role/create")]
        public async Task<IActionResult> Create()
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Create Role";

            var ret = new RoleDto()
            {
                RoleMenus = await _arSvc.GetRoleMenu(),
                IsActive = true
            };

            return View(ViewPath.AccManagementRole, ret);
        }

        [HttpPost("role/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDto data)
        {
            var ret = await _arSvc.Create(User.GetUserClaims(), data);

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

        [HttpGet("role/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var ret = await _arSvc.GetById(id);

            if (ret.IsSuccess)
            {
                ViewData[ViewDataType.ModalType] = ModalType.Edit;
                ViewData[ViewDataType.ModalTitle] = "Edit Role";

                return View(ViewPath.AccManagementRole, ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpPost("role/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleUpdateDto data)
        {
            var ret = await _arSvc.Update(User.GetUserClaims(), data);

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

        [HttpDelete("role/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ret = await _arSvc.Delete(User.GetUserClaims(), id);

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
