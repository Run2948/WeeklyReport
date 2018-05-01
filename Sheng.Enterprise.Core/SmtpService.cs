using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Sheng.Enterprise.Core
{
	public class SmtpService
	{
		private static readonly SmtpService _instance = new SmtpService();

		private SmtpClient _smtpClient = new SmtpClient();

		private string _emailPassword = ConfigurationManager.AppSettings["emailPassword"];

		public static SmtpService Instance
		{
			get
			{
				return SmtpService._instance;
			}
		}

		private SmtpService()
		{
			this._smtpClient.Host = "smtp.163.com";
			this._smtpClient.Port = 25;
			this._smtpClient.UseDefaultCredentials = true;
			this._smtpClient.Credentials = new NetworkCredential("linkup_noreply", this._emailPassword);
			this._smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
		}

		public void Send(MailMessage mailMessage)
		{
			if (mailMessage == null)
			{
				return;
			}
			try
			{
				this._smtpClient.Send(mailMessage);
			}
			catch
			{
			}
		}
	}
}
