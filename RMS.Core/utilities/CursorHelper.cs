using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Core.utilities
{
    public static class CursorHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct POSITION
        {
            public short x;
            public short y;
        }

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", EntryPoint = "SetConsoleCursorPosition", SetLastError = true, CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int SetConsoleCursorPosition(int hConsoleOutput, POSITION dwCursorPosition);

        [DllImport("kernel32.dll", EntryPoint = "FillConsoleOutputCharacter", SetLastError = true,
            CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int FillConsoleOutputCharacter(int hConsoleOutput, byte cCharacter, int nLength,
            POSITION dwWriteCoord, ref int lpNumberOfCharsWritten);

        public static void clrscr()
        {
            int STD_OUTPUT_HANDLE = -11;
            POSITION pos;
            pos.x = pos.y = 0;
            int writtenbytes = 0;
            FillConsoleOutputCharacter(GetStdHandle(STD_OUTPUT_HANDLE), 32, 80 * 25, pos, ref writtenbytes);
        }
        public static void gotoxy(int x, int y)
        {
            int STD_OUTPUT_HANDLE = -11;
            POSITION pos;
            pos.x = (short)x;
            pos.y = (short)y;
            SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);
        }
        public static void FormatScreen(ConsoleColor bgColor, ConsoleColor fgColor)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.Clear();
        }
    }
}
