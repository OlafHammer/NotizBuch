using System;
using System.Collections.Generic;
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

namespace NotizBuchClient
{
    /// <summary>
    /// Interaktionslogik für NotesView.xaml
    /// </summary>
    public partial class NotesView : Page
    {
        public Client Client { get; set; }
        public Notes Note { get; set; }

        // Zeigt entweder das Kontextmenu zum erstellen eines neuen Eintrages oder
        // Öffnet einen Eintrag 
        public NotesView(Client client, Notes note = null, bool isCreationModul = false)
        {
            Client = client;
            Note = note;

            InitializeComponent();

            if(isCreationModul)
            {
                SaveButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
                CreateButton.Visibility = Visibility.Visible;
                Author.Text = Client.User;
                CreationTime.Text = "-";
            }
            if(note != null)
            {
                Title.Text = Note.Title;
                Author.Text = Note.Username;
                CreationTime.Text = Note.CreationTime.ToShortDateString();
                Text.Text = Note.Text;
            }
        }

        // Speichert die Änderungen
        private void SaveEntry(object sender, RoutedEventArgs e)
        {
            var newNote = new Notes
            {
                Id = Note.Id,
                Username = Note.Username,
                CreationTime = Note.CreationTime,
                Text = Text.Text,
                Title = Title.Text
            };
            Client.EditNote(newNote);
        }
        
        // Löscht den gesamten Eintrag und entfernt diesen von der Benutzeroberfläche
        private void DeleteEntry(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Client.DeleteNote(Note.Id);
        }

        // Erstellt einen neuen Eintrag
        private void CreateEntry(object sender, RoutedEventArgs e)
        {
            var newNote = new Notes
            {
                Username = Client.User,
                CreationTime = DateTime.UtcNow,
                Text = Text.Text,
                Title = Title.Text
            };
            Client.AddNote(newNote);
        }
    }
}
