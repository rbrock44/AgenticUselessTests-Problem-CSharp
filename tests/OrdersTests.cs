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

public class OrdersTests
{
    [Fact]
    public void AddOrderIncrementsCount()
    {
        var orders = new Orders();
        Assert.Equal(0, orders.OrderCount);
        orders.AddOrder(new Order("0"));
        Assert.Equal(1, orders.OrderCount);
    }

    [Fact]
    public void OrdersRetainedInOrder()
    {
        var orders = new Orders();
        orders.AddOrder(new Order("0"));
        orders.AddOrder(new Order("1"));
        Assert.Equal("0", orders.GetOrder(0).OrderId);
        Assert.Equal("1", orders.GetOrder(1).OrderId);
    }
}
