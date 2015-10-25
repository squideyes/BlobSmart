namespace BlobSmart.Uploader
{
    public static class MainRunner
    {
        public static void Run()
        {
            var window = new MainWindow();

            var viewModel = new MainViewModel(
                Properties.Settings.Default.LastFolder);

            viewModel.OnFolderChanged += (s, e) =>
            {
                Properties.Settings.Default.LastFolder = e.Item;

                Properties.Settings.Default.Save();
            };

            viewModel.OnLogon += (s, e) =>
            {
                var uri = LogonRunner.Run(window);

                if (uri != null)
                    viewModel.HandleLogon(uri);
            };

            window.DataContext = viewModel;

            window.Show();
        }
    }
}
