using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VimRemoCon
{
	/// <summary>
	/// Notify icon in task tray
	/// </summary>
	public partial class NotifyIconWrapper : Component
	{
		/// <summary>
		/// Initialize NotifyIconWrapper class.
		/// </summary>
		public NotifyIconWrapper()
		{
			InitializeComponent();

			// Subscribe context menu event.
			toolStripMenuItem_Open.Click += ToolStripMenuItem_Open_Click;
			toolStripMenuItem_Exit.Click += ToolStripMenuItem_Exit_Click;

			// Start key hook.
			if(KeyboardHook.IsHooking)
			{
				KeyboardHook.Stop();
				return;
			}

			KeyboardHook.AddEvent(KeyEvent.HookKeyboardTest);
			KeyboardHook.Start();
		}

		/// <summary>
		/// Initialize NotifyIconWrapper class with assigning container.
		/// </summary>
		/// <param name="container"></param>
		public NotifyIconWrapper(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		/// <summary>
		/// Called in selected context menu:"Open".
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItem_Open_Click(object sender, EventArgs e)
		{
			// Generate and show MainWindow
			var wnd = new MainWindow();
			wnd.Show();
		}

		/// <summary>
		/// Called in selected context menu:"Exit".
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
		{
			Debug.Print("[Debug] : NotifyIconWrapper.ToolStripMenuItem_Exit_Click called");	// 4debug

			// Unsubscribe context menu event.
			toolStripMenuItem_Open.Click -= ToolStripMenuItem_Open_Click;
			toolStripMenuItem_Exit.Click -= ToolStripMenuItem_Exit_Click;

			// Shutdown this app.
			Application.Current.Shutdown();
		}
	}
}
