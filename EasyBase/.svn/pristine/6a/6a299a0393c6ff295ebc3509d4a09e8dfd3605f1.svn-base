using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
    public interface IProgressable
    {
        event ProgressEventHandler Progress;
    }

    public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);

    public class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs(int currentValue, int targetValue, string message)
        {
            CurrentValue = currentValue;
            TargetValue = targetValue;
            Message = message;
        }

        public int CurrentValue
        {
            get;
            private set;
        }

        public int TargetValue
        {
            get;
            private set;
        }

        public string Message { get; set; }

        public int Percent
        {
            get { return 100 * CurrentValue / TargetValue; }
        }
    }
}

