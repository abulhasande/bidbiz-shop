﻿using Basket.Api.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data
{
    public class CacheBasketRepository(IBasketRepository repository, IDistributedCache cache ) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
        {
            await repository.DeleteBasket(userName, cancellation);

            await cache.RemoveAsync(userName, cancellation);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellation);

            if(!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            var basket = await repository.GetBasket(userName,cancellation);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket),cancellation);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation = default)
        {
             await repository.StoreBasket(basket, cancellation);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellation);

            return basket;
        }
    }
}

