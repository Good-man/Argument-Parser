using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace ConsoleApp.Usage
{
    internal class CommandLineUtil
    {
        [DllImport("shell32.dll", SetLastError = true)]
        internal static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static string[] Split(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
                return Enumerable.Empty<string>().ToArray();

            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);
            if (argv == IntPtr.Zero)
                throw new Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }
                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }
    }
}