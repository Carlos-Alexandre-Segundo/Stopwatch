using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1;

public class StopwatchTimer
{
    public DateTime _startedAt {get; private set; }
    public TimeSpan _elapsedTime { get; private set; }

    private StopwatchState _state = StopwatchState.Idle;

    public TimeSpan GettingTimeElapsed()
    {
        if (_state == StopwatchState.Running)
        {
           return _elapsedTime + (DateTime.UtcNow - _startedAt);
        }
       return _elapsedTime;
    }
    public void Start()
    {
        if (_state != StopwatchState.Idle)
            return;

        _elapsedTime = TimeSpan.Zero;
        _startedAt = DateTime.UtcNow;
        _state = StopwatchState.Running;
    }
    public void Pause()
    {
        if (_state != StopwatchState.Running)
            return;

        _elapsedTime += DateTime.UtcNow - _startedAt;
        _state = StopwatchState.Paused;
    }
    public void Resume()
    {
        if (_state != StopwatchState.Paused)
            return;

        _startedAt = DateTime.UtcNow;
        _state = StopwatchState.Running;
    }
    public void Reset()
    {
        _elapsedTime = TimeSpan.Zero;
        _startedAt = DateTime.UtcNow;
        _state = StopwatchState.Idle;
    }


    enum StopwatchState
    {
        Running,
        Paused,
        Idle
    }
}