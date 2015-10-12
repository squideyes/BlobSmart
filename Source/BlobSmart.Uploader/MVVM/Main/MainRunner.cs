namespace BlobSmart.Uploader
{
    public static class MainRunner
    {
        public static void Run()
        {
            var model = new MainModel();

            var viewModel = new MainViewModel(model,
                Properties.Settings.Default.LastFolder);

            viewModel.OnFolderChanged += (s, e) =>
            {
                Properties.Settings.Default.LastFolder = e.Item;

                Properties.Settings.Default.Save();
            };

            var view = new MainWindow();

            view.DataContext = viewModel;

            view.Show();
        }
    }
}
