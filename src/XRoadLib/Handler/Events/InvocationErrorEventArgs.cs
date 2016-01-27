﻿using System;

namespace XRoadLib.Handler.Events
{
    public class InvocationErrorEventArgs : EventArgs
    {
        public Exception Exception { get; }
        public object Result { get; set; }

        public InvocationErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}