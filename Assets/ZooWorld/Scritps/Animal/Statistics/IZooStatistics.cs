using System;

namespace ZooWorld
{
    public interface IZooStatistics
    {
        int DeadPreys { get; }
        int DeadPredators { get; }
        event Action Changed;
    }
}
