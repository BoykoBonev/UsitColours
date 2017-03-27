﻿using System;

namespace UsitColours.Common
{
    public interface ICacheProvider
    {
        object GetValue(string key);

        void InsertWithAbsoluteExpiration(string key, object value, DateTime absoluteExpiration);

        void InsertWithSlidingExpiration(string key, object value, int minutes);
    }
}