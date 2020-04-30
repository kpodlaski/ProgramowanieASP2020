using HorseRace;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace HorseRaceWithQueue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IUI
    {
        String[] horsesNames = { "Star", "Sprakle", "Black Storm", "Froggie", "Flopper" };
        RaceWithContinueFlag race;
        MessageQueue mq = new MessageQueue();
        private int[] horsesPositions;
        //We cannot use race.RaceInProgress as we can still have data in mq 
        private bool raceFinished;

        public int raceDistance { get; set; }

        public MainWindow()
        {
            raceDistance = 1000;
            Horse.SleepInterval = 10;
            horsesPositions = new int[horsesNames.Length];
            InitializeComponent();
            for (int i = 0; i < horsesNames.Length; i++)
            {
                var label = (Label)this.FindName("h" + (i + 1) + "Label");
                label.Content = horsesNames[i];
                var slider = (Slider)this.FindName("h" + (i + 1) + "Slider");
                slider.Maximum = raceDistance;
                horsesPositions[i] = 0;
            }
            race = new RaceWithContinueFlag(raceDistance, horsesNames, this);
        }


        public void message(string v, params object[] p)
        {
            System.Diagnostics.Debug.WriteLine(v, p);
        }

        public void updateHorse(Horse horse)
        {
            //A change to Queue
            mq.addRecord(new Record(horse.Name, horse.Position));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            raceFinished = false;
            race.StartRace();
            Thread dataUpdateWorker = new Thread(dataUpdateTask);
            StartBtn.IsEnabled = false;
            dataUpdateWorker.Start();
            Thread uiUpdateWorker = new Thread(uiUpdateTask);
            uiUpdateWorker.Start();
        }

        private void dataUpdateTask()
        {
            //We do not want to have infinite loop, so RaceInProgress have been added
            while (race.RaceInProgress  || mq.Count()>0)
            {
                Record record = null;
                record = mq.peekRecord();
                if (record != null)
                {
                    int horseId = Array.IndexOf(horsesNames, record.HorseName);
                    horsesPositions[horseId] = record.Position;
                }
                else
                {
                    Thread.Sleep(5);
                }
            }
            raceFinished = true;
        }

        private void uiUpdateTask()
        {
            while (!raceFinished)
            {
                Dispatcher.Invoke(() =>
                {
                    for (int horseId=0; horseId < horsesPositions.Length; horseId++)
                    {
                        var slider = (Slider)this.FindName("h" + (horseId + 1) + "Slider");
                        //It is possible we are showing "old" position if actual is still in mq and haven't been  updated
                        //However this will make no big difference to the users and 
                        //the UI thread should not be blocked - in that case ui could "freeze" 
                        slider.Value = horsesPositions[horseId];
                    }
                });
                //The number is conected with "FPS" for UI refresh rate
                Thread.Sleep(30);
            }
        }
    }

}

