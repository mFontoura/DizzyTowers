
using System.Collections.Generic;

public class Player
{
    private readonly IntVar _healthPoints;
    
    public Player(IntVar healthPoints, IEnumerable<Block[]> blockPools)
    {
        _healthPoints = healthPoints;

        Block.onBlockTouchedKillZone += TakeHealthPoint;
    }

    private void TakeHealthPoint(Block block)
    {
        _healthPoints.SetValue(_healthPoints.GetValue()-1);
    }

    public void Destroy()
    {
        Block.onBlockTouchedKillZone -= TakeHealthPoint;
    }
}
