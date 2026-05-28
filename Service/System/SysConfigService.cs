using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Autofac;
using Common.Enum;
using Entity.Base;
using Entity.Constant;
using Entity.DbModel.System;
using Entity.Dto.Req;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using HybirdFrameworkCore.Redis;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security;
using Repository.System;
using Service.Mgr;
using SqlSugar;

namespace Service.System
{
    [Scope("SingleInstance")]
    public class SysConfigService : BaseServices<SysConfig>
    {
        private static readonly ConcurrentDictionary<string, string> Dictionary = new ConcurrentDictionary<string, string>();
        private readonly SysFileMgr _sysFileMgr;
        private readonly SysConfigRepository _sysConfigRep;

        public SysConfigService(SysConfigRepository sysConfigRep, SysFileMgr sysFileMgr)
        {
            _sysConfigRep = sysConfigRep;
            _sysFileMgr = sysFileMgr;
        }
        RedisHelper redisHelper = AppInfo.Container.Resolve<RedisHelper>();
        /// <summary>
        /// 获取参数配置分页列表 🔖
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<SysConfig>> Page(PageConfigReq input)
        {
            RefAsync<int> total = 0;
            var items = await _sysConfigRep.SysConfigQueryPageAsync(
                !string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name),
                !string.IsNullOrEmpty(input.Code), u => u.Code.Contains(input.Code),
                !string.IsNullOrEmpty(input.GroupCode), u => u.GroupCode.Equals(input.GroupCode),
                input.PageNum, input.PageSize, total, input);
            return new PageResult<SysConfig>()
            {
                PageNum = input.PageNum,
                PageSize = input.PageSize,
                ToTal = total,
                Rows = items,
            };
        }

        /// <summary>
        /// 获取参数配置列表 🔖
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取参数配置列表")]
        public async Task<List<SysConfig>> GetList()
        {
            return await _sysConfigRep.QueryAsync();
        }

        /// <summary>
        /// 获取分组列表 🔖
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetGroupList()
        {
            return await _sysConfigRep.QueryByGroupByAsync<string>(u => u.GroupCode, u => u.GroupCode);
        }
        /// <summary>
        /// 获得同一分组下 SysConfig
        /// </summary>
        /// <param name="GroupCode"></param>
        /// <returns></returns>
        public async Task<List<SysConfig>> GetGroupList(string GroupCode)
        {
            return await _sysConfigRep.QueryListByClauseAsync(u => u.GroupCode == GroupCode);
        }


        /// <summary>
        /// 增加参数配置 🔖
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[ApiDescriptionSettings(Name = "Add"), HttpPost]
        public async Task<string> AddConfig(AddConfigReq input)
        {
            string result = "";
            var isExist = await _sysConfigRep.QueryByClauseAsync(u => u.Name == input.Name || u.Code == input.Code);
            if (isExist != null)
                result = "已存在同名或同编码参数配置";
            SysConfig insertAsync = await _sysConfigRep.InsertAsync(input.Adapt<SysConfig>());
            if (insertAsync.Id > 0)
                result = "增加参数配置成功";
            return result;

        }

        /// <summary>
        /// 更新参数配置 🔖
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       // [ApiDescriptionSettings(Name = "Update"), HttpPost]
        [DisplayName("更新参数配置")]
        public async Task<string> UpdateConfig(UpdateConfigReq input)
        {
            string result = "";
            var isExist = await _sysConfigRep.QueryByClauseAsync(u => (u.Name == input.Name || u.Code == input.Code) && u.Id != input.Id);
            if (isExist != null)
                result = "已存在同名或同编码参数配置";
            var config = input.Adapt<SysConfig>();
            int updateResult = await _sysConfigRep.UpdateAsync(config, true);
            if (updateResult > 0)
                result = "更新参数配置成功";
            redisHelper.Remove(config.Code);
            return result;
        }
        /// <summary>
        /// 删除参数配置 🔖
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[ApiDescriptionSettings(Name = "Delete"), HttpPost]
        [DisplayName("删除参数配置")]
        public async Task<string> DeleteConfig(DeleteConfigReq input)
        {
            string result = "";
            var config = await _sysConfigRep.QueryByClauseAsync(u => u.Id == input.Id);
            if (config.SysFlag == YesNoEnum.Y) // 禁止删除系统参数
                result = "禁止删除系统参数";
            bool deleteResult = await _sysConfigRep.DeleteAsync(config);
            if (deleteResult)
                result = "删除参数配置成功";
            redisHelper.Remove(config.Code);
            return result;
        }

        /// <summary>
        /// 批量删除参数配置 🔖
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
       // [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
        [DisplayName("批量删除参数配置")]
        public async Task BatchDeleteConfig(List<long> ids)
        {
            foreach (var id in ids)
            {
                var config = await _sysConfigRep.QueryByClauseAsync(u => u.Id == id);
                if (config.SysFlag == YesNoEnum.Y) // 禁止删除系统参数
                    continue;

                await _sysConfigRep.DeleteAsync(config);
                redisHelper.Remove(config.Code);
            }
        }

        /// <summary>
        /// 获取参数配置详情 🔖
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("获取参数配置详情")]
        public async Task<SysConfig> GetDetail([FromQuery] ConfigReq input)
        {
            return await _sysConfigRep.QueryByClauseAsync(u => u.Id == input.Id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">GroupCode.code</param>
        /// <returns></returns>
        public string? Get(string key)
        {
            string[] keys = key.Split('.');
            if (keys.Length != 2)
            {
                throw new InvalidParameterException("配置数据key格式错误");
            }

            if (Dictionary.TryGetValue(key, out string? value))
            {
                return value;
            }

            SysConfig sysConfig = _sysConfigRep.QueryByClause(i => i.GroupCode == keys[0] && i.Code == keys[1]);
            if (sysConfig == null)
            {
                return null;
            }

            return sysConfig.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">GroupCode.code</param>
        /// <param name="value"></param>
        public bool Set(string key, object value)
        {
            bool setResult = false;
            string[] keys = key.Split('.');
            if (keys.Length != 2)
            {
                throw new InvalidParameterException("配置数据key格式错误");
            }

            string newValue = Convert.ToString(value);

            Dictionary.AddOrUpdate(key, newValue, (s, s1) => newValue);
            redisHelper.SetKeyValueStr(key, newValue);

            SysConfig sysConfig = _sysConfigRep.QueryByClause(i => i.GroupCode == keys[0] && i.Code == keys[1]);
            if (sysConfig == null)
            {
                sysConfig= _sysConfigRep.Insert(new SysConfig()
                {
                    GroupCode = keys[0],
                    Code = keys[1],
                    Value = newValue,
                    SysFlag = YesNoEnum.N,
                    Name = key
                });
                setResult = sysConfig !=null;
            }
            else
            {
                sysConfig.Value = newValue;
                setResult = _sysConfigRep.Update(sysConfig);
            }
            return setResult;
        }
        private readonly string _imageType = ".jpg.png.bmp.gif.tif";
        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<SysFile> UploadAvatar([Required] IFormFile file, string path)
        {
            var sysFile = await _sysFileMgr.HandleUploadFile(file, path, _imageType);
            bool setResult = Set(StationParamConst.Cover, sysFile.Url);
            if (setResult)
                return sysFile;
            else
                return null;
        }

    }
}
