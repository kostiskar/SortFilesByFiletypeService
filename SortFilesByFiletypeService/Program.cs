using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SortFilesByFiletypeService
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the service, run until it stops and then grab exitCode
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<SortFilesByFiletype>(s =>
                {
                    s.ConstructUsing(sortFilesByFiletype => new SortFilesByFiletype());
                    s.WhenStarted(sortFilesByFiletype => sortFilesByFiletype.Start());
                    s.WhenStopped(sortFilesByFiletype => sortFilesByFiletype.Stop());
                });

                x.RunAsLocalSystem();

                // Configure the 'machine-friendly' name
                x.SetServiceName("SortFilesByFiletypeService");

                // Configure the description
                x.SetDisplayName("This is a service that sorts the files in a folder" +
                                 "according to their extension putting them in folder" +
                                 " with simliar files.");
            });

            // Convert from enum to integer and passing it out
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
