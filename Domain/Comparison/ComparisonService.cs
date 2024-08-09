using AutoMapper;
using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models.Entities.Views;
using Data;
using Domain.Transaction.SpecValues;
using Domain.UnitSpec;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

using Dapper;
using System.Data;
using Domain.Master.MasterCategory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Comparison
{
    public class ComparisonService : IComparisonService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public ComparisonService(
            IUnitOfWork uow,
            IMapper mapper,
            ApplicationContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }


        public async Task<Result<IEnumerable<ComparisonDto>>> Generate(ComparisonFilterDto param)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CategoryCode", param.CategoryCode);
                parameters.Add("@ModelCode", param.ModelCode);

                var connectionString = "Server=10.0.10.73;Initial Catalog=AED;User ID=sa;Password=tnhc1$d13;TrustServerCertificate=False;Connection Timeout=30;";
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var repoResult = await connection.QueryAsync<dynamic>("sp_GenerateComparison", parameters, commandType: CommandType.StoredProcedure);

                    var comparisonDtos = repoResult.Select(MapToComparisonDto).ToList();

                    return Result.Ok(comparisonDtos.AsEnumerable());
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.Message);
            }
        }



        private ComparisonDto MapToComparisonDto(dynamic row)
        {
            var dictionaryRow = (IDictionary<string, object>)row;
            var comparisonDto = new ComparisonDto
            {
                SpecItemCode = dictionaryRow["SpecItemCode"].ToString(),
                Items = dictionaryRow["Items"].ToString(),
                SubItems = dictionaryRow["SubItems"].ToString(),
                ModelCode = dictionaryRow
                    .Where(kvp => kvp.Key != "SpecItemCode" && kvp.Key != "Items" && kvp.Key != "SubItems")
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString()),
            };

            return comparisonDto;
        }
    }
}
