using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VimRemoCon
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Icon in task tray
		/// </summary>
		private NotifyIconWrapper _notifyIcon;

		/// <summary>
		/// Ignite System.Windows.Application.Startup event.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			this._notifyIcon = new NotifyIconWrapper();
		}

		/// <summary>
		/// Ignite System.Window.Application.Exit event.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnExit(ExitEventArgs e)
		{
			if(KeyboardHook.IsHooking)
			{
				KeyboardHook.Stop();
				System.Diagnostics.Debug.Print("[Debug] : Hook stopped.");	// 4debug
			}

			base.OnExit(e);
			this._notifyIcon.Dispose();
		}
	}
}
