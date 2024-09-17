using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Repositories.Interfaces
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        Task<List<Campaign>> GetAllCampaignsAsync();
        Task<List<Campaign>> FilterCampaignsByStatusAsync(StatusEnum status);
        Task<Campaign> GetCampaignByIdAsync(int id);
        Task<List<Campaign>> CampaignPagingAsync(int page, int pageSize);
    }
}
