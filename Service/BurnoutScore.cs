using LifeRPG.Models;

namespace LifeRPG.Services;

public class BurnoutTracker
{
    public int Score { get; private set; } = 0;

    private const int HeavyPenalty = 2;
    private const int LightPenalty = 1;
    private const int ActiveRestRecovery = -3;
    private const int BurnoutThreshold = 4; // после 2 Heavy подряд без разрядки — тревога

    public void Apply(QuestLoad load)
    {
        Score += load switch
        {
            QuestLoad.Heavy => HeavyPenalty,
            QuestLoad.Light => LightPenalty,
            QuestLoad.ActiveRest => ActiveRestRecovery,
            QuestLoad.PassiveRest => 0, // не лечит
            _ => 0
        };
        if (Score < 0) Score = 0;
    }

    public bool IsBurnoutRisk => Score >= BurnoutThreshold;
}