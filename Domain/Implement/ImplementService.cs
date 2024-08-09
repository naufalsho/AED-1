using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Data;
using FluentResults;
using Microsoft.Data.SqlClient;

using Dapper;
using Domain.Master.MasterModel;

namespace Domain.Comparison
{
	public class ImplementService : IImplementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public ImplementService(
            IUnitOfWork uow,
            IMapper mapper,
            ApplicationContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }


        public async Task<Result<ImplementDto>> GetImplement(ImplementDto param)
        {
            try
            {
                var connectionString = "Server=10.0.10.73;Initial Catalog=AED;User ID=sa;Password=tnhc1$d13;TrustServerCertificate=False;Connection Timeout=30;";
                var parameters = new DynamicParameters();
                parameters.Add("ModelCode", param.ModelCode);

                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                var modelCodeCondition = param.ModelCode != null ? $"AND Code = '{param.ModelCode}'" : "";

                var modelsQuery = $"SELECT * FROM MstModel WHERE Type = 'Attachment' AND IsActive = 1  {modelCodeCondition}";
                var models = await connection.QueryAsync<TMstModelDto>(modelsQuery);

                var attchCondition = param.ModelCode != null ? $"AND ModelCodeAttach = '{param.ModelCode}'" : "";

                var sql = @$"
                    SELECT 
                        A.ModelCodeAttach, 
                        A.ModelCode, 
                        B.Model AS ModelName, 
                        A.ClassValueCode, 
                        C.Name AS Value, 
                        B.ModelImage
                    FROM ImplementMatriks A
                    INNER JOIN MstModel B ON A.ModelCode = B.Code
                    INNER JOIN MstClassValue C ON C.Code = A.ClassValueCode
                    WHERE B.IsActive = 1  {attchCondition}
                ";

                var attachments = await connection.QueryAsync<ImplementDto>(sql);
                var implementDtos = new ImplementDto
                {
                    ListModel = models.ToList(),
                    Implements = attachments.ToList()
                };

                return Result.Ok(implementDtos);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());

            }
        }


        public async Task<Result<ImplementDto>> GetProductModel(ImplementDto param)
        {
            try
            {
                var connectionString = "Server=10.0.10.73;Initial Catalog=AED;User ID=sa;Password=tnhc1$d13;TrustServerCertificate=False;Connection Timeout=30;";
                var parameters = new DynamicParameters();
                parameters.Add("ModelCode", param.ModelCode);

                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                var modelCodeCondition = param.ModelCode != null ? $"AND Code = '{param.ModelCode}'" : "";

                var modelsQuery = $"SELECT * FROM MstModel WHERE Type = 'Unit' AND IsActive = 1 {modelCodeCondition}";
                var models = await connection.QueryAsync<TMstModelDto>(modelsQuery);

                var attchCondition = param.ModelCode != null ? $"AND ModelCode = '{param.ModelCode}'" : "";

                var sql = @$"
                    SELECT 
                        A.ModelCodeAttach, 
                        A.ModelCode, 
                        B.Model AS ModelName, 
                        A.ClassValueCode, 
                        C.Name AS Value,
                        B.ModelImage
                    FROM ImplementMatriks A
                    INNER JOIN MstModel B ON A.ModelCodeAttach = B.Code
                    INNER JOIN MstClassValue C ON C.Code = A.ClassValueCode
                    WHERE  B.IsActive = 1 {attchCondition}
                ";

                var attachments = await connection.QueryAsync<ImplementDto>(sql);
                var implementDtos = new ImplementDto
                {
                    ListModel = models.ToList(),
                    Implements = attachments.ToList()
                };

                return Result.Ok(implementDtos);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());

            }
        }


    }
}
