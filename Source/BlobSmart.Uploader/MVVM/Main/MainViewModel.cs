using BlobSmart.GUI;
using System.Linq;
using Microsoft.Win32;
using System.ComponentModel;
using System;
using BlobSmart.Common.Generics;
using System.IO;
using System.Collections.Generic;

namespace BlobSmart.Uploader
{
    public class MainViewModel : NotifyBase<MainViewModel>
    {
        private HashSet<string> fileNames = new HashSet<string>();
        private bool? canSelectAll = false;

        private string initialFolder;
        private bool isLoggedOn;
        private bool isUploading;

        public event EventHandler<NotifyArgs> OnLogon;
        public event EventHandler<GenericArgs<string>> OnFolderChanged;

        public MainViewModel(string initialFolder)
        {
            this.initialFolder = initialFolder;

            UploadInfos = new BindingList<UploadInfo>();
        }

        public BindingList<UploadInfo> UploadInfos { get; }

        public string Title
        {
            get
            {
                return Global.AppInfo.GetTitle();
            }
        }

        public bool? CanSelectAll
        {
            get
            {
                if (UploadInfos.Count == 0)
                    return false;
                else
                    return canSelectAll;
            }
            set
            {
                canSelectAll = value;

                if (value == true)
                    UploadInfos.ToList().ForEach(di => di.Selected = true);
                else if (value == false)
                    UploadInfos.ToList().ForEach(di => di.Selected = false);

                NotifyPropertyChanged(vm => vm.CanSelectAll);
            }
        }

        public bool IsLoggedOn
        {
            get
            {
                return isLoggedOn;
            }
            private set
            {
                isLoggedOn = value;

                NotifyPropertyChanged(vm => vm.IsLoggedOn);
            }
        }

        public bool IsUploading
        {
            get
            {
                return isUploading;
            }
            private set
            {
                isUploading = value;

                NotifyPropertyChanged(vm => vm.IsUploading);
                NotifyPropertyChanged(vm => vm.DeleteCommand);
                NotifyPropertyChanged(vm => vm.ClearCommand);
                NotifyPropertyChanged(vm => vm.StartCommand);
            }
        }

        private void UpdateCanSelectAll()
        {
            var canFetchCount = UploadInfos.Count(di => di.Selected);

            if (canFetchCount == 0)
                canSelectAll = false;
            else if (canFetchCount == UploadInfos.Count)
                canSelectAll = true;
            else
                canSelectAll = null;

            NotifyPropertyChanged(vm => vm.CanSelectAll);
            NotifyPropertyChanged(vm => vm.StartCommand);
        }

        public DelegateCommand AddCommand
        {
            get
            {
                return new DelegateCommand(
                    () =>
                    {
                        var dialog = new OpenFileDialog()
                        {
                            CheckFileExists = true,
                            Filter = "Any Files (*.*)|*.*",
                            InitialDirectory = initialFolder,
                            Multiselect = true,
                            ValidateNames = true,
                            Title = "Select One or More FIles To Upload"
                        };

                        if (dialog.ShowDialog() == true)
                        {
                            foreach (var fileName in dialog.FileNames)
                            {
                                if (fileNames.Contains(fileName))
                                    continue;

                                fileNames.Add(fileName);

                                var uploadInfo = new UploadInfo(fileName);

                                uploadInfo.PropertyChanged += (s, e) => UpdateCanSelectAll();

                                UploadInfos.Add(uploadInfo);
                            }

                            NotifyPropertyChanged(vm => vm.DeleteCommand);
                            NotifyPropertyChanged(vm => vm.ClearCommand);
                            NotifyPropertyChanged(vm => vm.StartCommand);

                            if (OnFolderChanged != null)
                            {
                                initialFolder = Path.GetDirectoryName(dialog.FileName);

                                OnFolderChanged(this, new GenericArgs<string>(initialFolder));
                            }
                        }
                    });
            }
        }

        public DelegateCommand DeleteCommand
        {
            get
            {
                return new DelegateCommand(
                    () =>
                    {
                        for (int i = UploadInfos.Count - 1; i >= 0; i--)
                        {
                            var uploadInfo = UploadInfos[i];

                            if (uploadInfo.Selected)
                            {
                                fileNames.Remove(uploadInfo.FileName);

                                UploadInfos.RemoveAt(i);
                            }
                        }

                        NotifyPropertyChanged(vm => vm.DeleteCommand);
                        NotifyPropertyChanged(vm => vm.ClearCommand);
                        NotifyPropertyChanged(vm => vm.StartCommand);

                        UpdateCanSelectAll();
                    },
                    () => (!IsUploading) && (UploadInfos.Count > 0));
            }
        }

        public DelegateCommand ClearCommand
        {
            get
            {
                return new DelegateCommand(
                    () =>
                    {
                        fileNames.Clear();

                        UploadInfos.Clear();

                        NotifyPropertyChanged(vm => vm.DeleteCommand);
                        NotifyPropertyChanged(vm => vm.ClearCommand);
                        NotifyPropertyChanged(vm => vm.StartCommand);

                        UpdateCanSelectAll();
                    },
                    () => (!IsUploading) && (UploadInfos.Count > 0));
            }
        }

        public DelegateCommand LogonCommand
        {
            get
            {
                return new DelegateCommand(
                    () =>
                    {
                        RaiseNotifyEvent(OnLogon);

                        IsLoggedOn = true;
                    });
            }
        }

        public DelegateCommand LogoffCommand
        {
            get
            {
                return new DelegateCommand(
                    () =>
                    {
                        IsLoggedOn = false;
                    });
            }
        }

        public DelegateCommand StartCommand
        {
            get
            {
                return new DelegateCommand(
                    () =>
                    {
                        IsUploading = true;
                    },
                    () => CanUpload);
            }
        }

        public DelegateCommand StopCommand
        {
            get
            {
                return new DelegateCommand(
                    () =>
                    {
                        IsUploading = false;
                    });
            }
        }

        private bool CanUpload
        {
            get
            {
                return UploadInfos.Any(ui => ui.Selected);
            }
        }
    }
}
