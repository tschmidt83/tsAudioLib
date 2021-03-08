using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using tsAudioLib.Database;

namespace tsAudioLib
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private AudioEntry m_SelectedLibraryEntry;

        /// <summary>
        /// Wrapper for actual library, provides methods for adding/removing/searching
        /// </summary>
        public AudioLibrary Library { get; private set; } = new AudioLibrary();

        /// <summary>
        /// Currently selected library entry
        /// </summary>
        public AudioEntry SelectedLibraryEntry
        {
            get { return m_SelectedLibraryEntry; }
            set { m_SelectedLibraryEntry = value; RaisePropertyChanged("SelectedLibraryEntry"); }
        }

        /// <summary>
        /// Default (empty) constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            root.DataContext = this;

            // Load library
            if (Library.LoadLibrary() == false)
                MessageBox.Show("Fehler beim Laden der Bibliothek!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// UI callback: window closing, save library
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Library.SaveLibrary();
        }

        /// <summary>
        /// UI callback: create new entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewEntry_Click(object sender, RoutedEventArgs e)
        {
            EditEntryWindow w = new EditEntryWindow();

            if(w.ShowDialog() == true)
            {
                if (w.CreateEntry(out AudioEntry entry))
                    Library.AddEntry(entry);
            }
        }

        /// <summary>
        /// UI callback: edit selected entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditEntry_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedLibraryEntry != null)
            {
                EditEntryWindow w = new EditEntryWindow(SelectedLibraryEntry);

                if (w.ShowDialog() == true)
                {
                    //w.ModifyEntry(ref SelectedLibraryEntry);
                }
            }
        }

        /// <summary>
        /// UI callback: remove selected entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveEntry_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLibraryEntry != null)
            {
                Library.RemoveEntry(SelectedLibraryEntry);
                SelectedLibraryEntry = null;
            }
        }

        /// <summary>
        /// UI callback: copy path of current entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyPath_Click(object sender, RoutedEventArgs e)
        {
            // TODO: move to library class?
            if (SelectedLibraryEntry != null)
            {
                Clipboard.SetText(SelectedLibraryEntry.Path);
                MessageBox.Show("Pfad in die Zwischenablage kopiert.");
            }
        }

        /// <summary>
        /// UI callback: copy file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyFile_Click(object sender, RoutedEventArgs e)
        {
            // TODO: move to library class?
            if (SelectedLibraryEntry != null)
            {
                // Copy file to clipboard
                Clipboard.Clear();
                Clipboard.SetData(DataFormats.FileDrop, new string[] { SelectedLibraryEntry.Path });
                MessageBox.Show("Datei in die Zwischenablage kopiert.");
            }
        }

        #region INotifyPropertyChanged members

        /// <summary>
        /// INotifyPropertyChanged member
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// INotifyPropertyChanged member
        /// </summary>
        /// <param name="prop"></param>
        protected virtual void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion
    }
}
