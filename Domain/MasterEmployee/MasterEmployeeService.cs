using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Domain.MasterEmployee
{
    public class MasterEmployeeService : IMasterEmployeeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MasterEmployeeService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<MstEmployeeDto>>> GetList()
        {
            try
            {
                var repoResult = await _uow.VwMstEmployee.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<MstEmployeeDto>>(repoResult);

                return Result.Ok(result.OrderByDescending(m => m.NRP).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MstEmployeeDto>> GetById(int id)
        {
            try
            {
                var repoResult = await _uow.MstEmployee.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<MstEmployeeDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MstEmployeeDto>> GetByNRP(string nrp)
        {
            try
            {
                var repoResult = await _uow.MstEmployee.Set().FirstOrDefaultAsync(m => m.NRP == nrp);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Employee not found!");

                if (!repoResult.IsActive)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Employee not active!");

                if (repoResult.Status == "RESIGN")
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Employee resigned!");

                var result = _mapper.Map<MstEmployeeDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> Create(UserClaimModel user, MstEmployeeDto data)
        {
            try
            {
                var checkNrp = await _uow.MstEmployee.Set().FirstOrDefaultAsync(m => m.NRP == data.NRP);

                if (checkNrp != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":NRP is used by another employee. Please change the NRP!");

                var dataToAdd = _mapper.Map<TMstEmployee>(data);
                dataToAdd.CreatedBy = user.NameIdentifier;
                dataToAdd.CreatedDate = DateTime.Now;

                await _uow.MstEmployee.Add(dataToAdd);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> Update(UserClaimModel user, MstEmployeeUpdateDto data)
        {
            try
            {
                var repoResult = await _uow.MstEmployee.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");


                _mapper.Map(data, repoResult);
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.MstEmployee.Update(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> Delete(UserClaimModel user, int id)
        {
            try
            {
                var repoResult = await _uow.MstEmployee.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.MstEmployee.Remove(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
