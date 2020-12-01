using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace ManuBot
{
    static class ProcessManager
    {
        #region Enums

        /// <summary>
        /// Enumerazione per la funzione User32::ShowWindow
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        /// </summary>
        public enum SW
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL,
            SW_SHOWMINIMIZED,
            SW_SHOWMAXIMIZED,
            SW_SHOWNOACTIVATE,
            SW_SHOW,
            SW_MINIMIZE,
            SW_SHOWMINNOACTIVE,
            SW_SHOWNA,
            SW_RESTORE,
            SW_SHOWDEFAULT,
            SW_FORCEMINIMIZE
        }

        public enum KeyEvents
        {
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105,
        };

        public enum MouseEvents
        {
            WM_MOVE = 0x0200,
            WM_KEYDOWN = 0x0201,
            WM_KEYUP = 0x0202,
            WM_KEYCLK = 0x203
        };


        public enum KeyCodes
        {
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_END = 0x23,
            VK_HOME = 0x24,

            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,

            VK_CANCEL = 0x03,
            VK_BACK = 0x08,
            VK_TAB = 0x09,
            VK_RETURN = 0x0D,
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_PAUSE = 0x13,
            VK_CAPITAL = 0x14,
            VK_ESCAPE = 0x1B,
            VK_SPACE = 0x20,
            VK_SNAPSHOT = 0x2C,
            VK_INSERT = 0x2D,
            VK_DELETE = 0x2E,
            VK_LWIN = 0x5B,
            VK_RWIN = 0x5C,
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 0x64,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_MULTIPLY = 0x6A,
            VK_ADD = 0x6B,
            VK_SUBTRACT = 0x6D,
            VK_DECIMAL = 0x6E,
            VK_DIVIDE = 0x6F,
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7A,
            VK_F12 = 0x7B,
            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            VK_ALT = 0x12,

            // Regular

            VK_0 = 0x30,
            VK_1 = 0x31,
            VK_2 = 0x32,
            VK_3 = 0x33,
            VK_4 = 0x34,
            VK_5 = 0x35,
            VK_6 = 0x36,
            VK_7 = 0x37,
            VK_8 = 0x38,
            VK_9 = 0x39,

            VK_A = 0x41,
            VK_B = 0x42,
            VK_C = 0x43,
            VK_D = 0x44,
            VK_E = 0x45,
            VK_F = 0x46,
            VK_G = 0x47,
            VK_H = 0x48,
            VK_I = 0x49,
            VK_J = 0x4A,
            VK_K = 0x4B,
            VK_L = 0x4C,
            VK_M = 0x4D,
            VK_N = 0x4E,
            VK_O = 0x4F,
            VK_P = 0x50,
            VK_Q = 0x51,
            VK_R = 0x52,
            VK_S = 0x53,
            VK_T = 0x54,
            VK_U = 0x55,
            VK_V = 0x56,
            VK_W = 0x57,
            VK_X = 0x58,
            VK_Y = 0x59,
            VK_Z = 0x5A,

            VK_APPS = 0x5D,
            VK_SLEEP = 0x5F,
            VK_SEPERATOR = 0x6C,
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,
            VK_LCONTROL = 0xA2,
            VK_RCOONTROL = 0xA3,
            VK_LMENU = 0xA4,
            VK_RMENU = 0xA5,
        }

        #endregion


        #region Processes Variables for easy maintainability 

        private const string ClientProcessName = "NostaleClientX"; //.exe not included
        public static int Delay { get => 550; }

        #endregion

        #region Windows Management Methods

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(CallbackDef lpEnumFunc, IntPtr lParam);

        private delegate bool CallbackDef(int hWnd, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(int hWnd, StringBuilder title, int size);


        //To-Fix: not working
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, int lParam);

        public async static Task<bool> SendKey(IntPtr hwnd, KeyCodes key, int Delay = 0)
        {
            PostMessage(hwnd, (uint)KeyEvents.WM_KEYDOWN, (char)key, 1);
            await Task.Delay(Delay);
            PostMessage(hwnd, (uint)KeyEvents.WM_KEYUP, (char)key, 0);

            return true;
        }
        public static void SendClick(IntPtr hwnd, int x, int y)
        {
            PostMessage(hwnd, (uint)MouseEvents.WM_MOVE, 0, MakeLParam(x, y));

            PostMessage(hwnd, (uint)MouseEvents.WM_KEYDOWN, 1, MakeLParam(x, y));

            PostMessage(hwnd, (uint)MouseEvents.WM_KEYCLK, 1, MakeLParam(x, y));
            
            PostMessage(hwnd, (uint)MouseEvents.WM_KEYUP, 0, MakeLParam(x, y));
            
            PostMessage(hwnd, (uint)MouseEvents.WM_MOVE, 0, MakeLParam(0, 0));
        }

        private static int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }


        //Nuove aggiunte 03/05 [Image Search]

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Chiede all'handler delle Top Level Windows le misure della finestra in px
        /// </summary>
        /// <param name="hwnd">Handle della finestra</param>
        /// <param name="lpRect">Struttura RECT che restituisce la finestra</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetClientRect(IntPtr hwnd, out RECT lpRect);

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        public static Bitmap CaptureWindow(IntPtr hwnd)
        {
            RECT wRect = new RECT();

            GetClientRect(hwnd, out wRect);


            Bitmap screenBmp = new Bitmap(wRect.Right, wRect.Bottom);
            Graphics g = Graphics.FromImage(screenBmp);

            IntPtr dc1 = GetDC(hwnd);
            IntPtr dc2 = g.GetHdc();

            //Main drawing, copies the screen to the bitmap
            //last number is the copy constant

            //Right as Width & Bottom as Heigth
            BitBlt(dc2, 0, 0, wRect.Right, wRect.Bottom, dc1, 0, 0, 13369376);

            //Clean up
            ReleaseDC(hwnd, dc1);
            g.ReleaseHdc(dc2);
            g.Dispose();

            return screenBmp;

        }

        /// <summary>
        /// Ritorna un array di interi corrispondente alla BitMap passata per parametro (molto utile per fare una comparazione veloce)
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static int[][] GetPixelArray(Bitmap bitmap)
        {
            var result = new int[bitmap.Height][];
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            for (int y = 0; y < bitmap.Height; ++y)
            {
                result[y] = new int[bitmap.Width];
                Marshal.Copy(bitmapData.Scan0 + y * bitmapData.Stride, result[y], 0, result[y].Length);
            }

            bitmap.UnlockBits(bitmapData);

            return result;
        }



        #endregion



        #region Pixel Search

        public static int GetPixelColor(int[][] PixelArray, int x, int y) => PixelArray[y][x];

        public static Point? Find(Bitmap haystack, Bitmap needle)
        {
            if (null == haystack || null == needle)
            {
                return null;
            }
            if (haystack.Width < needle.Width || haystack.Height < needle.Height)
            {
                return null;
            }

            var haystackArray = GetPixelArray(haystack);
            var needleArray = GetPixelArray(needle);

            foreach (var firstLineMatchPoint in FindMatch(haystackArray.Take(haystack.Height - needle.Height), needleArray[0]))
            {
                if (IsNeedlePresentAtLocation(haystackArray, needleArray, firstLineMatchPoint, 1))
                {
                    return firstLineMatchPoint;
                }
            }

            return null;
        }

        //private static int[][] GetPixelArray(Bitmap bitmap)
        //{
        //    var result = new int[bitmap.Height][];
        //    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
        //        PixelFormat.Format32bppArgb);

        //    for (int y = 0; y < bitmap.Height; ++y)
        //    {
        //        result[y] = new int[bitmap.Width];
        //        Marshal.Copy(bitmapData.Scan0 + y * bitmapData.Stride, result[y], 0, result[y].Length);
        //    }

        //    bitmap.UnlockBits(bitmapData);

        //    return result;
        //}

        private static IEnumerable<Point> FindMatch(IEnumerable<int[]> haystackLines, int[] needleLine)
        {
            var y = 0;
            foreach (var haystackLine in haystackLines)
            {
                for (int x = 0, n = haystackLine.Length - needleLine.Length; x < n; ++x)
                {
                    if (ContainSameElements(haystackLine, x, needleLine, 0, needleLine.Length))
                    {
                        yield return new Point(x, y);
                    }
                }
                y += 1;
            }
        }

        private static bool ContainSameElements(int[] first, int firstStart, int[] second, int secondStart, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                if (first[i + firstStart] != second[i + secondStart])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsNeedlePresentAtLocation(int[][] haystack, int[][] needle, Point point, int alreadyVerified)
        {
            //we already know that "alreadyVerified" lines already match, so skip them
            for (int y = alreadyVerified; y < needle.Length; ++y)
            {
                if (!ContainSameElements(haystack[y + point.Y], point.X, needle[y], 0, needle[y].Length))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion



        private static Random _Random = new Random();
        public static int Random(int minInclusivo, int maxInclusivo) => _Random.Next(minInclusivo, maxInclusivo + 1);
        public static Process[] NostaleProcesses { get => Process.GetProcessesByName(ClientProcessName); }
        //public static List<IntPtr> NostaleWindowHandles
        //{
        //    get
        //    {
        //        List<IntPtr> hWnds = new List<IntPtr>();
        //        foreach (var p in NostaleProcesses)
        //            hWnds.Add(p.MainWindowHandle);
        //        return hWnds;
        //    }
        //}

        public static List<IntPtr> NostaleWindowHandles { get { return UpdateHandlesList(); } }

        private static List<IntPtr> _NostaleWindowHandles = new List<IntPtr>();

        private static List<IntPtr> UpdateHandlesList()
        {
            _NostaleWindowHandles.Clear();
            CallbackDef callback = new CallbackDef(ShowWindowHandler); //Definiamo un callback del tipo bool(int, int)
            EnumWindows(callback, IntPtr.Zero); //EnumWindows itera tutte le finestre e le passa al callback
                                                //Nel callback iteriamo le finestre passate da EnumWindows, le filtriamo e le aggiungiamo alla lista interna

            return _NostaleWindowHandles;
        }

        private static bool ShowWindowHandler(int hWnd, int lParam)
        {
            StringBuilder stringBuilder = new StringBuilder(255);
            GetWindowText(hWnd, stringBuilder, 255);
            string text = stringBuilder.ToString();
            if (text.Contains("NosTale"))
            {
                _NostaleWindowHandles.Insert(0, new IntPtr(hWnd));
                SetWindowText((IntPtr)hWnd, "NosTale - (" + hWnd.ToString() + ")");
            }
            return true;
        }





        public static void Minimize(IntPtr hWnd)
        {
            ShowWindow(hWnd.ToInt32(),(int)SW.SW_MINIMIZE);
        }

        public static void Show(IntPtr hWnd, bool Active = false)
        {
            if (Active)
                ShowWindow(hWnd.ToInt32(), (int)SW.SW_SHOW);
            else
                ShowWindow(hWnd.ToInt32(), (int)SW.SW_SHOWNOACTIVATE);
        }
    }

}
