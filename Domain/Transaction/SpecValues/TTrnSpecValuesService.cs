using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO.Pipelines;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;




namespace Domain.Transaction.SpecValues
{
    public class TTrnSpecValuesService : ITrnSpecValuesService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TTrnSpecValuesService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Result<TTrnSpecValuesDto>> Create(UserClaimModel user, TTrnSpecValuesCreatedDto data)
        {
            try
            {
                TTrnSpecValuesDto resultDto = null;

                // Iterasi melalui SpecValues dari data
                foreach (var item in data.SpecValues)
                {
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        // Cek apakah item sudah ada di database berdasarkan ModelCode dan SpecItemCode
                        var existingItem = await _uow.TrnSpecValues.Set().FirstOrDefaultAsync(x => x.ModelCode == data.ModelCode && x.SpecItemCode == item.SpecItemCode);

                        if (existingItem != null)
                        {
                            // Update nilai jika item sudah ada
                            existingItem.Values = item.Value;
                            existingItem.UpdatedBy = user.NameIdentifier;
                            existingItem.UpdatedDate = DateTime.UtcNow;

                            // Tandai entitas untuk update
                            _uow.TrnSpecValues.Update(existingItem);

                            resultDto = _mapper.Map<TTrnSpecValuesDto>(existingItem);
                        }
                        else
                        {
                            // Membuat instance baru dari TTrnSpecValues jika item belum ada
                            var param = new TTrnSpecValues
                            {
                                Id = null,
                                ModelCode = data.ModelCode,
                                SpecItemCode = item.SpecItemCode,
                                Values = item.Value,
                                CreatedBy = user.NameIdentifier,
                                CreatedDate = DateTime.UtcNow
                            };

                            // Menambahkan entitas ke unit of work untuk disimpan
                            await _uow.TrnSpecValues.Add(param);

                            resultDto = _mapper.Map<TTrnSpecValuesDto>(param);
                        }
                    }
                }

                // Menyimpan semua perubahan ke database
                await _uow.CompleteAsync();

                return Result.Ok(resultDto);
            }
            catch (Exception ex)
            {
                // Menangani error jika terjadi
                return Result.Fail<TTrnSpecValuesDto>(ex.Message);
            }
        }


        public Task<Result<TTrnSpecValuesDto>> Delete(UserClaimModel user, int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<TTrnSpecValuesDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<TTrnSpecValuesDto>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TTrnSpecValuesDto>> GetByParam(TTrnSpecValuesFilterDto param)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> GetLastCode()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<TTrnSpecValuesDto>>> GetListByParam(TTrnSpecValuesFilterDto param)
        {
            try
            {

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("CategoryCode", param.CategoryCode),
                    new SqlParameter("ModelCode", param.ModelCode)
                };
                var repoResult = await _uow.VwSpecValueMatriks.ExecuteStoredProcedure("sp_GetSpecValue", parameters);


                var result = _mapper.Map<IEnumerable<TTrnSpecValuesDto>>(repoResult);


                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }

        }

		public Task<Result<TTrnSpecValuesDto>> Update(UserClaimModel user, TTrnSpecValuesCreatedDto data)
        {
            throw new NotImplementedException();
        }
    }

}
