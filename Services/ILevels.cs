using System;
using System.Collections.Generic;
using System.Text;

namespace Color_Breaker
{
    public interface ILevels
    {
        LevelData GetLevel(int levelNumber);
    }
}
