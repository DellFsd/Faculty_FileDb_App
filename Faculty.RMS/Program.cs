using System;
using RMS.Core;

namespace Faculty.RMS
{
    class Program
    {
        static void Main(string[] args)
        {
            var teachers = new TeachersRecord(@"C:\Temp\teachers.bin");
            teachers.BeginApp();
        }
    }
}
