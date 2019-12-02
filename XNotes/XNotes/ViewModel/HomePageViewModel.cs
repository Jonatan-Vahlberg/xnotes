using Realms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XNotes.ViewModel
{
    class HomePageViewModel : INotifyPropertyChanged
    {
        #region Private Properties
        private readonly Realm realm;
        private String searchString = "";
        #endregion

        #region public properties
        public String SearchString 
        { 
            get { return searchString; }
            set
            {
                searchString = value;
                isSearching = (value != "" && value.Length >= 2);
                if (isSearching)
                {
                    preformSearch(value);
                }
                else
                {
                    notes = realm.All<Model.Note>();
                    foundNothing = false;
                    onPropertyChanged("notes");
                    onPropertyChanged("foundNothing");
                }
            }
        }
        public IEnumerable<Model.Note> notes { get; private set; }
        public bool isSearching = false;
        public bool foundNothing = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public Command DeleteItemCommand { get; }
        public Command ViewCellWasTapped { get; }

        public INavigation Navigation;
        #endregion

        #region Constructor
        public HomePageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            RealmConfigurationBase config = new RealmConfiguration("xnotes.1.realm");
            realm = Realm.GetInstance(config);

            notes = realm.All<Model.Note>();

            DeleteItemCommand = new Command(deleteItem);
            ViewCellWasTapped = new Command(viewCellTapped);
        }
        #endregion

        #region Methods and tasks
        void onPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void preformSearch(String value)
        {
            Console.WriteLine($"STARTING SEARCH: {notes.Count()}");
            Regex regex = new Regex(@"^#(\w+)");
            string lowerValue = value.ToLower();
            if (regex.Match(lowerValue).Success)
            {
                notes = realm.All<Model.Note>().Where(note => note.tagLine.Contains(lowerValue,StringComparison.OrdinalIgnoreCase) );
            }
            else
            {
                
                Console.WriteLine($"PASSING SEARCH: {notes.Count()}");
                notes = realm.All<Model.Note>().Where(note => (note.message.Contains(lowerValue,StringComparison.OrdinalIgnoreCase) || note.title.Contains(lowerValue,StringComparison.OrdinalIgnoreCase)));
            }

            foundNothing = (notes.Count() == 0);
            Console.WriteLine($"ENDING SEARCH: {notes.Count()}");
            onPropertyChanged("foundNothing");
            onPropertyChanged("notes");

        }

        public bool findTagInList(List<String> tags,String value)
        {
            bool isFound = false;
            foreach (String tag in tags)
            {
                isFound = tag.ToLower().Contains(value.ToLower());
                if (isFound) return isFound;
            }
            return isFound;
        }

        void deleteItem(Object sender)
        {
            using(var trans = realm.BeginWrite())
            {
                realm.Remove((Model.Note)sender);
                trans.Commit();
                onPropertyChanged("notes");
            }
        }

        public void viewCellTapped(Object sender)
        {
            navigateToUpdate(sender);
        }

        public async Task navigateToUpdate(Object sender)
        {
            await Navigation.PushAsync(new View.AddNewPage(sender));
        }
        #endregion

        
    }
}
