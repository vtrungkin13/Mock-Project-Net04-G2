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
        Task<List<Campaign>> FilterCampaignsByStatusAsync(StatusEnum status,int page,int pageSize);
        Task<Campaign> GetCampaignByIdAsync(int id);
        Task<List<Campaign>> CampaignPagingAsync(int page, int pageSize);
        Task<List<Campaign>> SearchCampaignsAsync(string campaignCode, string organizationPhone);
        Task<int> TotalCampaignsCountAsync();

        IQueryable<Campaign> GetAll();
        IQueryable<Cooperate> GetCooperations();
    }
}
