using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XNotes.ViewModel
{
    class AddNewViewModel : INotifyPropertyChanged
    {
        public AddNewViewModel(INavigation navigation)
        {
            SaveItemCommand = new Command(async () => await Save_Item());
            Navigation = navigation;

            
        }
        public AddNewViewModel(INavigation navigation, Object selectedNote)
        {
            SaveItemCommand = new Command(async () => await Save_Item());
            this.selectedNote = (Model.Note)selectedNote;
            Navigation = navigation;
            title = this.selectedNote.title;
            message = this.selectedNote.message;
            onPropertyChanged("Title");
            onPropertyChanged("Message");


        }
        Model.Note selectedNote = null;
        String title = "";
        String message = "";
        List<String> titleTags = new List<string>();
        List<String> messageTags = new List<string>();
        bool isGroup = false;
        bool isAdvanced = false;
        public Command SaveItemCommand { get ;}
        public INavigation Navigation;

        public String Title { 
            get { return title; }
            set 
            { 
                title = value;
                //findTags(value, "Title");
            } 
        }
        public String Message { 
            get { return message; } 
            set { 
                message = value;
                //findTags(value, "Message");
            } 
        }

        public bool IsAdvanced {
            get { return isAdvanced; } 
            set
            {
                isAdvanced = value;
                onPropertyChanged("IsAdvanced");
            }
        }

        public bool IsGroup { 
            get { return isGroup; } 
            set 
            {
                isGroup = value;
                onPropertyChanged("IsGroup");
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void onPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        void preformRealmWrite(List<String> tags)
        {
            RealmConfigurationBase config = new RealmConfiguration("xnotes.1.realm");
            Realm realm = Realm.GetInstance(config);
            Model.Note note = new Model.Note(title, message, tags);
            note.tagLine = note.createTagLabel(tags);
            if(selectedNote == null)
            {
                realm.Write(() =>
                {
                    realm.Add(note);
                });
            }
            else
            {
                note.id = selectedNote.id;
                realm.Write(() =>
                {
                    realm.Add(note, update: true);
                });
            }
        }
        void writeNewNote(List<String> tags)
        {
            RealmConfigurationBase config = new RealmConfiguration("xnotes.1.realm");
            Realm realm = Realm.GetInstance(config);
            Model.Note note = new Model.Note(title, message, tags);
            note.tagLine = note.createTagLabel(tags);
            Console.WriteLine($"TAGLINE{note.tagLine}");
            realm.Write(() =>
            {
                realm.Add(note);
            });
        }

        public void findTags(string value,String type)
        {
            Regex regex = new Regex(@"#(\w+)");
                if (type == "Title") titleTags = new List<string>();
                else messageTags = new List<string>();
                foreach (Match group in regex.Matches(value))
                {
                    if (type == "Title") titleTags.Add(group.Value);
                    else messageTags.Add(group.Value);

                }
        }

        public async Task Save_Item()
        {

            findTags(title, "Title");
            findTags(message, "Message");
            Console.ForegroundColor = ConsoleColor.Green;
            List<String> tags = titleTags.Union(messageTags).ToList();

            preformRealmWrite(tags);

            await Navigation.PopAsync();
        }
    }
}
