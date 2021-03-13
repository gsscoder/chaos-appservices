using System;
using System.Collections.Generic;
using Bogus;
using CSharpx;
using Acme;

sealed class FakeBackend : IBackend
{
    static Random _random = new CryptoRandom();
    readonly Faker<Product> _faker = new Faker<Product>()
        .StrictMode(true)
        .RuleFor(p => p.ID, f => Guid.NewGuid())
        .RuleFor(p => p.Name, f => f.Vehicle.Model())
        .RuleFor(p => p.Description, f => f.Lorem.Sentence())
        .RuleFor(p => p.Sku, f => _random.Next(100000, 200000))
        .RuleFor(p => p.Price, f => _random.NextDouble() * 100)
        .RuleFor(p => p.ImageUri, f => new Uri(f.Image.LoremFlickrUrl()))
        .RuleFor(p => p.InStock, f => f.PickRandom<bool>(new bool[] { false, true }));
        
    public IEnumerable<Product> GetProducts(int? sku)
    {
        return sku.HasValue
            ? new List<Product>() { _faker.Generate() }
            : _faker.Generate(_random.Next(1, 30));
    }
}