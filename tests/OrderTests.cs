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

using Xunit;

namespace IndustrialLogic.Tests;

public class OrderTests
{
    private static readonly Product Product =
        new("productID", "firstProduct", Color.White, 9.99m, ProductSize.Large);

    [Fact]
    public void AddProductIncrementsCount()
    {
        var order = new Order("0");
        Assert.Equal(0, order.ProductCount);
        order.AddProduct(Product);
        Assert.Equal(1, order.ProductCount);
    }

    [Fact]
    public void AddProductsRetainsProducts()
    {
        var order = new Order("0");
        order.AddProduct(Product);
        var secondProduct = new Product("productID", "secondProduct", Color.White, 9.99m, ProductSize.Large);
        order.AddProduct(secondProduct);
        Assert.Equal("firstProduct", order.GetProduct(0).Name);
        Assert.Equal("secondProduct", order.GetProduct(1).Name);
    }
}
