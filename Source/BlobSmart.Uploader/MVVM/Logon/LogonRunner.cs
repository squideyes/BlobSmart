namespace BlobSmart.Uploader
{
    public static class LogonRunner
    {
        public static void Run(MainWindow owner)
        {
            var model = new LogonModel();

            var viewModel = new LogonViewModel(model);

            var dialog = new LogonDialog();

            dialog.Owner = owner;

            dialog.DataContext = viewModel;

            if (dialog.ShowDialog() == true)
            {
            }
        }
    }
}
