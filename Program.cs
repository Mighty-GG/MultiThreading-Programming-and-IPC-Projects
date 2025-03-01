using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

class IPCExample
{
    static void Main(string[] args)
    {
        if (args.Length > 1 && args[0] == "Child")
        {
            RunChildProcess(args[1]);
        }
        else
        {
            RunParentProcess();
        }
    }


    static void RunParentProcess()
    {
        using (AnonymousPipeServerStream pipeServer = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
        {
            Console.WriteLine($"Parent Process: Pipe handle - {pipeServer.GetClientHandleAsString()}");

            Process childProcess = new Process();
            childProcess.StartInfo.FileName = "dotnet";
            childProcess.StartInfo.Arguments = $"run Child {pipeServer.GetClientHandleAsString()}";
            childProcess.StartInfo.UseShellExecute = true;
            childProcess.Start();

            //writing 'ls' output to the pipe
            /*using (StreamWriter writer = new StreamWriter(pipeServer))
            {
                writer.AutoFlush = true;

                Stopwatch stopwatch = Stopwatch.StartNew();
                writer.WriteLine(new string('A', 10_000_000)); //send 10MB of 'A'
                stopwatch.Stop();

                Console.WriteLine($"Transmission Time: {stopwatch.ElapsedMilliseconds} ms");
                Process lsProcess = new Process();
                lsProcess.StartInfo.FileName = "ls";
                lsProcess.StartInfo.RedirectStandardOutput = true;
                lsProcess.Start();
                writer.Write(lsProcess.StandardOutput.ReadToEnd());
                lsProcess.WaitForExit();
            }

            childProcess.WaitForExit(); */
            
            //writing data to the pipe
            using (StreamWriter writer = new StreamWriter(pipeServer))
            {
                writer.AutoFlush = true;
                writer.WriteLine("Hello from Parent.");
            }

            childProcess.WaitForExit();
        }
    }

    static void RunChildProcess(string pipeHandle)
    {
        try
        {

        
            using (AnonymousPipeClientStream pipeClient = new AnonymousPipeClientStream(PipeDirection.In, pipeHandle))
            using (StreamReader reader = new StreamReader(pipeClient))
            {
                string message = reader.ReadLine();
                Console.WriteLine($"Child Process received: {message}");

                /*Console.WriteLine("Files ending in '.cs':");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.EndsWith(".cs"))
                    {
                        Console.WriteLine(line);
                    }
                }*/
                
                /*Stopwatch stopwatch = Stopwatch.StartNew();
                string message = reader.ReadToEnd();
                stopwatch.Stop();

                Console.WriteLine($"Child Process received: {message.Length} bytes");
                Console.WriteLine($"Processing Time: {stopwatch.ElapsedMilliseconds} ms");*/
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"error: {e.Message}");
        }
    }
}
