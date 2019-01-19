using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SortFilesByFiletypeService
{
    public class SortFilesByFiletype
    {
        private readonly Timer _timer;

        // Get the path of the folders from the config file and assign them to vars
        string folderPath = ConfigurationManager.AppSettings["FolderPath"].ToString();
        string NoCategory_FolderPath = ConfigurationManager.AppSettings["NoCategory_FilesFolder"].ToString();
        string EXE_FolderPath = ConfigurationManager.AppSettings["EXE_FilesFolder"].ToString();
        string IMG_FolderPath = ConfigurationManager.AppSettings["IMG_FilesFolder"].ToString();
        string DOCS_FolderPath = ConfigurationManager.AppSettings["DOC_FilesFolder"].ToString();

        public SortFilesByFiletype()
        {
            // The Service runs in a loop every minute
            _timer = new Timer(1*1000) { AutoReset = true };

            // Create the event
            _timer.Elapsed += HandleTimerElapsed;
        }

        // Specify what you want to happen when the Elapsed event is  
        // raised.
        private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Get the folder that will get sorted
            DirectoryInfo folderWithFilesToBeSorted = new DirectoryInfo(folderPath);

            //Get all files and sort them
            foreach (FileInfo file in folderWithFilesToBeSorted.GetFiles())
            {
                string fileExtension = file.Extension;

                if (!Directory.Exists(IMG_FolderPath))
                {
                    Directory.CreateDirectory(IMG_FolderPath);
                }


                // Identify the file as an image
                if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == "jpeg")
                {
                    file.MoveTo($@"{IMG_FolderPath}\{file.Name}");
                }
                // Identify the file as executable
                else if (fileExtension == ".exe" || fileExtension == ".msi")
                {
                    file.MoveTo($@"{EXE_FolderPath}\{file.Name}");
                }
                // Identify the file as document
                else if (fileExtension == ".pdf" || fileExtension == ".docx" || fileExtension == ".doc")
                {
                    file.MoveTo($@"{DOCS_FolderPath}\{file.Name}");
                }
                // Identify the file as uncategorized
                else
                {
                    file.MoveTo($@"{NoCategory_FolderPath}\{file.Name}");
                }

            }

        }

        // Start() should be implemented in a service
        public void Start()
        {
            _timer.Start();
        }

        // Stop() should be implemented in a service
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
