
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogApp.Enums;
using WerfLogApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WerfLogApp.ViewModels
{
    public partial class WerfViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _nieuweWerfText; //achterliggend wordt een public NieuweWerfText aangemaakt. 

        //private field
        private ObservableCollection<Werf> _werven;

        //public getter & setter 
        public ObservableCollection<Werf> Werven
        {
            get => _werven;
            set
            {
                _werven = value;
                OnPropertyChanged(); //automatisch updaten UI/logica -> toegankelijk door ObservableObject.
            }
        }

        //constructor met dummy gegevens (tijdelijk)

        public WerfViewModel()
        {
            Werven = new ObservableCollection<Werf>()
            {
                new Werf {Id= "1", WerfStatus=Status.Oranje, Naam = "test1"},
                new Werf {Id= "2", WerfStatus=Status.Oranje, Naam = "test2" },
                new Werf {Id= "3", WerfStatus=Status.Oranje, Naam = "test3" }
            };
        }

        //insert om nieuwe entry vanboven in de lijst te krijgen ipv onder 
        public void WerfAanmaken()
        {
            if (!string.IsNullOrEmpty(NieuweWerfText))
        {
                Werven.Insert(0, new Werf { Id = Guid.NewGuid().ToString(), Naam = NieuweWerfText, WerfStatus = Status.Oranje }); 
                NieuweWerfText = string.Empty;
            }
        }
           

        public void WerfVerwijderen(Werf werf)
        {
            Werven.Remove(werf);
        }



        //aanmaken 
        public Command VoegWerfToeCommand => new Command(WerfAanmaken);
      

        //command voor verwijderen van een werf, dus geen EventHandler.
        //Linkt met databinding in xaml mainpage.
        public Command<Werf> VerwijderCommand => new Command<Werf>(async (werf) =>
        {
            var bevestigVerwijderen = await App.Current.MainPage.DisplayAlert("Bevestigen", "Weet u zeker dat u deze werf wilt verwijderen?", "Ja", "Nee");

            if (bevestigVerwijderen)
            {
                // Voer de verwijderactie uit
                WerfVerwijderen(werf);
            }
        });

        //navigatie notitie 

        public Command<string> NotitieCommand => new Command<string>(TapWerf);
        public async void TapWerf(string werfNaam)
        {
            await Shell.Current.GoToAsync($"{nameof(NotitiePage)}?text={werfNaam}");
        }
     

     

    }
}
 