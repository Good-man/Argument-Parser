using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace ArgumentParser.Internal
{
    internal class CommandLine : LinkedList<Argument>
    {
        private CommandLine(IEnumerable<Argument> arguments) : base(arguments)
        {
        }

        public static string Join(params string[] args)
        {
            return string.Join(" ", UnParser.WithQuotes(args));
        }

        public static string Join(IEnumerable<string> args)
        {
            return string.Join(" ", UnParser.WithQuotes(args));
        }

        public static CommandLine Parse(string commandLine)
        {
            var args = Split(commandLine);
            return Parse(args);
        }

        public static CommandLine Parse(IEnumerable<string> args)
        {
            var arguments = args
                .Select(Argument.Parse)
                .Where(arg => arg != null);
            var commandLine = new CommandLine(arguments);
            foreach (var argument in commandLine.ToArray())
            {
                if (argument.OptionPrefix != OptionPrefix.SingleHyphen
                    || !argument.HasName
                    || argument.Name.Length <= 1) continue;
                var currentNode = commandLine.Find(argument);
                var options = Split(argument);
                foreach (var option in options)
                {
                    Debug.Assert(currentNode != null, "currentNode != null");
                    var newNode = commandLine.AddAfter(currentNode, option);
                    currentNode = newNode;
                }
                commandLine.Remove(argument);
            }
            return commandLine;
        }

        /// <summary>
        /// This method splits an argument with a single dash and 
        /// multiple letters into a separate option for each letter.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns><![CDATA[ IEnumerable<Argument> ]]></returns>
        /// <returns>IEnumerable&lt;Argument&gt;</returns>
        internal static IEnumerable<Argument> Split(Argument argument)
        {
            if (argument.OptionPrefix != OptionPrefix.SingleHyphen)
                throw new ArgumentOutOfRangeException(nameof(argument), argument.OptionPrefix,
                    "The argument must have a single hyphen option prefix.");
            if (!argument.HasName)
                throw new ArgumentOutOfRangeException(nameof(argument), argument.Name,
                    "The argument must have a name in order to be splitable.");
            var arguments = new List<Argument>();
            var name = argument.Name;
            foreach (var letter in name)
            {
                var option = argument.Clone();
                option.Name = letter.ToString();
                arguments.Add(option);
            }
            return arguments;
        }

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

        public override string ToString()
        {
            return Join(this.Select(a => a.ToString()));
        }

        [DllImport("shell32.dll", SetLastError = true)]
        internal static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        internal Argument FindFirst(CommandSpecification commandSpecification)
        {
            return this.FirstOrDefault(argument => argument.Text == commandSpecification.LongName);
        }

        internal Argument FindFirst(ArgumentSpecification argumentSpecification)
        {
            var expectedType = argumentSpecification.MemberInfo.GetMemberType();
            if (argumentSpecification.HasName)
            {
                var argument = this
                    .FirstOrDefault(a =>
                        (argumentSpecification.HasLongName  && a.Name == argumentSpecification.LongName) |
                        (argumentSpecification.HasShortName && a.Name == argumentSpecification.ShortName.ToString()));
                if (argument == null) return null;
                if (argument.HasValue & argument.Is(expectedType)) return argument;
                if (argument.HasValue & !argument.Is(expectedType)) return null;
                
                // Check to see if option name & value were separated by a space. 
                // They would appear as two separate arguments in the list.
                var nextArgument = Find(argument)?.Next?.Value;
                if (nextArgument == null) return argument;

                // If the next argument is value option of the correct type, 
                if (nextArgument.HasValue & nextArgument.Is(expectedType) & !nextArgument.HasName)
                {
                    // Move its value to the current argument 
                    argument.Value = nextArgument.Value;
                    Remove(nextArgument);
                }
                return argument;
            }
            return this
                .Where(a => a.Name == null)
                .Skip(argumentSpecification.Index)
                .FirstOrDefault(a => a.Is(expectedType));
        }

        public IEnumerable<Argument> FindAll(ArgumentSpecification argumentSpecification)
        {
            var type = argumentSpecification.MemberInfo.GetMemberType();
            var expectedType = type;
            if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
            {
                expectedType = type.GetGenericArguments()[0];
            }

            var first = FindFirst(argumentSpecification);
            if (first == null)
                return Enumerable.Empty<Argument>();
            var list = new List<Argument> {first};
            var current = Find(first);
            while (true)
            {
                var next = current?.Next;
                if (next == null)
                {
                    break;
                }
                current = next;
                var argument = current.Value;
                if (argument.HasValue & argument.Is(expectedType)) list.Add(argument);
            }

            return list;
        }
    }
}