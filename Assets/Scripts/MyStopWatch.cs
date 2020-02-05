using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class MyStopWatch
{
    Stopwatch stopWatch;

    public MyStopWatch(bool start)
    {
        stopWatch = new Stopwatch();
        if (start) {
            stopWatch.Start();
        }
    }

    public MyStopWatch()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();
    }

    public long getElapsedSeconds() {
        return stopWatch.Elapsed.Seconds + (stopWatch.Elapsed.Minutes * 60);
    }

    public long getElapsedMilliseconds() {
        return stopWatch.Elapsed.Milliseconds + (1000 * getElapsedSeconds());
    }

    public bool isRunning() {
        return stopWatch.IsRunning;
    }

    public void stop() {
        stopWatch.Stop();
    }

    public bool execeedesMilliseconds(long milliseconds) {
        if (getElapsedMilliseconds() >= milliseconds) {
            return true;
        }
        return false;
    }

    public bool execeedesSeconds(long seconds) {
        return execeedesMilliseconds(1000 * seconds);
    }
    
}
