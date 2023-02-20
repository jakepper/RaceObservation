using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

using Program.Common;
using Program.Entities;

namespace Program
{
    public class Controller
    {
        public const int GROUP_SIZE = 100;

        private readonly DataReceiver _receiver = new();

        private readonly CheatDetector _cheatDetector = new();

        private readonly CheatingScreen _cheatingScreen = new();

        private readonly Dictionary<int, RaceGroup> _raceGroups = new();

        public readonly Dictionary<string, StatusScreen> _screens = new();

        private readonly List<Sensor> _sensors = new();

        public List<Racer> Racers { get; set; } = new();

        public void Setup(string racersFilePath, string groupsFilePath, string sensorsFilePath)
        {
            using (TextFieldParser textFieldParser = new(groupsFilePath))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(",");

                string[]? row;
                while (!textFieldParser.EndOfData)
                {
                    row = textFieldParser.ReadFields();
                    if (row != null)
                    {
                        string name = row[1];
                        int startTime = int.Parse(row[2]);
                        int bibMin = int.Parse(row[3]);
                        int bibMax = int.Parse(row[4]);
                        int groupId = bibMin / GROUP_SIZE;
                        _raceGroups.Add(groupId, new RaceGroup(groupId, name, startTime, bibMin, bibMax));
                    }
                }
            }

            using (TextFieldParser textFieldParser = new(racersFilePath))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(",");

                string[]? row;
                while (!textFieldParser.EndOfData)
                {
                    row = textFieldParser.ReadFields();
                    if (row != null)
                    {
                        string nameFirst = row[0];
                        string nameLast = row[1];
                        int bib = int.Parse(row[2]);
                        var groupId = bib / GROUP_SIZE;
                        var racer = new Racer(groupId, $"{nameFirst} {nameLast}", bib);
                        Racers.Add(racer);
                        _raceGroups[groupId].AddRacer(racer);
                    }
                }
            }

            using (TextFieldParser textFieldParser = new(sensorsFilePath))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(",");

                string[]? row;
                while (!textFieldParser.EndOfData)
                {
                    row = textFieldParser.ReadFields();
                    if (row != null)
                    {
                        int sensorId = int.Parse(row[0]);
                        float location = float.Parse(row[1]);
                        _sensors.Add(new Sensor(sensorId, location));
                    }
                }
            }

            // Cheating Observer Creation //
            foreach (var raceGroup in _raceGroups.Values)
            {
                foreach(var racer in raceGroup.Racers)
                {
                    racer.Subscribe(_cheatingScreen);
                }
            }

            _receiver.Start(HandleStatusMessage);
        }

        public void HandleStatusMessage(RacerStatus statusMessage)
        {
            var racer = GetRacer(statusMessage.RacerBibNumber);
            var sensor = _sensors.Find(sensor => sensor.Id == statusMessage.SensorId);
            if (racer != null)
            {
                _cheatDetector.Process(statusMessage, racer.GroupId);

                racer.Cheating = _cheatDetector.IsCheating(racer.Bib);
                racer.Location = sensor.Location;

                racer.Notify();

                Debug.WriteLine($"Notified Observers of Updated Racer with BIB# {racer.Bib}\n");
            }

            Debug.WriteLine("Cheating Observer");
            Debug.WriteLine("\tBIB#\tNAME");
            _cheatingScreen.Cheaters().ForEach(racer =>
            {
            Debug.WriteLine($"\t{racer.Bib}\t{racer.Name}");
            });
            Debug.WriteLine("");
        }

        public List<Racer> GetSubjects(string screen)
        {
            return _screens[screen].Observing.Values.ToList();
        }

        public List<string> GetObservers()
        {
            var observers = _screens.Keys.ToList();
            observers.Add("Default");

            return observers;
        }

        // Screen Observer Creation //
        public bool CreateScreen(string name, List<Racer> Racers) 
        {
            bool created = true;

            if (_screens.ContainsKey(name))
            {
                created = false;
            }
            else
            {
                var screen = new StatusScreen() { Name = name };
                foreach (var racer in Racers)
                {
                    racer.Subscribe(screen);
                }
                _screens.Add(name, screen);
            }

            return created;
        }

        public void DestroyScreen(string name)
        {
            _screens[name].UnsubscribeAll();
            _screens.Remove(name);
        }

        private Racer? GetRacer(int bib)
        {
            return _raceGroups[bib / GROUP_SIZE].GetRacer(bib);
        }
    }
}
