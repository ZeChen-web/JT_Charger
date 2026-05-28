using Common.Enum;
using HybirdFrameworkCore.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util
{
    public class CryptogramUtil
    {
        public static readonly bool StrongPassword = Boolean.Parse((ReadOnlySpan<char>)AppSettingsHelper.GetContent("Cryptogram", "StrongPassword"));
        public static readonly string PasswordStrengthValidation = AppSettingsHelper.GetContent("Cryptogram", "PasswordStrengthValidation");
        public static readonly string PasswordStrengthValidationMsg = AppSettingsHelper.GetContent("Cryptogram", "PasswordStrengthValidationMsg");
        public static readonly string CryptoType = AppSettingsHelper.GetContent("Cryptogram", "CryptoType");
        public static readonly string PublicKey = AppSettingsHelper.GetContent("Cryptogram", "PublicKey");
        public static readonly string PrivateKey = AppSettingsHelper.GetContent("Cryptogram", "PrivateKey");
        public static readonly string SM4_key = "0123456789abcdeffedcba9876543210";
        public static readonly string SM4_iv = "595298c7c6fd271f0402f804c33d3f66";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            if (CryptoType == CryptogramEnum.MD5.ToString())
            {
                //此处默认MD532位加密
                return MD5Util.MD5Encrypt32(plainText);
            }
            else if (CryptoType == CryptogramEnum.SM2.ToString())
            {
                return SM2Encrypt(plainText);
            }
            else if (CryptoType == CryptogramEnum.SM4.ToString())
            {
                return SM4EncryptECB(plainText);
            }
            return plainText;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            if (CryptoType == CryptogramEnum.SM2.ToString())
            {
                return SM2Decrypt(cipherText);
            }
            else if (CryptoType == CryptogramEnum.SM4.ToString())
            {
                return SM4DecryptECB(cipherText);
            }
            return cipherText;
        }

        /// <summary>
        /// SM2加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string SM2Encrypt(string plainText)
        {
            return GMUtil.SM2Encrypt(PublicKey, plainText);
        }

        /// <summary>
        /// SM2解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string SM2Decrypt(string cipherText)
        {
            return GMUtil.SM2Decrypt(PrivateKey, cipherText);
        }

        /// <summary>
        /// SM4加密（ECB）
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string SM4EncryptECB(string plainText)
        {
            return GMUtil.SM4EncryptECB(SM4_key, plainText);
        }

        /// <summary>
        /// SM4解密（ECB）
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string SM4DecryptECB(string cipherText)
        {
            return GMUtil.SM4DecryptECB(SM4_key, cipherText);
        }

        /// <summary>
        /// SM4加密（CBC）
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string SM4EncryptCBC(string plainText)
        {
            return GMUtil.SM4EncryptCBC(SM4_key, SM4_iv, plainText);
        }

        /// <summary>
        /// SM4解密（CBC）
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string SM4DecryptCBC(string cipherText)
        {
            return GMUtil.SM4DecryptCBC(SM4_key, SM4_iv, cipherText);
        }
    }
}
