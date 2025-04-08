using System.Diagnostics;
using System;
using System.Data;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace WebApiServices.Models
{
    public class UT_Kerberos
    {
        private const string Co_LlavePrivada = "P2#5o*gH";

        /// <summary>
        /// Encripta una cadena con una llave privada pre-establecida.
        /// </summary>
        /// <param name="str_pEncriptar">La cadena a Encriptar.</param>
        /// <returns>El criptograma resultante.</returns>
        public string Encriptar(string str_pEncriptar)
        {
            return Encriptar(str_pEncriptar, Co_LlavePrivada);
        }

        /// <summary>
        /// Encripta una cadena con la llave privada indicada.
        /// </summary>
        /// <param name="str_pEncriptar">La cadena a Encriptar.</param>
        /// <param name="str_pKeyPrivada8carac">La llave privada usada para encriptar la cadena.</param>
        /// <returns>El criptograma resultante.</returns>
        public static string Encriptar(string str_pEncriptar, string str_pKeyPrivada8carac)
        {
            string str_ReturnValue;
            DESCryptoServiceProvider obj_DESCryptoServiceProvider;
            byte[] byt_InputArray;
            MemoryStream ms_Memory;
            CryptoStream cs_Crypto;
            byte[] byt_mIV = { 0x45, 0x32, 0xA5, 0x18, 0x67, 0x58, 0xAC, 0xBA };
            byte[] byt_mkey = { };

            try
            {
                byt_mkey = System.Text.Encoding.UTF8.GetBytes(str_pKeyPrivada8carac.Substring(0, 8));
                obj_DESCryptoServiceProvider = new DESCryptoServiceProvider();
                byt_InputArray = Encoding.UTF8.GetBytes(str_pEncriptar);
                ms_Memory = new MemoryStream();
                cs_Crypto = new CryptoStream(ms_Memory, obj_DESCryptoServiceProvider.CreateEncryptor(byt_mkey, byt_mIV), CryptoStreamMode.Write);
                cs_Crypto.Write(byt_InputArray, 0, byt_InputArray.Length);
                cs_Crypto.FlushFinalBlock();
                str_ReturnValue = Convert.ToBase64String(ms_Memory.ToArray());
            }
            catch (Exception)
            {
                str_ReturnValue = "";
            }
            finally
            {
                obj_DESCryptoServiceProvider = null;
                ms_Memory = null;
                cs_Crypto = null;
            }
            return str_ReturnValue;
        }

        /// <summary>
        /// Desencripta una cadena con una llave privada pre-establecida.
        /// </summary>
        /// <param name="str_pDesEncriptar">El criptograma a desencriptar.</param>
        /// <returns>El texto claro resultante de la desencriptación.</returns>
        public string Desencriptar(string str_pDesEncriptar)
        {
            return Desencriptar(str_pDesEncriptar, Co_LlavePrivada);
        }

        /// <summary>
        /// Desencripta una cadena con la llave privada indicada.
        /// </summary>
        /// <param name="str_pDesEncriptar">El criptograma a desencriptar.</param>
        /// <param name="str_pKeyPrivada8carac">La llave privada usada para desencriptar la cadena.</param>
        /// <returns>Retorna la cadena desencriptada.</returns>
        public static string Desencriptar(string str_pDesEncriptar, string str_pKeyPrivada8carac)
        {
            string str_ReturnValue;
            byte[] byt_InputArray = new byte[str_pDesEncriptar.Length + 1];
            DESCryptoServiceProvider obj_DESCryptoServiceProvider;
            MemoryStream ms_Memory;
            CryptoStream cs_Crypto;
            System.Text.Encoding obj_Encoding;
            byte[] byt_mIV = { 0x45, 0x32, 0xA5, 0x18, 0x67, 0x58, 0xAC, 0xBA };
            byte[] byt_mkey = { };

            try
            {
                byt_mkey = System.Text.Encoding.UTF8.GetBytes(str_pKeyPrivada8carac.Substring(0, 8));
                obj_DESCryptoServiceProvider = new DESCryptoServiceProvider();
                byt_InputArray = Convert.FromBase64String(str_pDesEncriptar);
                ms_Memory = new MemoryStream();
                cs_Crypto = new CryptoStream(ms_Memory, obj_DESCryptoServiceProvider.CreateDecryptor(byt_mkey, byt_mIV), CryptoStreamMode.Write);
                cs_Crypto.Write(byt_InputArray, 0, byt_InputArray.Length);
                cs_Crypto.FlushFinalBlock();
                obj_Encoding = System.Text.Encoding.UTF8;
                str_ReturnValue = obj_Encoding.GetString(ms_Memory.ToArray());
            }
            catch (Exception)
            {
                str_ReturnValue = "";
            }
            finally
            {
                obj_DESCryptoServiceProvider = null;
                ms_Memory = null;
                cs_Crypto = null;
            }
            return str_ReturnValue;
        }

        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = @"C:\Logs\";
            logFilePath = logFilePath + "Logs-" + System.DateTime.Today.ToString("yyyy-MM-dd") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }
    }
}