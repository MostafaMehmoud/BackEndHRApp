using AutoMapper;

using Microsoft.EntityFrameworkCore;


using HrApp.BL.Services.IServices;
using HrApp.DAL.Data;
using HrApp.DAL.Dtos;


namespace HrApp.BL.Services;

public class BranchService : IBranchService
{
    private readonly IMapper _mapper;
    private readonly HrAppDbContext _dbcontext;

    public BranchService(IMapper mapper, HrAppDbContext dbcontext)
    {
        _mapper = mapper;
        _dbcontext = dbcontext;
    }

    public async Task<List<ModelOutputDto>> GetAllBranchAsync()
    {
        var list =await _dbcontext.Branches.Select(m => new ModelOutputDto()
        {
            Id = m.Id,
            NameAr = m.NameAr,
            NameEn = m.NameEn,
        }).ToListAsync();

        return list;
    }

}
