using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace BookingYacht.API.Extensions
{
    public static class DistributedExtensionCache
    {
        public static async Task SetRecordAsync<T>(
            this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpiredTime = null,
            TimeSpan? unusedTime = null)
        {
            var option = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiredTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = unusedTime
            };


            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

            // var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, option);
        }

        public static async Task<T> GetRecordAsync<T>(
            this IDistributedCache cache,
            string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            return jsonData is null
                ? default
                : JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}