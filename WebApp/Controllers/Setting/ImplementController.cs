using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces.IRepositories.Tables;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.Comparison;
using Domain.Master;
using Domain.MasterComparisonType;
using Domain.MasterYardArea;
using Domain.Transaction.Implement;
using Domain.Transaction.SpecValues;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ZXing;
using ZXing.QrCode.Internal;

namespace WebApp.Controllers.Setting
{
    [Route("Implement")]
    public class ImplementController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IMasterComparisonService _mstComparisonService;
        private readonly ITrnImplementService _trnImplementService;

        public ImplementController(ICommonService commonSvc, IMasterComparisonService mstComparisonService, ITrnImplementService trnImplementService)
        {
            _commonSvc = commonSvc;
            _mstComparisonService = mstComparisonService;
            _trnImplementService = trnImplementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ret = new TTrnImplementDto();
            ret.SLModel = await _commonSvc.SLGetModel(MasterModelType.Unit);
            return View(ViewPath.ImplementConf, ret);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(TTrnImplementFilterDto param)
        {
            var ret = await _trnImplementService.GetListByParam(param);



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
		public async Task<IActionResult> CreateAsync()
		{
			ViewData[ViewDataType.ModalType] = ModalType.Create;
			ViewData[ViewDataType.ModalTitle] = "Create New Matriks Implement";

			var ret = new TTrnImplementDto
			{

				SLModelAttachment = await _commonSvc.SLGetModel(MasterModelType.Attachment),
				SLModelProduct = await _commonSvc.SLGetModel(MasterModelType.Unit),
				SLClassValue = await _commonSvc.SLGetClassValue(),
				IsActive = true

			};
			return View(ViewPath.ImplementProcess, ret);
		}


		[HttpPost("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TTrnImplementDto data)
		{

			if (!ModelState.IsValid)
			{
				string errors = GetModelStateErrors(data);
				return StatusCode(400, errors);
			}

			var ret = await _trnImplementService.Create(User.GetUserClaims(), data);

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
        public async Task<IActionResult> Edit(int Id)
        {
            ViewData[ViewDataType.ModalType] = ModalType.Create;
            ViewData[ViewDataType.ModalTitle] = "Edit Matriks Implement";

            var ret = new TTrnImplementDto();

            var result = await _trnImplementService.GetById(Id);

            ret.ModelCode = result.Value.ModelCode;
            ret.ModelCodeAttach = result.Value.ModelCodeAttach;
            ret.ClassValueCode = result.Value.ClassValueCode;
            ret.IsActive = result.Value.IsActive;
            ret.SLModelAttachment = await _commonSvc.SLGetModel(MasterModelType.Attachment);
            ret.SLModelProduct = await _commonSvc.SLGetModel(MasterModelType.Unit);
            ret.SLClassValue = await _commonSvc.SLGetClassValue();

            return View(ViewPath.ImplementProcess, ret);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TTrnImplementDto data)
        {


            var ret = await _trnImplementService.Update(User.GetUserClaims(), data);


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
        public async Task<IActionResult> Delete(int id)
        {
            var ret = await _trnImplementService.Delete(User.GetUserClaims(), id);

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
