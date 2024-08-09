using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ZXing.QrCode.Internal;

namespace WebApp.Controllers
{
    [Route("[Controller]")]
    public class YardAreaController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IYardAreaService _yardServices;

        public YardAreaController(ICommonService commonSvc, IYardAreaService yardAreaServices)
        {
            _commonSvc = commonSvc;
            _yardServices = yardAreaServices;
            UserClaimModel users = User.GetUserClaims();
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            // var ret = await _yardServices.GetAll();

            return View(ViewPath.MasterYardArea);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _yardServices.GetList();

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
        

        [HttpGet("Create/{id?}")]

        public async Task<IActionResult> CreateAsync(int? id)
        {

            var result = id.HasValue ? await _yardServices.GetById(id.Value) : null;
            ViewData[ViewDataType.ModalType] = id.HasValue ? ModalType.Edit : ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = id.HasValue ? "Edit Yard Area" : "Create New Yard Area";
            ViewData[ViewDataType.Action] = id.HasValue ? "Edit" : "Create";

            var ret = result?.IsSuccess == true ? result.Value : new YardAreaDto() { IsActive = true };

             return View(ViewPath.MasterYardAreaForm, ret);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(YardAreaDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }
               

            var ret = await _yardServices.Create(User.GetUserClaims(), data);

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
        


        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(YardAreaDto data)
        {


            var ret = await _yardServices.Update(User.GetUserClaims(), data);

            // if(ret.Value.IsActive != data.IsActive || data.IsActive == true)
            // {
            //     var getYardArea = await _yardServices.GetById
            // }

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

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var ret = await _yardServices.Delete(User.GetUserClaims(), id);

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


        [HttpGet("DownloadQR/{filename}")]
        public async Task<IActionResult> DownloadQR(string fileName)

        {
            if (string.IsNullOrEmpty(fileName))
            {
                return StatusCode(400, "File name is not provided.");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/qrcode", fileName);
            if (!System.IO.File.Exists(path))
            {
                return StatusCode(400, "QRCode Not Found");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "image/jpeg", fileName);
        }


        [HttpGet("View")]
        public async Task<IActionResult> View()
        {


            var ret = await _yardServices.GetAll();



            // if(ret.Value.IsActive != data.IsActive || data.IsActive == true)
            // {
            //     var getYardArea = await _yardServices.GetById
            // }

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
        // #endregion

        // #region Menu
        // [HttpGet("menu/getlist")]
        // public async Task<IActionResult> GetListMenu()
        // {
        //     var ret = await _amSvc.MenuGetList();

        //     if (ret.IsSuccess)
        //     {
        //         return Ok(ret.Value);
        //     }
        //     else
        //     {
        //         var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //         return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //     }
        // }

        // [HttpGet("menu")]
        // public async Task<IActionResult> CreateMenu()
        // {
        //     ViewData[ViewDataType.ModalType] = ModalType.Create;
        //     ViewData[ViewDataType.ModalTitle] = "Create Menu";

        //     var ret = new MenuDto()
        //     {
        //         MenuGroups = await _commonSvc.SLGetGroupMenu(),
        //         IsActive = true
        //     };

        //     return View(ViewPath.AccManagementMenu, ret);
        // }

        // [HttpPost("menu/create")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> CreateMenu(MenuDto data)
        // {
        //     var ret = await _amSvc.MenuCreate(User.GetUserClaims(), data);

        //     if (ret.IsSuccess)
        //     {
        //         return Ok();
        //     }
        //     else
        //     {
        //         var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //         return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //     }
        // }

        // [HttpGet("menu/{id}")]
        // public async Task<IActionResult> EditMenu(int id)
        // {
        //     var ret = await _amSvc.MenuGetById(id);

        //     if (ret.IsSuccess)
        //     {
        //         ViewData[ViewDataType.ModalType] = ModalType.Edit;
        //         ViewData[ViewDataType.ModalTitle] = "Edit Menu";

        //         ret.Value.MenuGroups = await _commonSvc.SLGetGroupMenu();

        //         return View(ViewPath.AccManagementMenu, ret.Value);
        //     }
        //     else
        //     {
        //         var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //         return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //     }
        // }

        // [HttpPost("menu/edit")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> EditMenu(MenuUpdateDto data)
        // {
        //     var ret = await _amSvc.MenuUpdate(User.GetUserClaims(), data);

        //     if (ret.IsSuccess)
        //     {
        //         return Ok();
        //     }
        //     else
        //     {
        //         var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //         return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //     }
        // }

        // [HttpDelete("menu/{id}")]
        // public async Task<IActionResult> DeleteMenu(int id)
        // {
        //     var ret = await _amSvc.MenuDelete(User.GetUserClaims(), id);

        //     if (ret.IsSuccess)
        //     {
        //         return Ok();
        //     }
        //     else
        //     {
        //         var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //         return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //     }
        // }
    }
}
