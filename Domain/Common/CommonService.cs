﻿using Core;
using Core.Interfaces;
using Core.Models.Entities.Tables.Master;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common
{
    public class CommonService : ICommonService
    {
        private readonly IUnitOfWork _uow;

        public CommonService(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region Master
        public async Task<IEnumerable<SelectListItem>> SLGetBrand()
        {
            var repoResult = await _uow.MstBrand.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Name });

            return result;
        }
        #endregion

        public async Task<IEnumerable<SelectListItem>> SLGetGroupMenu()
        {
            var repoResult = await _uow.AccMenuGroup.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = m.Name });

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> SLGetRole()
        {
            var repoResult = await _uow.AccRole.Set().Where(m => m.IsActive && !m.IsDelete).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = m.Name });

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> SLGetMasterDataGeneral(string mdType)
        {
            var repoResult = await _uow.MstGeneral.Set().Where(m => m.Type == mdType && m.IsActive && !m.IsDelete).OrderBy(m => m.Code).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = $"{m.Code} - {m.Name}" });

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> SLGetMasterDataByParentId(string mdType, int? parentId)
        {
            IEnumerable<SelectListItem> result = null;

            if (parentId.HasValue)
            {
                var repoResult = await _uow.MstGeneral.Set().Where(m => m.ParentId == parentId && m.Type == mdType && m.IsActive && !m.IsDelete).OrderBy(m => m.Code).ToListAsync();

                result = repoResult.Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = $"{m.Code} - {m.Name}" });
            }

            return result;
        }


        public IEnumerable<SelectListItem> SLGetEmployeeStatus()
        {
            var result = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "PERMANENT", Text = "PERMANENT" },
                new SelectListItem() { Value = "CONTRACT", Text = "CONTRACT" },
                new SelectListItem() { Value = "EXPATRIATE", Text = "EXPATRIATE" },
                new SelectListItem() { Value = "TRAINEE", Text = "TRAINEE" },
                new SelectListItem() { Value = "OUTSOURCE", Text = "OUTSOURCE" },
                new SelectListItem() { Value = "RESIGN", Text = "RESIGN" },
                new SelectListItem() { Value = "PENSION", Text = "PENSION" }
            };

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> SLGetClassValue()
        {
            var repoResult = await _uow.MstClassValue.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Name });

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> SLGetClass()
        {
            var repoResult = await _uow.MstClass.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Name });

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> SLGetCategory()
        {
            var repoResult = await _uow.MstCategory.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Description });

            return result;
        }

		public async Task<IEnumerable<SelectListItem>> SLGetModel(string type)
		{
			IQueryable<TMstModel> query = _uow.MstModel.Set().Where(m => m.IsActive && !m.IsDelete);

			if (!string.IsNullOrEmpty(type))
			{
				query = query.Where(m => m.Type == type);
			}

			var repoResult = await query.ToListAsync();
			var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Model });

			return result;
		}

	}
}
