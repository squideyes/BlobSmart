using System;

namespace BlobSmart.Uploader
{
    public static class LogonRunner
    {
        public static Uri Run(MainWindow owner)
        {
            var dialog = new LogonDialog();

            var model = new LogonModel();

            var viewModel = new LogonViewModel(model);

            dialog.Owner = owner;

            dialog.DataContext = viewModel;

            dialog.OnLoadCompleted += (s, e) =>
            {
                model.Uri = e.Item;

                dialog.Close();
            };

            dialog.ShowDialog();

            return model.Uri;
        }
    }
}
