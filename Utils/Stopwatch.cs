using System;

namespace Gench.Utils
{
    public class Stopwatch
    {
        private DateTime _past;
        private TimeSpan _elapsed;

        public Stopwatch()
        {
        }

        public void Start()
        {
            _past = DateTime.Now;
        }

        public void Stop()
        {
            _elapsed = DateTime.Now - _past;
        }

        public string Elapsed()
        {
            return _elapsed.ToString();
        }

        public TimeSpan ElapsedTimeSpan()
        {
            return _elapsed;
        }
    }
}