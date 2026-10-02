using System;
using System.Collections.Generic;
using System.Text;

namespace Code
{
    public class Product
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string Category { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public Image Image { get; set; }
    }
}
