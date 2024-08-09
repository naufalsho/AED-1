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
using Domain.Master.Class;
using Domain.Master.MasterCategory;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.EntityFrameworkCore;




namespace Domain.Master.ClassValue
{
    public class TMstClassValueService : IMstClassValueService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TMstClassValueService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<TMstClassValueDto>> Create(UserClaimModel user, TMstClassCreatedDto data)
        {
            try
            {

                var checkCode = await _uow.MstClassValue.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                if (checkCode != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

                var param = _mapper.Map<TMstClassValue>(data);
                param.Name = data.Name;
                param.UpdatedDate = null;
                param.CreatedBy = user.NameIdentifier;
                param.CreatedDate = DateTime.Now;

                await _uow.MstClassValue.Add(param);
                await _uow.CompleteAsync();
                
                var result = _mapper.Map<TMstClassValueDto>(param);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstClassValueDto>> Delete(UserClaimModel user, string id)
        {
            try
            {
                var repoResult = await _uow.MstClassValue.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstClassValue.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstClassValueDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }


        public async Task<Result<IEnumerable<TMstClassValueDto>>> GetAll()
        {
            try
            {
                var repoResult = await _uow.MstClassValue.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<TMstClassValueDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstClassValueDto>> GetById(string id)
        {
            try
            {
                var repoResult = await _uow.MstClassValue.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<TMstClassValueDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }   
        }

        public Task<Result<TMstClassValueDto>> GetByParam(TMstClassValueDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetLastCode()
            {
            try
            {
                var lastBrandCode = await _uow.MstClassValue.Set().OrderByDescending(b => b.CreatedDate).FirstOrDefaultAsync();

                if (lastBrandCode == null)
                    return Result.Ok("CV0001");


                string prefix = lastBrandCode.Code.Substring(0, 2); // Misalnya, 'B' dari 'B000'
                int lastNumber = int.Parse(lastBrandCode.Code.Substring(2)); // Misalnya, 000 dari 'B000'

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

            throw new NotImplementedException();
        }

        public Task<Result<TMstClassValueDto>> GetListByParam(TMstClassValueDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TMstClassValueDto>> Update(UserClaimModel user, TMstClassCreatedDto data)
        {
            try
            {
                var repoResult = await _uow.MstClassValue.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                string CreatedBy = repoResult.CreatedBy;



                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.Name= data.Name;
                repoResult.CreatedBy = CreatedBy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.MstClassValue.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstClassValueDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }





    }

}
