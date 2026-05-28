using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Aliyun.OSS.Util;
using Common.Util;
using Entity.DbModel.System;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Configuration;
using Microsoft.AspNetCore.Http;
using Repository.System;

namespace Service.Mgr
{
    [Scope("SingleInstance")]
    public class SysFileMgr
    {
        private readonly SysFileRepository _sysFileRep;
        private readonly UploadOptions _uploadOptions;
        private readonly string _imageType = ".jpg.png.bmp.gif.tif";
        public SysFileMgr(SysFileRepository sysFileRep)
        {
            _sysFileRep = sysFileRep;
            _uploadOptions = new UploadOptions
            {
                Path = AppSettingsHelper.GetContent("Upload", "Path"),
                MaxSize = Convert.ToInt64(AppSettingsHelper.GetContent("Upload", "MaxSize")),
                ContentType = new List<string> { "image/jpg", "image/png", "image/jpeg", "image/gif", "image/bmp", "text/plain", "application/pdf", "application/msword", "application/vnd.ms-excel", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "video/mp4" },
                EnableMd5 = false,
            };
        }


        public async Task<SysFile> UploadAvatar([Required] IFormFile file, string path)
        {
            var sysFile = await HandleUploadFile(file, path, _imageType);
            return sysFile;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="savePath">路径</param>
        /// <param name="allowSuffix">允许格式：.jpg.png.gif.tif.bmp</param>
        /// <returns></returns>
        public async Task<SysFile> HandleUploadFile(IFormFile file, string savePath, string allowSuffix = "")
        {
            if (file == null)
                throw new ArgumentException($"文件不存在");//文件不存在

            // 判断是否重复上传的文件
            var sizeKb = (long)(file.Length / 1024.0); // 大小KB
            var fileMd5 = string.Empty;
            if (_uploadOptions.EnableMd5)
            {
                using (var fileStream = file.OpenReadStream())
                {
                    fileMd5 = OssUtils.ComputeContentMd5(fileStream, fileStream.Length);
                }
                /*
                 * Mysql8 中如果使用了 utf8mb4_general_ci 之外的编码会出错，尽量避免在条件里使用.ToString()
                 * 因为 Squsugar 并不是把变量转换为字符串来构造SQL语句，而是构造了CAST(123 AS CHAR)这样的语句，这样这个返回值是utf8mb4_general_ci，所以容易出错。
                 */
                var strSizeKb = sizeKb.ToString();
                var sysFile = await _sysFileRep.QueryByClauseAsync(u => u.FileMd5 == fileMd5 && (u.SizeKb == null || u.SizeKb == strSizeKb));
                if (sysFile != null) return sysFile;
            }

            var path = savePath;
            if (string.IsNullOrWhiteSpace(savePath))
            {
                path = _uploadOptions.Path;
                var reg = new Regex(@"(\{.+?})");
                var match = reg.Matches(path);
                match.ToList().ForEach(a =>
                {
                    var str = DateTime.Now.ToString(a.ToString().Substring(1, a.Length - 2)); // 每天一个目录
                    path = path.Replace(a.ToString(), str);
                });
            }

            // 验证文件类型
            if (!_uploadOptions.ContentType.Contains(file.ContentType))
                throw new ArgumentException($"不允许的文件类型");
            // 验证文件大小
            if (sizeKb > _uploadOptions.MaxSize)
                throw new ArgumentException($"文件超过允许大小");
            // 获取文件后缀
            var suffix = Path.GetExtension(file.FileName).ToLower(); // 后缀
            if (string.IsNullOrWhiteSpace(suffix))
                throw new ArgumentException($"文件后缀错误");
            var newFile = new SysFile
            {
                //Id = YitIdHelper.NextId(),

                FileName = Path.GetFileNameWithoutExtension(file.FileName),
                Suffix = suffix,
                SizeKb = sizeKb.ToString(),

                FileMd5 = fileMd5,
            };

            var finalName = newFile.Id + suffix; // 文件最终名称

            string time = DateTime.Now.ToString("yyyyMMdd");

            var filePath = Path.Combine(savePath, time);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath = Path.Combine(filePath, finalName);


            // 使用FileStream保存文件
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            newFile.FilePath = filePath;
            newFile.Url = AppSettingsHelper.GetContent("HttpContextRequest", "Scheme") + "/" + time + "/" + finalName;
            await _sysFileRep.InsertAsync(newFile);
            SysFile newAddFile = await _sysFileRep.QueryByClauseAsync(u => u.FilePath == filePath);
            if (newAddFile != null)
            {
                newFile.Id = newAddFile.Id;
                return newFile;
            }
            else
                return null;

        }

    }
}
