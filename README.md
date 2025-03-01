Multi-Threading and IPC Projects 

This repository demonstrates two projects, one based around Multi-Threading and the other with Inter-process Communication build via a virtual machine. The Multi-Threading component showcases how multiple threads run concurrently while ensuring synchronization using mutexes ad locks to prevent race conditions and deadlocks. The IPC component implements anonymous pipes, where a parent process communicates with a child process by transmitting data through a pipe. Additionally, performance benchmarking is conducted to evaluate data transmission efficiency.

Installation Instructions:
To receive the Multi-Threading source code, download the raw sln file by clicking on it within the repository

To receive the IPC Source code, download the raw Program.cs file by clicking on it within the repository. If you are going to take the steps to run it through a virtual machine, proceed to follow these steps. First, install .NET SDK with the commands: 'sudo apk update' , 'sudo apt install dotnet-sdk-8.0 -y'. Next build and run the project with the commands: 'dotnet build', and 'dotnet run'. It may serve you best to have VSCode install if you want to see the visual code rather than whats being ran. 
