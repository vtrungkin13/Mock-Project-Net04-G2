﻿using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Repositories.Interfaces
{
    public interface ICooperateRepository : IGenericRepository<Cooperate>
    {
        Task<List<Cooperate>> FindByCampaignIdAsync(int campaignId);
    }
}
