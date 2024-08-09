using AutoMapper;
using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models.Entities.Views;
using Domain.Transaction.SpecValues;
using Domain.UnitSpec;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Report
{
    public class UnitSpecService : IUnitSpecService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UnitSpecService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UnitSpecDto>>> GetAllByParam(UnitSpecFilterDto param)
        {
            try
            {

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("CategoryCode", param.CategoryCode),
                    new SqlParameter("ModelCode", param.ModelCode)
                };
                var repoResult = await _uow.VwUnitSpec.ExecuteStoredProcedure("sp_GetUnitSpec", parameters);


                var result = _mapper.Map<IEnumerable<UnitSpecDto>>(repoResult);


                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //public async Task<Result<IEnumerable<UnitSpecDto>>> GetData()
        //{
        //    try
        //    {
        //        var repoResult = await _uow.VwMstAsset.Set()
        //            .Where(m => m.AssetStatus == AssetStatus.AVAILABLE)
        //            .ToListAsync();

        //        var result = _mapper.Map<IEnumerable<ReportDeviceDto>>(repoResult);

        //        return Result.Ok(result.OrderByDescending(m => m.DeviceTypeName).AsEnumerable());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}

        //public async Task<Result<IEnumerable<ReportTransDeviceDto>>> RptDeviceOnUser(ReportRequestDto reqData)
        //{
        //    try
        //    {
        //        Expression<Func<VwTransDeployDevice, bool>> whereCond = c => !c.UndeployDate.HasValue;
        //        if (reqData.CompanyId != 0)
        //        {
        //            whereCond = whereCond.And(m => m.CompanyId == reqData.CompanyId);
        //        }

        //        if (reqData.BranchId != 0)
        //        {
        //            whereCond = whereCond.And(m => m.BranchId == reqData.BranchId);
        //        }

        //        var repoResult = await _uow.VwTransDeployDevice.Set().Where(whereCond).ToListAsync();

        //        var result = _mapper.Map<IEnumerable<ReportTransDeviceDto>>(repoResult);

        //        return Result.Ok(result.OrderByDescending(m => m.BASTCompDate).AsEnumerable());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}

        //public async Task<Result<IEnumerable<ReportTransDeviceDto>>> RptEndOfPeriod(ReportRequestDto reqData)
        //{
        //    try
        //    {
        //        Expression<Func<VwMstAsset, bool>> whereCond = c => c.IsEndPeriod;
        //        if (reqData.CompanyId != 0)
        //        {
        //            whereCond = whereCond.And(m => m.CompanyId == reqData.CompanyId);
        //        }

        //        if (reqData.BranchId != 0)
        //        {
        //            whereCond = whereCond.And(m => m.BranchId == reqData.BranchId);
        //        }

        //        if (reqData.EndPeriod.HasValue)
        //        {
        //            whereCond = whereCond.And(m => m.PeriodTo.Value.Date.Month == reqData.EndPeriod.Value.Date.Month && m.PeriodTo.Value.Date.Year == reqData.EndPeriod.Value.Date.Year);
        //        }

        //        var repoResult = await _uow.VwMstAsset.Set().Where(whereCond).ToListAsync();

        //        var result = _mapper.Map<IEnumerable<ReportTransDeviceDto>>(repoResult);

        //        return Result.Ok(result.OrderByDescending(m => m.PeriodTo).AsEnumerable());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}

        //public async Task<Result<IEnumerable<ReportDeviceDto>>> RptHistorical(ReportRequestDto reqData)
        //{
        //    try
        //    {
        //        Expression<Func<VwTransDeployDevice, bool>> whereCond = c => true;
        //        if (reqData.CompanyId != 0)
        //        {
        //            whereCond = whereCond.And(m => m.CompanyId == reqData.CompanyId);
        //        }

        //        if (reqData.BranchId != 0)
        //        {
        //            whereCond = whereCond.And(m => m.BranchId == reqData.BranchId);
        //        }

        //        var repoResult = await _uow.VwTransDeployDevice.Set().Where(whereCond).ToListAsync();

        //        var result = _mapper.Map<IEnumerable<ReportDeviceDto>>(repoResult);

        //        result = result.DistinctBy(m => new { m.AssetId, m.DeviceCatName, m.DeviceTypeName, m.ProductBrandName, m.ProductTypeName, m.SerialNumber, m.AssetStatus });

        //        return Result.Ok(result.OrderByDescending(m => m.DeviceTypeName).AsEnumerable());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}

        //public async Task<Result<IEnumerable<ReportDeviceHistoryDto>>> RptHistoricalDetail(int assetId)
        //{
        //    try
        //    {
        //        var repoResult = await _uow.VwTransDeployDevice.Set()
        //            .Where(m => m.AssetId == assetId)
        //            .ToListAsync();

        //        var result = _mapper.Map<IEnumerable<ReportDeviceHistoryDto>>(repoResult);

        //        return Result.Ok(result.OrderByDescending(m => m.BASTCompDate).AsEnumerable());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}

    }
}
