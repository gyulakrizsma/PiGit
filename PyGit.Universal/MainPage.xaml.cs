using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using PiGit.Common;
using PyGit.Universal.Signalr;

namespace PyGit.Universal
{
    public sealed partial class MainPage
    {
        private readonly GitListener _gitListener;
        private static readonly Random _randomize = new Random();

        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            _gitListener = new GitListener();
            _gitListener.MessageReceived += MessageReceived;
        }

        private async void MessageReceived(object sender, GitMessage message)
        {
            var folderName = BitBucketActions.Actions.Single(a => a.Key == message.ActionType).Value;
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, async () =>
                {
                    await PlaySoundFromFolder(folderName);
                }
                );
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await _gitListener.Init();
        }

        private async Task PlaySoundFromFolder(string folderName)
        {
            var path = GetPathToFolder(folderName);

            var folder = await StorageFolder.GetFolderFromPathAsync(path);

            IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();

            PlayRandomSoundInFolder(fileList);
        }

        private void PlayRandomSoundInFolder(IReadOnlyList<StorageFile> fileList)
        {
            var index = _randomize.Next(fileList.Count);
            MyMediaElement.Source = new Uri(fileList[index].Path);
        }

        private static string GetPathToFolder(string folderName)
        {
            var root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            var path = root + string.Format(@"\Assets\GitSounds\{0}", folderName);
            return path;
        }
    }
}
