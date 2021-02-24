using System.Collections.Generic;
using Acme;

interface IBackend
{
    IEnumerable<Product> GetProducts(int? sku);
}