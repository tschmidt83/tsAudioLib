using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace tsAudioLib
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Collection of library entries. For testing purposes, it resides inside the view. I might move it to a ViewModel if I decide to go totally MVVM...
        /// </summary>
        public ObservableCollection<AudioEntry> Entries { get; private set; } = new ObservableCollection<AudioEntry>();
        
        /// <summary>
        /// Default (empty) constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            root.DataContext = this;

            // Test
            Entries.Add(new AudioEntry()
            {
                Name = "Test-Sample",
                Category = Categories.CategoryType.Effect,
                RecordDate = DateTime.Now
            });
        }
    }
}
