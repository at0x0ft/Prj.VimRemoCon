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
			this.toolStripMenuItem_Open.Click += this.ToolStripMenuItem_Open_Click;
			this.toolStripMenuItem_Exit.Click += this.ToolStripMenuItem_Exit_Click;
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
			// Shutdown this app.
			Application.Current.Shutdown();
		}
	}
}
