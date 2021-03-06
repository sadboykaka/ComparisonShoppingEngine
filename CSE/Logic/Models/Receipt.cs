﻿using Logic.Metadata;
using System;
using System.Collections.Generic;

namespace Logic.Models
{
    public class Receipt
    {
        public IEnumerable<Product> Products { get; private set; }
        public Store StoreName { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Receipt(IEnumerable<Product> products, Store storeName, DateTime timestamp)
        {
            Products = products;
            StoreName = storeName;
            Timestamp = timestamp;
        }
    }
}
