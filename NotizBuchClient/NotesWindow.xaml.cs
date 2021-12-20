using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static NotizBuchClient.Notes;

namespace NotizBuchClient
{
    /// <summary>
    /// Interaktionslogik für NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public Brush PrimaryColor = (Brush)new BrushConverter().ConvertFrom("#231f1d");
        public Brush SecondaryColor = (Brush)new BrushConverter().ConvertFrom("#54d7ee");


        public List<Notes> CurrentNotes { get; set; }
        public Client Client { get; set; }


        // Lädt das Notizbuch Fenster und fügt erstmalige Einträge hinzu
        public NotesWindow(Client client)
        {

            Client = client;

            InitializeComponent();

            NotesView.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

            CurrentNotes = Client.GetAllNotes();
            LoadNotesIntoView(CurrentNotes);
            StartUpdateThread();
        }

        // Erstellt einen Timer der alle 300ms RefreshDisplay aufruft 
        public void StartUpdateThread()
        {
            var timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(RefreshDisplay);
            timer.Interval = 300;
            timer.AutoReset = true;
            timer.Start();
        }

        // Lädt die Liste an Einträgen neu, vergleicht diese mit der alten und lädt 
        // die Benutzeroberflächer bei Bedarf neu
        public void RefreshDisplay(object source, ElapsedEventArgs e)
        {
            var dataList = Client.GetAllNotes();

            var changedElements = dataList.Except(CurrentNotes, new NotesComparer()).Concat(CurrentNotes.Except(dataList, new NotesComparer()));
            if (changedElements.Count() > 0)
            {
                Dispatcher.Invoke(() =>
                {
                    LoadNotesIntoView(dataList);
                });
                CurrentNotes = dataList;
            }
        }

        // Lädt eine Liste von sortierten Einträgen neu in die Benutzeroberfläche
        public void LoadNotesIntoView(List<Notes> notes)
        {
            ButtonStack.Children.Clear();
            notes = notes.OrderBy(x => x.CreationTime).ToList();
            foreach (var noteInfo in notes)
            {
                Button noteButton = new Button
                {
                    Content = noteInfo.Title,
                    Tag = noteInfo.Id,
                    Height = 40,
                    Margin = new Thickness(0, 0, 0, 3),
                    Background = PrimaryColor,
                    Foreground = SecondaryColor,
                    BorderBrush = SecondaryColor
                };
                noteButton.Click += SelectNote;
                ButtonStack.Children.Add(noteButton);
            }
        }

        // Öffnet einen Eintrag
        public void SelectNote(object sender, RoutedEventArgs e)
        {
            int noteId = (int)(e.Source as Button).Tag;
            Notes note = Client.GetNote(noteId);
            NotesView view = new NotesView(Client, note);
            NotesView.Navigate(view);
        }

        // Öffnet das Kontext Menu für das Erstellen eines neuen Eintrag
        private void CreateNewNote(object sender, RoutedEventArgs e)
        {
            NotesView view = new NotesView(Client, isCreationModul: true);
            NotesView.Navigate(view);
        }
    }
}
