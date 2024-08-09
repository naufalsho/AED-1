using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Domain.Master.MasterModel;
using Domain.Master.MasterSpecItem;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.EntityFrameworkCore;




namespace Domain.Master.MasterSpecItem
{
    public class TMstSpecItemsService : ITMstSpecItemsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TMstSpecItemsService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<Result<TMstSpecItemsDto>> Create(UserClaimModel user, TMstSpecItemsCreatedDto data)
        {
            try
            {
                var checkCode = await _uow.MstSpecItem.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                if (checkCode != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

                var param = _mapper.Map<TMstSpecItem>(data);
                param.CreatedBy = user.NameIdentifier;
                param.CreatedDate = DateTime.Now;

                await _uow.MstSpecItem.Add(param);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstSpecItemsDto>(param);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstSpecItemsDto>> Delete(UserClaimModel user, string id)
        {
            try
            {
                var repoResult = await _uow.MstSpecItem.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstSpecItem.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstSpecItemsDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }


        public async Task<Result<IEnumerable<TMstSpecItemsDto>>> GetAll()
        {
            try
            {
                var repoResult = await _uow.MstSpecItem.Set().Where(m => m.IsActive && !m.IsDelete)
                    .Include(m => m.Category)
                    .ToListAsync();

                var result = _mapper.Map<IEnumerable<TMstSpecItemsDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<TMstSpecItemsDto>> GetById(string id)
        {
            try
            {
                var repoResult = await _uow.MstSpecItem.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<TMstSpecItemsDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public Task<Result<TMstSpecItemsDto>> GetByParam(TMstSpecItemsDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetLastCode()
        {
            try
            {
                var lastBrandCode = await _uow.MstSpecItem.Set().OrderByDescending(b => b.CreatedDate).FirstOrDefaultAsync();

                if (lastBrandCode == null)
                    return "S00001";

                string prefix = lastBrandCode.Code.Substring(0, 1); // Misalnya, 'B' dari 'B000'
                int lastNumber = int.Parse(lastBrandCode.Code.Substring(1)); // Misalnya, 000 dari 'B000'

                // Menambahkan 1 pada bagian nomor
                lastNumber++;

                // Menggabungkan kembali bagian huruf dan bagian nomor yang telah diubah
                string nextCode = $"{prefix}{lastNumber:D5}"; // Format nomor agar selalu tiga digit

                Console.WriteLine(nextCode);


                return Result.Ok(nextCode);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }

        }

        public Task<Result<TMstSpecItemsDto>> GetListByParam(TMstSpecItemsDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TMstSpecItemsDto>> Update(UserClaimModel user, TMstSpecItemsCreatedDto data)
        {
            try
            {
                var repoResult = await _uow.MstSpecItem.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                string CreatedBy = repoResult.CreatedBy;

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.CreatedBy = CreatedBy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.MstSpecItem.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstSpecItemsDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

    }

}
