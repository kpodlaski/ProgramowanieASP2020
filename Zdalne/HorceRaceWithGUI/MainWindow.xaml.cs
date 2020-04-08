using HorseRace;
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

namespace HorceRaceWithGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, IUI
    {
        String[] horsesNames = { "Star", "Sprakle", "Black Storm", "Froggie", "Flopper" };
        Race race;
        public int raceDistance { get; set; }



        public MainWindow()
        {
            raceDistance = 1000;
            Horse.SleepInterval = 10;

            InitializeComponent();
            for (int i=0; i<horsesNames.Length; i++)
            {
                var label = (Label)this.FindName("h" + (i + 1) + "Label");
                label.Content = horsesNames[i];
                var slider = (Slider)this.FindName("h" + (i + 1) + "Slider");
                slider.Maximum = raceDistance;
            }
            race = new Race(raceDistance, horsesNames, this);
            
        }

        public void message(string v, params object[] p)
        {
            System.Diagnostics.Debug.WriteLine(v, p);
        }

        public void updateHorse(Horse horse)
        {
            //Get horse index
            Dispatcher.Invoke(() =>
            {
               int horseId = Array.IndexOf(horsesNames, horse.Name);
               //message("Horse {0} has id {1}", horse.Name, horseId);
               var slider = (Slider)this.FindName("h" + (horseId + 1) + "Slider");
               slider.Value = horse.Position;
           });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            race.StartRace();
            StartBtn.IsEnabled = false;
        }
    }
}
