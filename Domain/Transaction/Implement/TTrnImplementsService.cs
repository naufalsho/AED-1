using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables.Transaction;
using FluentResults;
using Microsoft.EntityFrameworkCore;




namespace Domain.Transaction.Implement
{
    public class TTrnImplementsService : ITrnImplementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TTrnImplementsService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<TTrnImplementDto>> Create(UserClaimModel user, TTrnImplementDto data)
        {
			try
			{
				var checkCode = await _uow.TrnImplement.Set().FirstOrDefaultAsync(m => m.ModelCode == data.ModelCode && m.ModelCodeAttach == data.ModelCodeAttach && m.ClassValueCode == data.ClassValueCode && m.IsActive);

				if (checkCode != null)
					return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

				var param = _mapper.Map<TTrnImplement>(data);
				param.CreatedBy = user.NameIdentifier;
				param.CreatedDate = DateTime.Now;

				await _uow.TrnImplement.Add(param);
				await _uow.CompleteAsync();

				return Result.Ok(data);
			}
			catch (Exception ex)
			{
				return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
			}
		}

        public async Task<Result<TTrnImplementDto>> Delete(UserClaimModel user, int id)
        {
            try
            {
                var repoResult = await _uow.TrnImplement.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.TrnImplement.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TTrnImplementDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<TTrnImplementDto>> GetById(int id)
        {
            try
            {
                var repoResult = await _uow.TrnImplement.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<TTrnImplementDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }


        public async Task<Result<IEnumerable<TTrnImplementDto>>> GetListByParam(TTrnImplementFilterDto param)
        {
			try
			{
				var repoResult = await _uow.TrnImplement.Set().Where(m => m.IsActive  && !m.IsDelete)
                        .Include(m => m.ModelAttach)
                        .Include(m => m.ModelProduct)
                        .Include(c => c.ClassValues)
                        .ToListAsync();

				var result = _mapper.Map<IEnumerable<TTrnImplementDto>>(repoResult);

				return Result.Ok(result.OrderByDescending(m => m.Id).AsEnumerable());
			}
			catch (Exception ex)
			{
				return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
			}
        }

        public async Task<Result<TTrnImplementDto>> Update(UserClaimModel user, TTrnImplementDto data)
        {
            try
            {
                var repoResult = await _uow.TrnImplement.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

                string CreatedBy = repoResult.CreatedBy;
                DateTime CreatedDate = repoResult.CreatedDate;




                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.CreatedBy = CreatedBy;
                repoResult.CreatedDate = CreatedDate;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.TrnImplement.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TTrnImplementDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

    }

}
