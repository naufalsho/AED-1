using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Domain.MasterGeneral
{
    public class MasterGeneralService : IMasterGeneralService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MasterGeneralService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<MstGeneralDto>>> GetList(string mdType)
        {
            try
            {
                var repoResult = await _uow.MstGeneral.Set().Include(m => m.Parent).Where(m => m.Type == mdType && !m.IsDelete && (m.ParentId.HasValue ? !m.Parent.IsDelete : true)).ToListAsync();

                var result = _mapper.Map<IEnumerable<MstGeneralDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MstGeneralDto>> GetById(int id)
        {
            try
            {
                var repoResult = await _uow.MstGeneral.Set().Include(m => m.Parent).FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<MstGeneralDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MstGeneralDto>> Create(UserClaimModel user, string mdType, MstGeneralDto data)
        {
            try
            {
                var checkCode = await _uow.MstGeneral.Set().FirstOrDefaultAsync(m => m.Code == data.Code && !m.IsDelete);

                if (checkCode != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

                var dataToAdd = _mapper.Map<TMstGeneral>(data);
                dataToAdd.Type = mdType;
                dataToAdd.CreatedBy = user.NameIdentifier;
                dataToAdd.CreatedDate = DateTime.Now;

                await _uow.MstGeneral.Add(dataToAdd);
                await _uow.CompleteAsync();

                return Result.Ok(data);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MstGeneralDto>> Update(UserClaimModel user, MstGeneralUpdateDto data)
        {
            try
            {
                var repoResult = await _uow.MstGeneral.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.MstGeneral.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<MstGeneralDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MstGeneralDto>> Delete(UserClaimModel user, int id)
        {
            try
            {
                var repoResult = await _uow.MstGeneral.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstGeneral.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<MstGeneralDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
