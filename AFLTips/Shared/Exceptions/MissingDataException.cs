﻿using System;

namespace AFLTips.Shared.Exceptions
{
    public class MissingDataException : Exception
    {
        public MissingDataException(string message) : base(message)
        {
        }
    }
}
