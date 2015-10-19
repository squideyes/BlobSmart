namespace BlobSmart.Uploader
{
    public static class MainRunner
    {
        public static void Run()
        {
            var view = new MainWindow();

            var viewModel = new MainViewModel(
                Properties.Settings.Default.LastFolder);

            viewModel.OnFolderChanged += (s, e) =>
            {
                Properties.Settings.Default.LastFolder = e.Item;

                Properties.Settings.Default.Save();
            };

            viewModel.OnLogon += (s, e) =>
            {
                LogonRunner.Run(view);
            };

            view.DataContext = viewModel;

            view.Show();
        }
    }
}
