
using System.Collections.Generic;

public class Player
{
    private readonly IntVar _healthPoints;

    public Player(IntVar healthPoints, IEnumerable<Block[]> blockPools)
    {
        _healthPoints = healthPoints;
        foreach (var pool in blockPools) {
            foreach (var block in pool) {
                block.blockTouchedKillZone += TakeHealthPoint;
            }
        }
    }

    private void TakeHealthPoint(Block block)
    {
        _healthPoints.SetValue(_healthPoints.GetValue()-1);
    }
}
