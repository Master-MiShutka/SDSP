/**
 * Utilities.cs
 * 
 **/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SDSP
{
	/// <summary>
	/// This is the utility class containing all helper methods
	/// </summary>
	static partial class U
	{
		#region Members

		/// <summary>
		/// Contains the time when the class was first initialized
		/// </summary>
		private static DateTime initTime;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the path to the logfile
		/// </summary>
		public static string LogFile { get; set; }

		/// <summary>
		/// Gets or sets the minimum level of messages to print/write
		/// </summary>
		public static LogLevel Level { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a utility class with a "Main.log" logfile in the TEMP folder and a Level of Warning
		/// </summary>
		static U()
		{
			//LogFile = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "Main.log");
            LogFile = Path.Combine(App.Current.StartupUri.AbsolutePath, "Main.log");
			Level = LogLevel.Warning;
			initTime = DateTime.Now;
		}

		#endregion

		#region Methods

		#region Public

		/// <summary>
		/// Logs a message to file and/or console.
		/// </summary>
		/// <param name="level">The level of the message (if this is lower than Level the message will be ignored)</param>
		/// <param name="caller">The caller of the message</param>
		/// <param name="message">The message</param>
		public static void L(LogLevel level, string caller, string message)
		{
			if (LevelToInt(level) < LevelToInt(Level)) return;

			TimeSpan ts = (DateTime.Now - initTime);
			string logLine = String.Format("{0} {1}:{2:00}:{3:00}.{4:000} ({5:G}) [{6}] {7}: {8}",
				ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds, DateTime.Now,
				LevelToString(level), // #7
				caller.ToUpper(),
				message);

			if (Level == LogLevel.Debug)
				Console.WriteLine(logLine);

#if (!DEBUG)
			System.IO.StreamWriter sw = null;
			try
			{
				sw = System.IO.File.AppendText(LogFile);
				sw.WriteLine(logLine);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Could not write to logfile: " + e.Message);
			}
			if (sw != null)
				sw.Close();
#endif
		}

		/// <summary>
		/// Logs a HttpWebResponse to file and/or console.
		/// </summary>
		/// <param name="level">The level of the message (if this is lower than Level the message will be ignored)</param>
		/// <param name="caller">The caller of the message</param>
		/// <param name="response">The HttpWebResponse object.</param>
		public static void L(LogLevel level, string caller, System.Net.HttpWebResponse response)
		{
			if (response == null)
				U.L(level, caller, "Response was empty.");
			else
			{
				U.L(level, caller, String.Format("Content Encoding: {0}", response.ContentEncoding));
				U.L(level, caller, String.Format("Content Type: {0}", response.ContentType));
				U.L(level, caller, String.Format("Status Description: {0}", response.StatusDescription));
				StreamReader sr = new StreamReader(response.GetResponseStream());
				string str;
				while ((str = sr.ReadLine()) != null)
					U.L(level, caller, str);
				U.L(level, caller, String.Format("-- End of response. Total bytes: {0} --", response.ContentLength));
			}
		}

		#endregion

		#region Private

		/// <summary>
		/// Converts a LogLevel to an integer
		/// </summary>
		/// <param name="level">The level to convert</param>
		/// <returns><paramref name="level"/> represented as an integer where Debug &lt; PropertiesWindow &lt; Warning &lt; Error</returns>
		private static int LevelToInt(LogLevel level)
		{
			if (level == LogLevel.Debug) return 1;
			else if (level == LogLevel.Information) return 2;
			else if (level == LogLevel.Warning) return 3;
			else if (level == LogLevel.Error) return 4;
			else return 0;
		}

		/// <summary>
		/// Converts a LogLevel to a string
		/// </summary>
		/// <param name="level">The level to convert</param>
		/// <returns><paramref name="level"/> represented as a string</returns>
		private static string LevelToString(LogLevel level)
		{
			if (level == LogLevel.Debug) return "DEBUG";
			else if (level == LogLevel.Information) return "INFO";
			else if (level == LogLevel.Warning) return "OOPS";
			else if (level == LogLevel.Error) return "SHIT";
			else return "HUH?";
		}

		#endregion

		#endregion
	}

	#region Enum

	/// <summary>
	/// Describes the level of a log message
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		/// Messages that are useful when debugging the application
		/// </summary>
		Debug,

		/// <summary>
		/// Messages that show general information about the application's actions
		/// </summary>
		Information,

		/// <summary>
		/// Messages that informs about something that have gone wrong
		/// </summary>
		Warning,

		/// <summary>
		/// Messages informing that something fatal has happened to the application
		/// </summary>
		Error
	}

	#endregion
}