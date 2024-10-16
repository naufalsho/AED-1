using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models.Entities.Tables.Master;
using Domain.Master;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
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

        public async Task<IEnumerable<SelectListItem>> SLGetClassByBrand(string brand, string distributor)
        {
            // Ensure the brand parameter is not null or empty
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new ArgumentException("Brand parameter cannot be null or empty", nameof(brand));
            }

            try
            {
                // Define the parameters for the stored procedure
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("BrandName", brand),
                    new SqlParameter("Distributor", distributor),
                };
                // Execute the stored procedure
                var repoResult = await _uow.MstClass.ExecuteStoredProcedure("sp_SLGetClassByBrand", parameters);

                // Map the results to SelectListItem
                var result = repoResult.Select(m => new SelectListItem
                {
                    Value = m.Code, // Ensure Code is a property in your model
                    Text = m.Name // Ensure Name is a property in your model
                }).ToList(); // Convert to list

                return result;
            }
            catch (Exception ex)
            {
                // Handle exceptions (log if necessary)
                throw new Exception("An error occurred while retrieving classes by brand: " + ex.Message);
            }
        }

        public async Task<IEnumerable<SelectListItem>> SLGetCategory()
        {
            var repoResult = await _uow.MstCategory.Set().Where(m => m.IsActive && m.Tag != "TN").ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Description });

            ////var repoResult = await _uow.MstCategory.Set().Where(m => m.IsActive).ToListAsync();
            //var repoResult = await _uow.MstCategory.Set()
            //            .Where(m => m.IsActive)
            //            .GroupBy(m => m.Description)
            //            .Select(g => g.OrderBy(m => m.Code).FirstOrDefault())
            //            .ToListAsync();


            //var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Description }).OrderBy(m => m.Value);

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
        
        public async Task<IEnumerable<SelectListItem>> SLGetModelProductTN(string type)
        {
            IQueryable<TMstModel> query = _uow.MstModel.Set().Where(m => m.IsActive && m.Distributor == Distributor.ProductTN && !m.IsDelete);

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(m => m.Type == type && m.IsActive && !m.IsDelete);
            }

            var repoResult = await query.ToListAsync();
            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Model });

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> SLGetCap()
        {
            var repoResult = await _uow.MstCap.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Name });

            return result;

        }

        public async Task<IEnumerable<SelectListItem>> SLGetLiftingHeight()
        {
            var repoResult = await _uow.MstLiftingHeight.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Name });

            return result;

        }

        public async Task<IEnumerable<SelectListItem>> SLGetMastType()
        {
            var repoResult = await _uow.MstMastType.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Name });

            return result;

        }

        public async Task<IEnumerable<SelectListItem>> SLGetTire()
        {
            var repoResult = await _uow.MstTire.Set().Where(m => m.IsActive).ToListAsync();

            var result = repoResult.Select(m => new SelectListItem() { Value = m.Code, Text = m.Name });

            return result;

        }
    }
}
