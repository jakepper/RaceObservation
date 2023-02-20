using Program.Common;
using Program.Entities;

namespace Program.Tests;

public class UnitTests
{
    [Fact]
    public void TestCheatDetector1() // All Cheating Requirements Apply -> True
    {
        var cheatDetector = new CheatDetector();

        var status1 = new RacerStatus() { RacerBibNumber = 1, SensorId = 1, Timestamp = 100 };
        var status2 = new RacerStatus() { RacerBibNumber = 2, SensorId = 1, Timestamp = 1000 };
        var status3 = new RacerStatus() { RacerBibNumber = 1, SensorId = 2, Timestamp = 4321 };
        var status4 = new RacerStatus() { RacerBibNumber = 2, SensorId = 2, Timestamp = 5432 };

        cheatDetector.Process(status1, 1);
        cheatDetector.Process(status2, 2);
        cheatDetector.Process(status3, 1);
        cheatDetector.Process(status4, 2);

        Assert.True(cheatDetector.IsCheating(1) && cheatDetector.IsCheating(2));
    }

    [Fact]
    public void TestCheatDetector2() // Timestamp difference is greater than 3 seconds -> False
    {
        var cheatDetector = new CheatDetector();

        var status1 = new RacerStatus() { RacerBibNumber = 1, SensorId = 1, Timestamp = 100 };
        var status2 = new RacerStatus() { RacerBibNumber = 2, SensorId = 1, Timestamp = 1000 };
        var status3 = new RacerStatus() { RacerBibNumber = 1, SensorId = 2, Timestamp = 4321 };
        var status4 = new RacerStatus() { RacerBibNumber = 2, SensorId = 2, Timestamp = 8000 };

        cheatDetector.Process(status1, 1);
        cheatDetector.Process(status2, 2);
        cheatDetector.Process(status3, 1);
        cheatDetector.Process(status4, 2);

        Assert.False(cheatDetector.IsCheating(1) && cheatDetector.IsCheating(2));
    }

    [Fact]
    public void TestCheatDetector3() // Sensors are NOT consecutive -> False
    {
        var cheatDetector = new CheatDetector();

        var status1 = new RacerStatus() { RacerBibNumber = 1, SensorId = 1, Timestamp = 100 };
        var status2 = new RacerStatus() { RacerBibNumber = 2, SensorId = 1, Timestamp = 1000 };
        var status3 = new RacerStatus() { RacerBibNumber = 1, SensorId = 2, Timestamp = 4321 };
        var status4 = new RacerStatus() { RacerBibNumber = 2, SensorId = 1, Timestamp = 5432 };

        cheatDetector.Process(status1, 1);
        cheatDetector.Process(status2, 2);
        cheatDetector.Process(status3, 1);
        cheatDetector.Process(status4, 2);

        Assert.False(cheatDetector.IsCheating(1) && cheatDetector.IsCheating(2));
    }
}