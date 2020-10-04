using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BusinessLogic
{
	public sealed class LogManager
	{
		private static readonly LogManager _current = new LogManager();
		///this should be common for the whole api

		/// <summary>
		/// Gets the current <see cref="LogManager"/>.
		/// </summary>
		/// <value>The current.</value>
		public static LogManager Current
		{
			get { return _current; }
		}

		/// <summary>
		/// Gets the <see cref="ILog"/>.
		/// </summary>
		/// <value>The log.</value>
		public ILog Log
		{
			get
			{
				return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			}
		}
	}
}
