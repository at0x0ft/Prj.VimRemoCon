using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimRemoCon
{
	/// <summary>
    /// キーボード入力のシミュレーションに関するクラス
    /// </summary>
    public static class InputSimulator
    {
        /// <summary>
        /// P/Invoke
        /// </summary>
        private static class NativeMethods
        {
            /// <summary>
            /// 仮想キーコードをスキャンコード、または文字の値（ASCII 値）へ変換します。
            /// また、スキャンコードを仮想コードへ変換することもできます。
            /// </summary>
            /// <param name="wCode">仮想キーコードまたはスキャンコード</param>
            /// <param name="wMapType">実行したい変換の種類</param>
            /// <returns></returns>
            [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
            public extern static int MapVirtualKey(int wCode, int wMapType);

            /// <summary>
            /// キーストロークを合成します。
            /// </summary>
            /// <param name="nInputs"><paramref name="pInputs"/> 配列内の構造体の数を指定します。</param>
            /// <param name="pInputs">INPUT 構造体の配列へのポインタを指定します。構造体はそれぞれキーボードまたはマウス入力ストリームに挿入されるイベントを表します。</param>
            /// <param name="cbsize">INPUT 構造体のサイズを指定します。cbSize パラメータの値が INPUT 構造体のサイズと等しくない場合、関数は失敗します。</param>
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public extern static void SendInput(int nInputs, Input[] pInputs, int cbsize);
        }

        /// <summary>
        /// シミュレートされたキーボードイベントの構造体
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public short VirtualKey;
            public short ScanCode;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        /// <summary>
        /// キーボード以外の入力デバイスによって生成されたシミュレートされたメッセージの構造体
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        /// <summary>
        /// キーストローク、マウスの動き、マウスクリックなどの入力イベントの構造体
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
        public struct Input
        {
            [System.Runtime.InteropServices.FieldOffset(0)]
            public int Type;

            [System.Runtime.InteropServices.FieldOffset(4)]
            public KeyboardInput Keyboard;

            [System.Runtime.InteropServices.FieldOffset(4)]
            public HardwareInput Hardware;
        }

        /// <summary>
        /// キーボード動作の列挙型
        /// </summary>
        public enum KeyboardStroke
        {
            KEY_DOWN = 0x0000,
            KEY_UP = 0x0002
        }

        /// <summary>
        /// KEYEVENTF_UNICODE
        /// </summary>
        private const int KBD_UNICODE = 0x0004;

        /// <summary>
        /// キーボード入力のイベントを追加します。
        /// </summary>
        /// <param name="inputs">入力イベントのリスト</param>
        /// <param name="srcStr">入力する文字列</param>
        public static void AddKeyboardInput(ref System.Collections.Generic.List<Input> inputs, string srcStr)
        {
            if(string.IsNullOrEmpty(srcStr))
            {
                return;
            }

            foreach(char s in srcStr)
            {
                AddKeyboardInput(ref inputs, (int)KeyboardStroke.KEY_DOWN | KBD_UNICODE, 0, (short)s, 0, 0);
                AddKeyboardInput(ref inputs, (int)KeyboardStroke.KEY_UP | KBD_UNICODE, 0, (short)s, 0, 0);
            }
        }

        /// <summary>
        /// キーボード入力のイベントを追加します。
        /// </summary>
        /// <param name="inputs">入力イベントのリスト</param>
        /// <param name="flags">キーボードの動作</param>
        /// <param name="key">入力するキー</param>
        public static void AddKeyboardInput(ref System.Collections.Generic.List<Input> inputs, KeyboardStroke flags, System.Windows.Forms.Keys key)
        {
            int keyboardFlags = (int)flags | KBD_UNICODE;
            short virtualKey = (short)key;
            short scanCode = (short)NativeMethods.MapVirtualKey(virtualKey, 0);

            AddKeyboardInput(ref inputs, keyboardFlags, virtualKey, scanCode, 0, 0);
        }

        /// <summary>
        /// キーボード入力のイベントを追加します。
        /// </summary>
        /// <param name="inputs">入力イベントのリスト</param>
        /// <param name="flags">キーストロークのオプション</param>
        /// <param name="virtualKey">仮想キーコード</param>
        /// <param name="scanCode">キーのハードウェアスキャンコード</param>
        /// <param name="time">ミリ秒単位でのイベントのタイムスタンプ</param>
        /// <param name="extraInfo">キーストロークに関連付けられた付加価値</param>
        public static void AddKeyboardInput(ref System.Collections.Generic.List<Input> inputs, int flags, short virtualKey, short scanCode, int time, int extraInfo)
        {
            Input input = new Input
            {
                Type = 1 // KEYBOARD = 1
            };
			input.Keyboard.Flags = flags;
            input.Keyboard.VirtualKey = virtualKey;
            input.Keyboard.ScanCode = scanCode;
            input.Keyboard.Time = time;
            input.Keyboard.ExtraInfo = extraInfo;

            inputs.Add(input);
        }

        /// <summary>
        /// 入力イベントを実行します。
        /// </summary>
        public static void SendInput(System.Collections.Generic.List<Input> inputs)
        {
            Input[] inputArray = inputs.ToArray();
            SendInput(inputArray);
        }

        /// <summary>
        /// 入力イベントを実行します。
        /// </summary>
        public static void SendInput(Input[] inputs)
        {
            NativeMethods.SendInput(inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
        }
    }
}
