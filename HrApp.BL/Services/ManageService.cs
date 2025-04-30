using AutoMapper;

using Microsoft.EntityFrameworkCore;

using HrApp.BL.Services.IServices;
using HrApp.DAL.Data;
using HrApp.DAL.Dtos;

namespace HrApp.BL.Services;

public class ManageService : IManageService
{
    private readonly IMapper _mapper;
    private readonly HrAppDbContext _dbcontext;

    public ManageService(IMapper mapper, HrAppDbContext dbcontext)
    {
        _mapper = mapper;
        _dbcontext = dbcontext;
    }

    public async Task<List<ModelOutputDto>> GetAllManageAsync()
    {
        var list =await _dbcontext.Manages.Select(m => new ModelOutputDto()
        {
            Id = m.Id,
            NameAr = m.NameAr,
            NameEn = m.NameEn,
        }).ToListAsync();

        return list;
    }

}
