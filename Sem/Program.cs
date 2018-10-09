using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Sem
{
    public class DisposableSemaphore : IDisposable
    {
        public DisposableSemaphore(int maximumCount, string name)
        {
            _semaphore = new Semaphore(maximumCount, maximumCount, name);
            _semaphore.WaitOne();
        }

        public void Dispose()
        {
            _semaphore.Release();
        }

        private readonly Semaphore _semaphore;
    }

    class Program
    {
        static int Main(string[] args)
        {
            using (new DisposableSemaphore(int.Parse(args[0]), args[1]))
            {
                var process = new Process { StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = string.Join(" ", args.Skip(1)
                            .Prepend("/c")),
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = false,
                    }
                };
                process.OutputDataReceived += (sender, arguments) => Console.WriteLine(arguments.Data);
                if (!process.Start())
                {
                    throw new Exception("Couldn't start process");
                }
                process.BeginOutputReadLine();
                process.WaitForExit();
                return process.ExitCode;
            }
        }
    }
}