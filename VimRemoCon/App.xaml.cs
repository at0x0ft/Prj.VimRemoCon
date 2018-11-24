using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
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
		/// Mutex which avoid the mutiple starting of this app.
		/// </summary>
		private static Mutex _mutex;

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
			if(CheckHasStarted()) return;

			base.OnStartup(e);
			ShutdownMode = ShutdownMode.OnExplicitShutdown;
			_notifyIcon = new NotifyIconWrapper();
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
			_notifyIcon.Dispose();
		}

		/// <summary>
		/// Check has started this app (with Mutex).
		/// </summary>
		/// <returns></returns>
		private bool CheckHasStarted()
		{
			_mutex = new Mutex(false, "VimRemoCon-{40B84AD3-7AAD-4277-A45B-1804ADE21FE4}");
			if(!_mutex.WaitOne(0, false))
			{
				_mutex.Close();
				_mutex = null;
				Shutdown();
				return true;
			}
			return false;
		}
	}
}
