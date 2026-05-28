
using Entity.Base;
using Entity.DbModel.System;
using Entity.Dto.Req;
using HybirdFrameworkCore.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.System;

namespace WebStarter.Controllers.System
{
    [Produces("application/json")]
    [ApiController]
    public class SysConfigController
    {
        private readonly SysConfigService _sysConfigService;
        public SysConfigController(SysConfigService sysConfigService)
        {
            _sysConfigService = sysConfigService;
        }

        [HttpPost]
        [Route("/api/sysConfig/page")]
        public async Task<Result<PageResult<SysConfig>>> Page(PageConfigReq input)
        {
            return Result<PageResult<SysConfig>>.Success(await _sysConfigService.Page(input));
        }

        [HttpGet]
        [Route("/api/sysConfig/list")]
        public async Task<List<SysConfig>> GetList()
        {
            return await _sysConfigService.GetList();
        }

        [HttpGet]
        [Route("/api/sysConfig/detail")]
        public async Task<SysConfig> GetDetail([FromQuery] ConfigReq input)
        {
            return await _sysConfigService.GetDetail(input);
        }

        [HttpGet]
        [Route("/api/sysConfig/groupList")]
        public async Task<List<string>> GetGroupList()
        {
            return await _sysConfigService.GetGroupList();
        }

        [HttpPost]
        [Route("/api/sysConfig/add")]
        public async Task Add(AddConfigReq input)
        {
            await _sysConfigService.AddConfig(input);
        }

        [HttpPost]
        [Route("/api/sysConfig/update")]
        public async Task Update(UpdateConfigReq input)
        {
            await _sysConfigService.UpdateConfig(input);
        }

        [HttpPost]
        [Route("/api/sysConfig/delete")]
        public async Task Delete(DeleteConfigReq input)
        {
            await _sysConfigService.DeleteConfig(input);
        }

        [HttpPost]
        [Route("/api/sysConfig/batchDelete")]
        public async Task BatchDelete(List<long> input)
        {
            await _sysConfigService.BatchDeleteConfig(input);
        }
    }
}
