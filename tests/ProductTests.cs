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

public class ProductTests
{
    private static readonly Product Product =
        new("p1", "Fire Truck", Color.Red, 8.95m, ProductSize.Medium);

    [Fact]
    public void IdReturnsTheId()
    {
        Assert.Equal("p1", Product.Id);
    }

    [Fact]
    public void NameReturnsTheName()
    {
        Assert.Equal("Fire Truck", Product.Name);
    }

    [Fact]
    public void PriceReturnsThePrice()
    {
        Assert.Equal(8.95m, Product.Price);
    }

    [Fact]
    public void ColorReturnsTheColor()
    {
        Assert.Equal(Color.Red, Product.Color);
    }

    [Fact]
    public void SizeReturnsTheSize()
    {
        Assert.Equal(ProductSize.Medium, Product.Size);
    }

    [Fact]
    public void DiscountDefaultsToZero()
    {
        Assert.Equal(0m, Product.Discount);
    }
}
