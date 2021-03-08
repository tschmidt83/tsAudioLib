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
using System.Windows.Shapes;

namespace tsAudioLib
{
    /// <summary>
    /// Interaction logic for EditEntryWindow.xaml
    /// </summary>
    public partial class EditEntryWindow : Window, INotifyPropertyChanged
    {
        private string m_Path;
        private Categories.CategoryType m_Category;
        private string m_AudioName;
        private DateTime m_RecordDate;
        private string m_EnumeratedTags;

        /// <summary>
        /// Path/Location of audio file
        /// </summary>
        public string Path
        {
            get { return m_Path; }
            set { m_Path = value; RaisePropertyChanged("Path"); }
        }

        /// <summary>
        /// Audio entry category
        /// </summary>
        public Categories.CategoryType Category
        {
            get { return m_Category; }
            set { m_Category = value; RaisePropertyChanged("Category"); }
        }

        /// <summary>
        /// Audio file name
        /// </summary>
        public string AudioName
        {
            get { return m_AudioName; }
            set { m_AudioName = value; RaisePropertyChanged("AudioName"); }
        }
        
        /// <summary>
        /// Date and time when the audio was recorded/created
        /// </summary>
        public DateTime RecordDate
        {
            get { return m_RecordDate; }
            set { m_RecordDate = value; RaisePropertyChanged("RecordDate"); }
        }
        
        /// <summary>
        /// Enumerated (comma-separated) tags
        /// </summary>
        public string EnumeratedTags
        {
            get { return m_EnumeratedTags; }
            set { m_EnumeratedTags = value; RaisePropertyChanged("EnumeratedTags"); }
        }

        /// <summary>
        /// Available categories
        /// </summary>
        public List<Categories.CategoryType> AvailableCategories { get; private set; } = new List<Categories.CategoryType>();

        /// <summary>
        /// Default (empty) constructor
        /// </summary>
        public EditEntryWindow()
        {
            InitializeComponent();
            PopulateCategories();

            this.DataContext = this;

            AudioName = "Neue Audiodatei";
            RecordDate = DateTime.Now;
        }

        /// <summary>
        /// Constructor which initializes the dialog with a library entry
        /// </summary>
        /// <param name="entry">Entry for initialization</param>
        public EditEntryWindow(AudioEntry entry)
        {
            InitializeComponent();
            PopulateCategories();

            this.DataContext = this;

            if (entry != null)
            {
                Path = entry.Path;
                Category = entry.Category;
                AudioName = entry.Name;
                RecordDate = entry.RecordDate;

                EnumeratedTags = entry.GetTags();
            }
        }

        /// <summary>
        /// Populate the category selection box
        /// </summary>
        private void PopulateCategories()
        {
            foreach (Categories.CategoryType cat in Enum.GetValues<Categories.CategoryType>())
                AvailableCategories.Add(cat);

            Category = AvailableCategories[0];
        }

        /// <summary>
        /// Create a library entry from the dialog entries
        /// </summary>
        /// <param name="entry">(out) Created audio entry</param>
        /// <returns>True if created successful</returns>
        public bool CreateEntry(out AudioEntry entry)
        {
            try
            {
                entry = new AudioEntry();

                entry.Name = AudioName;
                entry.Path = Path;
                entry.RecordDate = RecordDate;
                entry.Category = Category;

                string[] tags = EnumeratedTags.Trim().Split(';');

                if (tags != null && tags.Length > 0)
                {
                    foreach(string s in tags)
                    {
                        if (s.Trim().Length > 0)
                            entry.Tags.Add(s.Trim());
                    }
                }
                else
                {
                    entry.Tags.Clear();
                }

                return true;
            }
            catch
            {
                entry = null;
                return false;
            }
        }

        /// <summary>
        /// Modify an existing library entry with dialog entries
        /// </summary>
        /// <param name="entry">(ref) Existing audio entry</param>
        /// <returns>True if created successful</returns>
        public bool ModifyEntry(ref AudioEntry entry)
        {
            if(entry == null)
            {
                return false;
            }

            try
            {
                entry.Name = AudioName;
                entry.Path = Path;
                entry.RecordDate = RecordDate;
                entry.Category = Category;

                string[] tags = EnumeratedTags.Trim().Split(';');

                if (tags != null && tags.Length > 0)
                {
                    foreach (string s in tags)
                    {
                        if (s.Trim().Length > 0)
                            entry.Tags.Add(s.Trim());
                    }
                }
                else
                {
                    entry.Tags.Clear();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Browse file, and set path according to file's path
        /// </summary>
        private void BrowseFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "Audiodatei auswählen";
            dlg.Filter = "Alle Dateien (*.*)|*.*";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                Path = dlg.FileName;
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

        /// <summary>
        /// UI callback: browse file path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            BrowseFile();
        }

        /// <summary>
        /// UI callback: dialog OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// UI callback: dialog cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
