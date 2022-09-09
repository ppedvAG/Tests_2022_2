using Stammdatenverwaltung.Model;
using Stammdatenverwaltung.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Stammdatenverwaltung.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Mitarbeiter> MitarbeiterList { get; set; }

        public Mitarbeiter SelectedMitarbeiter
        {
            get { return selectedMitarbeiter; }
            set
            {
                selectedMitarbeiter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMitarbeiter)));
            }
        }

        private IRepository _repo = new Data.EfRepository();
        private Mitarbeiter selectedMitarbeiter;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            MitarbeiterList = new ObservableCollection<Mitarbeiter>(_repo.GetAll<Mitarbeiter>().ToList());

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var m = new Mitarbeiter() { Name = "Fred99" };
            MitarbeiterList.Add(m);
            _repo.Add(m);
            _repo.SaveAll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MitarbeiterList.Clear();
            foreach (var item in _repo.GetAll<Mitarbeiter>())
            {
                MitarbeiterList.Add(item);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _repo.SaveAll();

        }
    }
}
