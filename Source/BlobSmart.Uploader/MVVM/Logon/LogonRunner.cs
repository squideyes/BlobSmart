namespace BlobSmart.Uploader
{
    public static class LogonRunner
    {
        public static bool Run(MainWindow owner, LogonModel model)
        {
            var dialog = new LogonDialog();

            var viewModel = new LogonViewModel(model);

            viewModel.OnCancel += (s, e) => dialog.Close();

            dialog.Owner = owner;

            dialog.DataContext = viewModel;

            return (dialog.ShowDialog() == true);
        }
    }
}
