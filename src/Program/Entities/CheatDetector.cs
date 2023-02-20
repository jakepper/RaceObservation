using Program.Common;

namespace Program.Entities
{
    public class CheatDetector
    {
        private List<(RacerStatus status, int groupId)> _prevRacers = new();
        private Dictionary<int, Dictionary<int, (int sensorId, int count)>> _possibleCheaters = new(); // <Racers Bib, <Other Racers Bib, (sensorId, count)>>
        private List<int> _cheaters = new(); // <Racers Bib>

        public bool IsCheating(int bib) {
            return _cheaters.Contains(bib);
        }

        public void Process(RacerStatus racerStatus, int groupId)
        {
            // Remove all previous RacerStatus's with a Timestamp difference greater than 3 seconds
            _prevRacers.RemoveAll(prevRacer => (racerStatus.Timestamp - prevRacer.status.Timestamp) > 3000);

            foreach(var prevRacer in _prevRacers)
            {
                // If not same Racer AND Racer's belong to different groups AND same Sensor was passed
                if (racerStatus.RacerBibNumber != prevRacer.status.RacerBibNumber && groupId != prevRacer.groupId && racerStatus.SensorId == prevRacer.status.SensorId)
                {
                    // If racer exists in possible cheaters (already cheated once)
                    if (_possibleCheaters.ContainsKey(racerStatus.RacerBibNumber))
                    {
                        if (_possibleCheaters[racerStatus.RacerBibNumber].ContainsKey(prevRacer.status.RacerBibNumber))
                        {
                            // If Sensor's are consecutive (cheated again)
                            if (Math.Abs(racerStatus.SensorId - _possibleCheaters[racerStatus.RacerBibNumber][prevRacer.status.RacerBibNumber].sensorId) == 1)
                            {
                                if (!_cheaters.Contains(racerStatus.RacerBibNumber)) _cheaters.Add(racerStatus.RacerBibNumber);
                                if (!_cheaters.Contains(prevRacer.status.RacerBibNumber)) _cheaters.Add(prevRacer.status.RacerBibNumber);
                            }
                            else
                            {
                                _possibleCheaters[racerStatus.RacerBibNumber].Remove(prevRacer.status.RacerBibNumber);
                            }
                        }
                        else
                        {
                            _possibleCheaters[racerStatus.RacerBibNumber] = new Dictionary<int, (int sensorId, int count)>
                            {
                                { prevRacer.status.RacerBibNumber, (racerStatus.SensorId, 1) }
                            };
                        }
                        
                    }
                    else
                    {
                        _possibleCheaters[racerStatus.RacerBibNumber] = new Dictionary<int, (int sensorId, int count)>
                        {
                            { prevRacer.status.RacerBibNumber, (racerStatus.SensorId, 1) }
                        };
                    }
                }
            }
            
            _prevRacers.Add((racerStatus, groupId));
        }
    }
}