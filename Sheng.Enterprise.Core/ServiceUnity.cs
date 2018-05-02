using Linkup.Common;
using Linkup.Data;
using System;

namespace Sheng.Enterprise.Core
{
	public class ServiceUnity
	{
		private static readonly ServiceUnity _instance = new ServiceUnity();

		private DatabaseWrapper _dataBase = new DatabaseWrapper();

		private LogService _log = LogService.Instance;

		private ExceptionHandlingService _exceptionHandling = ExceptionHandlingService.Instance;

		public static ServiceUnity Instance
		{
			get
			{
				return _instance;
			}
		}

		public DatabaseWrapper Database
		{
			get
			{
				return this._dataBase;
			}
			set
			{
				this._dataBase = value;
			}
		}

		public LogService Log
		{
			get
			{
				return this._log;
			}
		}

		public ExceptionHandlingService ExceptionHandling
		{
			get
			{
				return this._exceptionHandling;
			}
		}
	}
}
