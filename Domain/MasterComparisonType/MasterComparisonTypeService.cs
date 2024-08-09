using System.ComponentModel.DataAnnotations;
using System.Drawing;
using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Domain.MasterComparisonType
{
    public class MasterComparisonTypeService : IMasterComparisonService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MasterComparisonTypeService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<MasterComparisonTypeDto>>> GetList()
        {
            try
            {
                var repoResult = await _uow.MstComparisonType.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<MasterComparisonTypeDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Title).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //public async Task<Result<YardAreaDto>> Create(UserClaimModel user, YardAreaDto data)
        //{
        //    try
        //    {
        //        var checkCode = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.CodeArea == data.CodeArea);

        //        if (checkCode != null)
        //            return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

        //        var param = _mapper.Map<TtMstYardArea>(data);
        //        param.CodeArea = data.CodeArea;
        //        param.Name = data.Name;
        //        param.CurrentOccupancy =  data.CurrentOccupancy;
        //        param.Capacity = data.Capacity;
        //        param.UpdatedDate = null;
        //        param.CreatedBy = user.NameIdentifier;
        //        param.CreatedDate = DateTime.Now;
        //        param.YardQRCode = GenerateQRCode(data);

        //        await _uow.MstYardArea.Add(param);
        //        await _uow.CompleteAsync();

        //        return Result.Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }

        //}

        //public async Task<Result<YardAreaDto>> Delete(UserClaimModel user, int id)
        //{
        //    try
        //    {
        //        var repoResult = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.Id == id);

        //        if (repoResult == null)
        //            return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

        //        repoResult.IsActive = false;
        //        repoResult.IsDelete = true;
        //        repoResult.DeletedBy = user.NameIdentifier;
        //        repoResult.DeletedDate = DateTime.Now;

        //        _uow.MstYardArea.Update(repoResult);
        //        await _uow.CompleteAsync();

        //        var result = _mapper.Map<YardAreaDto>(repoResult);

        //        return Result.Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}

        //public async Task<Result<IEnumerable<YardAreaDto>>> GetAll()
        //{
        //    try
        //    {

        //        var repoResult = await _uow.MstYardArea.ExecuteStoredProcedure("sp_GetMstYardArea");

        //        // var repoResult = await _uow.MstYardArea.Set().ToListAsync();

        //        var result = _mapper.Map<IEnumerable<YardAreaDto>>(repoResult);

        //        return Result.Ok(result.OrderBy(m => m.Name).AsEnumerable());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}

        //public async Task<Result<YardAreaDto>> GetById(int id)
        //{
        //    try
        //    {
        //        var repoResult = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.Id == id);

        //        if (repoResult == null)
        //            return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

        //        var result = _mapper.Map<YardAreaDto>(repoResult);

        //        return Result.Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }        
        //}

        //public async Task<Result<YardAreaDto>> GetByParam(YardAreaFilterDto param)
        //{
        //    try
        //    {


        //    dynamic parameters = new
        //    {
        //        IsDelete = param.IsDelete,
        //        Id = param.Id
        //    };

        //    var result = await _uow.MstYardArea.ExecuteStoredProcedure("YourStoredProcedureName", parameters).FirstOrDefaultAsync();

        //    return Result.Ok();

        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }        
        //}


        //public async Task<Result<IEnumerable<YardAreaDto>>> GetList()
        //{
        //    try
        //    {
        //        var repoResult = await _uow.MstYardArea.Set().ToListAsync();

        //        var result = _mapper.Map<IEnumerable<YardAreaDto>>(repoResult);

        //        return Result.Ok(result.OrderBy(m => m.Name).AsEnumerable());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}

        //public async Task<Result<YardAreaDto>> Update(UserClaimModel user, YardAreaDto data)
        //{
        //    try
        //    {
        //        // Verifikasi model
        //        var validationContext = new ValidationContext(data, serviceProvider: null, items: null);
        //        var validationResults = new List<ValidationResult>();
        //        if (!Validator.TryValidateObject(data, validationContext, validationResults, validateAllProperties: true))
        //        {
        //            var errorMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
        //            return Result.Fail(ResponseStatusCode.BadRequest + ":" + errorMessage);
        //        }

        //        // Gunakan ekspresi lambda yang lebih efisien
        //        var repoResult = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

        //        if (repoResult == null)
        //            return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

        //        // Memperbarui properti yang diperlukan saja
        //        repoResult.Name = data.Name;
        //        repoResult.Capacity = data.Capacity;
        //        repoResult.CurrentOccupancy = data.CurrentOccupancy;
        //        repoResult.UpdatedBy = user.NameIdentifier;
        //        repoResult.UpdatedDate= DateTime.Now;

        //        _uow.MstYardArea.Update(repoResult);
        //        await _uow.CompleteAsync();

        //        var result = _mapper.Map<YardAreaDto>(repoResult);

        //        return Result.Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}
    }

}
