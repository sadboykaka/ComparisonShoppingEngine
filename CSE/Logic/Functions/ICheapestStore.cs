﻿using System.Collections.Generic;
using Logic.Metadata;
using Logic.Models;

namespace Logic.Functions
{
    interface ICheapestStore<T>
    {
        Store GetCheapestStore(IEnumerable<T> products);
    }
}
