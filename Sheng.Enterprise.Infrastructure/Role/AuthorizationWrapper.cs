using System;
using System.Collections;
using System.Collections.Generic;

namespace Sheng.Enterprise.Infrastructure
{
	public class AuthorizationWrapper
	{
		private Hashtable _authorizationHashtable = new Hashtable();

		private List<Authorization> authorizationList = new List<Authorization>();

		public List<Authorization> AuthorizationList
		{
			get
			{
				return this.authorizationList;
			}
			set
			{
				this.authorizationList = value;
				this._authorizationHashtable.Clear();
				if (value != null)
				{
					foreach (Authorization current in this.authorizationList)
					{
						if (current != null && !string.IsNullOrEmpty(current.Key) && !this._authorizationHashtable.ContainsKey(current.Key))
						{
							this._authorizationHashtable.Add(current.Key, current);
						}
					}
				}
			}
		}

		public bool Verify(string key)
		{
			return string.IsNullOrEmpty(key) || this._authorizationHashtable.ContainsKey(key);
		}
	}
}
