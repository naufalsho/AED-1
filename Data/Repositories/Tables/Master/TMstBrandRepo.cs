﻿using Core.Interfaces.IRepositories.Tables;
using Core.Interfaces.IRepositories.Tables.Master;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;

namespace Data.Repositories.Tables.Master
{
    public class TMstBrandRepo : BaseRepository<TMstBrand>, IMstBrandRepo
    {
        public TMstBrandRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
