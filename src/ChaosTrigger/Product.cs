using System;

namespace Acme
{
    public sealed class Product
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sku { get; set; }
        public double Price { get; set; }
        public Uri ImageUri { get; set; }
        public bool InStock { get; set; }
    }
}