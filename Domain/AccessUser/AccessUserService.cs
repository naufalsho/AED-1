using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Domain.AccessUser
{
    public class AccessUserService : IAccessUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;

        public AccessUserService(
            IUnitOfWork uow,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _uow = uow;
            _mapper = mapper;

            _appSettings = appSettings.Value;
        }

        public async Task<Result<IEnumerable<UserDto>>> GetList()
        {
            try
            {
                var repoResult = await _uow.AccUser.Set().Include(m => m.Role).Where(m => !m.IsDelete).ToListAsync();

                var result = _mapper.Map<IEnumerable<UserDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<UserDto>> GetById(int id)
        {
            try
            {
                var repoResult = await _uow.AccUser.Set().Include(m => m.Role).FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<UserDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> Create(UserClaimModel user, UserDto data)
        {
            try
            {
                var checkOrder = await _uow.AccUser.Set().FirstOrDefaultAsync(m => m.UserId == data.UserId && !m.IsDelete);

                if (checkOrder != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":UserId not available. Please change the UserId!");

                var dataToAdd = _mapper.Map<TAccUser>(data);
                dataToAdd.Password = BCryptNet.HashPassword(_appSettings.DefaultPassword);
                dataToAdd.CreatedBy = user.NameIdentifier;
                dataToAdd.CreatedDate = DateTime.Now;

                await _uow.AccUser.Add(dataToAdd);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> Update(UserClaimModel user, UserUpdateDto data)
        {
            try
            {
                var repoResult = await _uow.AccUser.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.AccUser.Update(repoResult);
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
                var repoResult = await _uow.AccUser.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.AccUser.Update(repoResult);
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
