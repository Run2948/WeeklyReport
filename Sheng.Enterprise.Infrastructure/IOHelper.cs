using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sheng.Enterprise.Infrastructure
{
	public class IOHelper
	{
		public static string GetMD5HashFromFile(string fileName)
		{
			FileStream fileStream = null;
			string result;
			try
			{
				fileStream = new FileStream(fileName, FileMode.Open);
				byte[] array = new MD5CryptoServiceProvider().ComputeHash(fileStream);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("GetMD5HashFromFile() fail,error:\r\n" + ex.Message);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
					fileStream.Dispose();
				}
			}
			return result;
		}

		public static string GetMD5HashFromString(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			byte[] array = new MD5CryptoServiceProvider().ComputeHash(bytes);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("x2");
			}
			return text.ToUpper();
		}
	}
}
