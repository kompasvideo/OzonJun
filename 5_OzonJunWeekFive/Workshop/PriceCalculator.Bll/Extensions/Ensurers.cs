﻿using PriceCalculator.Bll.Commands;

namespace PriceCalculator.Bll;

public static class Ensurers
{
    public static CalculateDeliveryPriceCommand EnsureHasGoods(
        this CalculateDeliveryPriceCommand src)
    {
        if (src.Goods.Any())
        {
            throw new GoodsNotFoundException();
        }
        return src;
    }
}