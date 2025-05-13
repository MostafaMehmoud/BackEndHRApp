using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface INeighborService
    {
        Task<ApiResponse<Neighbor>> Add(CreateNeighbor createNeighbor);
        Task<ApiResponse<Neighbor>> Update(UpdateNeighbor updateNeighbor);
        Task<ApiResponse<Neighbor>> Delete(string id);
        Task<ApiResponse<Neighbor>> GetById(string id);
        Task<ApiResponse<List<Neighbor>>> GetAll();
    }
}
