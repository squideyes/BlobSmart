using BlobSmart.GUI;
using System;
using System.Windows;

namespace BlobSmart.Uploader
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs sea)
        {
            Current.DispatcherUnhandledException += (s, e) => 
                HandleFailure(e.Exception);

            try
            {
                ShutdownMode = ShutdownMode.OnLastWindowClose;

                MainRunner.Run();
            }
            catch (Exception error)
            {
                HandleFailure(error);
            }
        }

        public void HandleFailure(Exception error)
        {
            // Log the failure

            Modal.FailureDialog("Failure: " + error.Message);

            Shutdown();
        }
    }
}
