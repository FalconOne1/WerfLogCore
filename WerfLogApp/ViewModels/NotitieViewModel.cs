using CommunityToolkit.Mvvm.ComponentModel;
using WerfLogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WerfLogApp.ViewModels
{

    [QueryProperty("Text", "text")]
    public partial class NotitieViewModel : ObservableObject 
    {
        [ObservableProperty]
        string text; //id van werf

        [ObservableProperty]
        private string _nieuweNotitieText; //achterliggend wordt een public NieuweNotitieText aangemaakt. 


        //private field
        private ObservableCollection<Notitie> _notities;

        //public getter & setter 
        public ObservableCollection<Notitie> Notities
        {
            get => _notities;
            set
            {
                _notities = value;
                OnPropertyChanged(); //automatisch updaten UI/logica -> toegankelijk door BindableObject.
            }
        }


        public NotitieViewModel()
        {
            Notities = new ObservableCollection<Notitie>()
            {
                new Notitie {Id= "1", WerfId = "6",Datum = DateTime.Now, Tekst ="Hier kan er wel nog meer gebeuren, het is altijd hetzelfde met deze werf."},
                new Notitie {Id= "2", WerfId = "7",Datum = DateTime.Now, Tekst ="Hier kan er wel nog meer gebeuren, het is altijd hetzelfde met deze werf."},
                new Notitie {Id= "3", WerfId = "8",Datum = DateTime.Now, Tekst ="Hier kan er wel nog meer gebeuren, het is altijd hetzelfde met deze werf."},
            };
        }


        //aanmaken 
        public Command VoegNotitieToeCommand => new Command(NotitieAanmaken);
        public void NotitieAanmaken()
        {
            if (!string.IsNullOrEmpty(NieuweNotitieText))
            {
                Notities.Insert(0,new Notitie { Id = Guid.NewGuid().ToString(), WerfId = Text, Datum = DateTime.Now, Tekst = NieuweNotitieText });
                NieuweNotitieText = string.Empty; // Clear the text after adding
            }
        }

        //verwijderen

        public Command DeleteNotitieCommand => new Command<Notitie>(NotitieVerwijderen);
        public void NotitieVerwijderen(Notitie notitie)
        {
            Notities.Remove(notitie);
        }


        //navigatie
        public async void GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        public Command GoBackCommand => new Command(GoBack);

    
    }
}
