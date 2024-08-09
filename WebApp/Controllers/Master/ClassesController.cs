using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.Master;
using Domain.Master.Class;
using Domain.Master.ClassValue;
using Domain.Master.MasterCategory;
using Domain.MasterComparisonType;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ZXing.QrCode.Internal;

namespace WebApp.Controllers.Master
{
    [Route("Classes")]
    public class ClassesController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMstClassService _mstClassService;
        private readonly IMstClassValueService _mstClassValueService;

        public ClassesController(ICommonService commonSvc, IMstClassService mstClassService, IMstClassValueService mstClassValueService)
        {
            _commonSvc = commonSvc;
            _mstClassService = mstClassService;
            _mstClassValueService = mstClassValueService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(ViewPath.MasterClass);
        }

        [HttpGet("GetList/{type}")]
        public async Task<IActionResult> GetList(string type)
        {

            if(type == "Class")
            {
                var ret = await _mstClassService.GetAll();

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

            if (type == "ClassValue")
            {
                var ret = await _mstClassValueService.GetAll();

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

            return StatusCode(400, ":Data Not Found");

        }

        [HttpGet("Create/{mdType}")]
        public async Task<IActionResult> CreateAsync(int? id, string mdType)
        {
            var getLastCode = mdType == "class"
                                ? await _mstClassService.GetLastCode()
                                : await _mstClassValueService.GetLastCode();


            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = (mdType == "class" ? "Create New Class" : "Create New Class Value");

            var ret = new TMstClassCreatedDto();
            ret.Code = getLastCode.Value;
            ret.IsActive = true;
            
            return View(ViewPath.MasterClassProcess, ret);
        }


        [HttpPost("Create/{type}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string type, TMstClassCreatedDto data)
        {

            if (!ModelState.IsValid)
            {
                string errors = GetModelStateErrors(data);
                return StatusCode(400, errors);
            }


            if(type == "class")
            {
                var ret = await _mstClassService.Create(User.GetUserClaims(), data);

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
            else
            {
                var ret = await _mstClassValueService.Create(User.GetUserClaims(), data);

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


        [HttpGet("Edit/{type}")]
        public async Task<IActionResult> Edit(string type, string Id)
        {


            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = (type == "class" ? "Edit Class" : "Edit Class Value");



            var ret = new TMstClassCreatedDto();

            if (type == "class")
            {
                var result = await _mstClassService.GetById(Id);
                ret.Code = result.Value.Code;
                ret.Name = result.Value.Name;
                ret.IsActive = result.Value.IsActive;
            }
            else
            {
                var result = await _mstClassValueService.GetById(Id);
                ret.Code = result.Value.Code;
                ret.Name = result.Value.Name;
                ret.IsActive = result.Value.IsActive;
            }

            return View(ViewPath.MasterClassProcess, ret);
        }



        [HttpPost("Edit/{type}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string type, TMstClassCreatedDto data)
        {


            if(type == "class")
            {
                var ret = await _mstClassService.Update(User.GetUserClaims(), data);

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
            else
            {
                var ret = await _mstClassValueService.Update(User.GetUserClaims(), data);

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


        [HttpDelete("{type}/{id}")]
        public async Task<IActionResult> Delete(string type, string id)
        {

            if(type == "class")
            {
                var ret = await _mstClassService.Delete(User.GetUserClaims(), id);

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
            else
            {
                var ret = await _mstClassValueService.Delete(User.GetUserClaims(), id);

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



        //[HttpGet("Create/{id?}")]

        //public async Task<IActionResult> CreateAsync(int? id)
        //{

        //    var result = id.HasValue ? await _yardServices.GetById(id.Value) : null;
        //    ViewData[ViewDataType.ModalType] = id.HasValue ? ModalType.Edit : ModalType.Create;
        //    ViewData[ViewDataType.ModalTitle] = id.HasValue ? "Edit Yard Area" : "Create New Yard Area";
        //    ViewData[ViewDataType.Action] = id.HasValue ? "Edit" : "Create";

        //    var ret = result?.IsSuccess == true ? result.Value : new YardAreaDto() { IsActive = true };

        //     return View(ViewPath.MasterYardAreaForm, ret);
        //}

        //[HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(YardAreaDto data)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        string errors = GetModelStateErrors(data);
        //        return StatusCode(400, errors);
        //    }


        //    var ret = await _yardServices.Create(User.GetUserClaims(), data);

        //    if (ret.IsSuccess)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //        return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //    }
        //}



        //[HttpPost("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(YardAreaDto data)
        //{


        //    var ret = await _yardServices.Update(User.GetUserClaims(), data);

        //    // if(ret.Value.IsActive != data.IsActive || data.IsActive == true)
        //    // {
        //    //     var getYardArea = await _yardServices.GetById
        //    // }

        //    if (ret.IsSuccess)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //        return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //    }
        //}

        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> DeleteGroup(int id)
        //{
        //    var ret = await _yardServices.Delete(User.GetUserClaims(), id);

        //    if (ret.IsSuccess)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //        return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //    }
        //}


        //[HttpGet("DownloadQR/{filename}")]
        //public async Task<IActionResult> DownloadQR(string fileName)

        //{
        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        return StatusCode(400, "File name is not provided.");
        //    }

        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/qrcode", fileName);
        //    if (!System.IO.File.Exists(path))
        //    {
        //        return StatusCode(400, "QRCode Not Found");
        //    }

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(path, FileMode.Open))
        //    {
        //        stream.CopyTo(memory);
        //    }
        //    memory.Position = 0;

        //    return File(memory, "image/jpeg", fileName);
        //}


        //[HttpGet("View")]
        //public async Task<IActionResult> View()
        //{


        //    var ret = await _yardServices.GetAll();



        //    // if(ret.Value.IsActive != data.IsActive || data.IsActive == true)
        //    // {
        //    //     var getYardArea = await _yardServices.GetById
        //    // }

        //    if (ret.IsSuccess)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //        return StatusCode(int.Parse(resp.StatusCode), resp.Message);
        //    }
        //}
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
