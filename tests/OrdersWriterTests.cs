// ***************************************************************************
// Copyright (c) 2026, Industrial Logic, Inc., All Rights Reserved.
//
// This code is the exclusive property of Industrial Logic, Inc. It may ONLY be
// used by students during Industrial Logic's workshops or by individuals
// who are being coached by Industrial Logic on a project.
//
// This code may NOT be copied or used for any other purpose without the prior
// written consent of Industrial Logic, Inc.
// ****************************************************************************

using System.Text.Json;
using Xunit;

namespace IndustrialLogic.Tests;

public class OrdersWriterTests
{
    private static readonly Product FireTruck =
        new("f1234", "Fire Truck", Color.Red, 8.95m, ProductSize.Medium);

    private static readonly Product ToyConvertible =
        new("p1112", "Toy Porsche Convertible", Color.Pink, 230.0m, ProductSize.NotApplicable);

    [Fact]
    public void NoOrders()
    {
        var orders = new Orders();
        var writer = new OrdersWriter(orders);

        var result = JsonDocument.Parse(writer.GetContents());
        var orderArray = result.RootElement.GetProperty("orders");

        Assert.Equal(0, orderArray.GetArrayLength());
    }

    [Fact]
    public void OneOrderNoProducts()
    {
        var orders = new Orders();
        var order = new Order("321");
        orders.AddOrder(order);

        var writer = new OrdersWriter(orders);
        var result = JsonDocument.Parse(writer.GetContents());
        var firstOrder = result.RootElement.GetProperty("orders")[0];

        Assert.Equal("321", firstOrder.GetProperty("id").GetString());
        Assert.Equal(0, firstOrder.GetProperty("products").GetArrayLength());
    }

    [Fact]
    public void OneOrderOneProduct()
    {
        var orders = new Orders();
        var order = new Order("321");
        order.AddProduct(FireTruck);
        orders.AddOrder(order);

        var writer = new OrdersWriter(orders);
        var result = JsonDocument.Parse(writer.GetContents());
        var product = result.RootElement.GetProperty("orders")[0].GetProperty("products")[0];

        Assert.Equal("f1234", product.GetProperty("id").GetString());
        Assert.Equal("red", product.GetProperty("color").GetString());
        Assert.Equal("medium", product.GetProperty("size").GetString());
        Assert.Equal("Fire Truck", product.GetProperty("name").GetString());
        Assert.Equal(8.95m, product.GetProperty("price").GetProperty("amount").GetDecimal());
        Assert.Equal("USD", product.GetProperty("price").GetProperty("currency").GetString());
    }

    [Fact]
    public void ProductWithDiscount()
    {
        var orders = new Orders();
        var order = new Order("500");
        var discountedTruck = new Product("f1234", "Fire Truck", Color.Red, 8.95m, ProductSize.Medium, 0.1m);
        order.AddProduct(discountedTruck);
        orders.AddOrder(order);

        var writer = new OrdersWriter(orders);
        var result = JsonDocument.Parse(writer.GetContents());
        var product = result.RootElement.GetProperty("orders")[0].GetProperty("products")[0];

        Assert.Equal(8.055m, product.GetProperty("price").GetProperty("amount").GetDecimal());
        Assert.Equal("10%", product.GetProperty("discount").GetString());
    }

    [Fact]
    public void ProductWithNoSizeOmitsSize()
    {
        var orders = new Orders();
        var order = new Order("700");
        order.AddProduct(ToyConvertible);
        orders.AddOrder(order);

        var writer = new OrdersWriter(orders);
        var result = JsonDocument.Parse(writer.GetContents());
        var product = result.RootElement.GetProperty("orders")[0].GetProperty("products")[0];

        Assert.False(product.TryGetProperty("size", out _));
    }
}
