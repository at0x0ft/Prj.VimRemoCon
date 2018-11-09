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

		private static VimMode CurrentVimMode { get; set; }

		/// <summary>
		/// キーボード操作時に実行する関数
		/// </summary>
		/// <param name="s">キーボードの状態の構造体</param>
		public static void HookKeyboardTest(ref KeyboardHook.StateKeyboard s)
		{
			CurrentVimMode = VimMode.Normal;

			System.Diagnostics.Debug.Print($"Stroke : {s.Stroke}, Key : {s.Key}, ScanCode : {s.ScanCode}, VimMode : {CurrentVimMode}");	// 4debug
		}

		public static void SetNormal()
		{
			if(CurrentVimMode != VimMode.Normal)
			{
				CurrentVimMode = VimMode.Normal;
			}

			return;
		}
	}
}
