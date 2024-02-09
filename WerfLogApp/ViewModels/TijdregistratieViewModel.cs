using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WerfLogBl.DTOS;

namespace WerfLogApp.ViewModels
{
    public partial class TijdregistratieViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> maanden;

        [ObservableProperty]
        private string geselecteerdeMaand;

        [ObservableProperty]
        private ObservableCollection<TijdregistratieDto> tijdregistraties;

        public TijdregistratieViewModel()
        {
            Maanden = new ObservableCollection<string> { "Januari 2023", "Februari 2023", "Maart 2023" };
            GeselecteerdeMaand = Maanden[0];

            // Initialiseren van dummy tijdregistraties met fictieve werfnamen
            Tijdregistraties = new ObservableCollection<TijdregistratieDto>
            {
                new TijdregistratieDto { Id = 1, WerfId = 1, StartTijd = new DateTime(2023, 1, 1, 8, 0, 0), EindTijd = new DateTime(2023, 1, 1, 16, 0, 0), WerfNaamRegistratie = "Werf A" },
                new TijdregistratieDto { Id = 2, WerfId = 2, StartTijd = new DateTime(2023, 1, 2, 9, 0, 0), EindTijd = new DateTime(2023, 1, 2, 17, 0, 0), WerfNaamRegistratie = "Werf B" },
                 new TijdregistratieDto { Id = 1, WerfId = 1, StartTijd = new DateTime(2023, 1, 1, 8, 0, 0), EindTijd = new DateTime(2023, 1, 1, 16, 0, 0), WerfNaamRegistratie = "Aaaaaaaaaaaaaaaaa" },
                new TijdregistratieDto { Id = 2, WerfId = 2, StartTijd = new DateTime(2023, 1, 2, 9, 0, 0), EindTijd = new DateTime(2023, 1, 2, 17, 0, 0), WerfNaamRegistratie = "Werf B" },
                 new TijdregistratieDto { Id = 1, WerfId = 1, StartTijd = new DateTime(2023, 1, 1, 8, 0, 0), EindTijd = new DateTime(2023, 1, 1, 16, 0, 0), WerfNaamRegistratie = "Werf A" },
                new TijdregistratieDto { Id = 2, WerfId = 2, StartTijd = new DateTime(2023, 1, 2, 9, 0, 0), EindTijd = new DateTime(2023, 1, 2, 17, 0, 0), WerfNaamRegistratie = "Werf B" },
                 new TijdregistratieDto { Id = 1, WerfId = 1, StartTijd = new DateTime(2023, 1, 1, 8, 0, 0), EindTijd = new DateTime(2023, 1, 1, 16, 0, 0), WerfNaamRegistratie = "Werf A" },
                new TijdregistratieDto { Id = 2, WerfId = 2, StartTijd = new DateTime(2023, 1, 2, 9, 0, 0), EindTijd = new DateTime(2023, 1, 2, 17, 0, 0), WerfNaamRegistratie = "Werf B" },
                 new TijdregistratieDto { Id = 1, WerfId = 1, StartTijd = new DateTime(2023, 1, 1, 8, 0, 0), EindTijd = new DateTime(2023, 1, 1, 16, 0, 0), WerfNaamRegistratie = "Werf A" },
                new TijdregistratieDto { Id = 2, WerfId = 2, StartTijd = new DateTime(2023, 1, 2, 9, 0, 0), EindTijd = new DateTime(2023, 1, 2, 17, 0, 0), WerfNaamRegistratie = "Werf B" },
                 new TijdregistratieDto { Id = 1, WerfId = 1, StartTijd = new DateTime(2023, 1, 1, 8, 0, 0), EindTijd = new DateTime(2023, 1, 1, 16, 0, 0), WerfNaamRegistratie = "Werf A" },
                new TijdregistratieDto { Id = 2, WerfId = 2, StartTijd = new DateTime(2023, 1, 2, 9, 0, 0), EindTijd = new DateTime(2023, 1, 2, 17, 0, 0), WerfNaamRegistratie = "Werf B" },
                 new TijdregistratieDto { Id = 1, WerfId = 1, StartTijd = new DateTime(2023, 1, 1, 8, 0, 0), EindTijd = new DateTime(2023, 1, 1, 16, 0, 0), WerfNaamRegistratie = "Werf A" },
                new TijdregistratieDto { Id = 2, WerfId = 2, StartTijd = new DateTime(2023, 1, 2, 9, 0, 0), EindTijd = new DateTime(2023, 1, 2, 17, 0, 0), WerfNaamRegistratie = "Werf B" }
            };
        }

        //public void OnStartTimeChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(TimePicker.Time))
        //    {
        //        var picker = (TimePicker)sender;
        //        var newTime = picker.Time;
        //        var item = ... // Zoek je TijdregistratieDto object

        //// Update de tijd van je item
        //item.StartTijd = new DateTime(item.StartTijd.Year, item.StartTijd.Month, item.StartTijd.Day, newTime.Hours, newTime.Minutes, 0);

        //        // Vergeet niet om eventuele wijzigingen terug te koppelen naar de UI indien nodig
        //    }
        //}

    }
}


