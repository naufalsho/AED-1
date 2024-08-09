using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.AccessMenu;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("access/menu")]
    public class AccessMenuController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IAccessMenuService _amSvc;

        public AccessMenuController(
            ICommonService commonSvc,
            IAccessMenuService amSvc)
        {
            _commonSvc = commonSvc;
            _amSvc = amSvc;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Group Menu
        [HttpGet("group/getlist")]
        public async Task<IActionResult> GetListGroup()
        {
            var ret = await _amSvc.GroupGetList();

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

        [HttpGet("group")]
        public IActionResult CreateGroup()
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Create Menu Group";

            var ret = new MenuGroupDto()
            {
                IsActive = true
            };

            return View(ViewPath.AccManagementMenuGroup, ret);
        }

        [HttpPost("group/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroup(MenuGroupDto data)
        {
            var ret = await _amSvc.GroupCreate(User.GetUserClaims(), data);

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

        [HttpGet("group/{id}")]
        public async Task<IActionResult> EditGroup(int id)
        {
            var ret = await _amSvc.GroupGetById(id);

            if (ret.IsSuccess)
            {
                ViewData[ViewDataType.ModalType] = ModalType.Edit;
                ViewData[ViewDataType.ModalTitle] = "Edit Menu Group";

                return View(ViewPath.AccManagementMenuGroup, ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpPost("group/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(MenuGroupDto data)
        {
            var ret = await _amSvc.GroupUpdate(User.GetUserClaims(), data);

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

        [HttpDelete("group/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var ret = await _amSvc.GroupDelete(User.GetUserClaims(), id);

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
        #endregion

        #region Menu
        [HttpGet("menu/getlist")]
        public async Task<IActionResult> GetListMenu()
        {
            var ret = await _amSvc.MenuGetList();

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

        [HttpGet("menu")]
        public async Task<IActionResult> CreateMenu()
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Create Menu";

            var ret = new MenuDto()
            {
                MenuGroups = await _commonSvc.SLGetGroupMenu(),
                IsActive = true
            };

            return View(ViewPath.AccManagementMenu, ret);
        }

        [HttpPost("menu/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenu(MenuDto data)
        {
            var ret = await _amSvc.MenuCreate(User.GetUserClaims(), data);

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

        [HttpGet("menu/{id}")]
        public async Task<IActionResult> EditMenu(int id)
        {
            var ret = await _amSvc.MenuGetById(id);

            if (ret.IsSuccess)
            {
                ViewData[ViewDataType.ModalType] = ModalType.Edit;
                ViewData[ViewDataType.ModalTitle] = "Edit Menu";

                ret.Value.MenuGroups = await _commonSvc.SLGetGroupMenu();

                return View(ViewPath.AccManagementMenu, ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [HttpPost("menu/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMenu(MenuUpdateDto data)
        {
            var ret = await _amSvc.MenuUpdate(User.GetUserClaims(), data);

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

        [HttpDelete("menu/{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var ret = await _amSvc.MenuDelete(User.GetUserClaims(), id);

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
        #endregion
    }
}
