using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace Lektion08_WPF_Exercise {

    public class User : INotifyPropertyChanged {

        private int id;
        private string name;
        private int age;
        private int score;

        public User(string data) {
            var L = data.Split(';');
            Id = int.Parse(L[0]);
            Name = L[1];
            Age = int.Parse(L[2]);
            Score = int.Parse(L[3]);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id {
            get {
                return id;
            }
            set {
                id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        public int Age {
            get {
                return age;
            }
            set {
                age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }
        public int Score {
            get {
                return score;
            }
            set {
                score = value;
                NotifyPropertyChanged(nameof(Score));
            }
        }
        public override string ToString() {
            return String.Format(" Name: {0} \n ID: {1}", Name, Id);
        }

        private void NotifyPropertyChanged(string propertyName) {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public partial class MainWindow : Window {

        private List<User> usersList = new List<User>();
        private String[] stringList;

        public MainWindow() {
            InitializeComponent();
        }

        private void FileOpen(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var currentUsers = new List<User>();

            if(openFileDialog.ShowDialog() == true) {
                stringList = File.ReadAllLines(openFileDialog.FileName);
                for(int i = 0; i < stringList.Length; i++) {
                    usersList.Add(new User(stringList[i]));
                }
                currentUsers.AddRange(usersList);
                userListView.ItemsSource = currentUsers;
                gridOuter.DataContext = currentUsers;
                statusLabel.Content = "Users in list: " + currentUsers.Count + "\t" + DateTime.Now;
            }
        }

        private void AppExit(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuAbout(object sender, RoutedEventArgs e) {
            MessageBox.Show("With this application you can view users from text files, now isn't that peculiar", "About the application");
        }

        private void userListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            nameBox.DataContext = userListView.SelectedItem;
            idBox.DataContext = userListView.SelectedItem;
            ageBox.DataContext = userListView.SelectedItem;
            scoreBox.DataContext = userListView.SelectedItem;
        }
    }
}