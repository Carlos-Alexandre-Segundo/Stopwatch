using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1;

public class StopwatchTimer
{
    public DateTime StartedAt {get; private set; }
    public TimeSpan ElapsedTime { get; private set; }

    private StopwatchState _state = StopwatchState.Idle;

    public TimeSpan GettingTimeElapsed()
    {
        if (_state == StopwatchState.Running)
        {
           return ElapsedTime + (DateTime.UtcNow - StartedAt);
        }
       return ElapsedTime;
    }
    public void Start()
    {
        if (_state != StopwatchState.Idle)
            return;

        ElapsedTime = TimeSpan.Zero;
        StartedAt = DateTime.UtcNow;
        _state = StopwatchState.Running;
    }
    public void Pause()
    {
        if (_state != StopwatchState.Running)
            return;

        ElapsedTime += DateTime.UtcNow - StartedAt;
        _state = StopwatchState.Paused;
    }
    public void Resume()
    {
        if (_state != StopwatchState.Paused)
            return;

        StartedAt = DateTime.UtcNow;
        _state = StopwatchState.Running;
    }
    public void Reset()
    {
        ElapsedTime = TimeSpan.Zero;
        StartedAt = DateTime.UtcNow;
        _state = StopwatchState.Idle;
    }
    enum StopwatchState
    {
        Running,
        Paused,
        Idle
    }

    public string StateName => _state.ToString();
}