using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimRemoCon
{
	public class KeyEvent
	{
		/// <summary>
		/// Vim Mode Enum
		/// </summary>
		private enum VimMode
		{
			Normal,
			Insert,
			Visual
		}

		private static VimMode CurrentMode { get; set; }

		/// <summary>
		/// キーボード操作時に実行する関数
		/// </summary>
		/// <param name="s">キーボードの状態の構造体</param>
		public static void HookKeyboardTest(ref KeyboardHook.StateKeyboard s)
		{
			CurrentMode = VimMode.Normal;

			System.Diagnostics.Debug.Print($"Stroke : {s.Stroke}, Key : {s.Key}, ScanCode : {s.ScanCode}, VimMode : {CurrentMode}");	// 4debug
		}

		public static void SetNormal()
		{
			if(CurrentMode != VimMode.Normal)
			{
				CurrentMode = VimMode.Normal;
			}

			return;
		}
	}
}
