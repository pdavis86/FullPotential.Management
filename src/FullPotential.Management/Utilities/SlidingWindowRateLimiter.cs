using Microsoft.AspNetCore.RateLimiting;

namespace FullPotential.Management.Utilities;

public class SlidingWindowRateLimiter
{
    public const string PolicyName = "SlidingWindow";

    public void Configure(RateLimiterOptions limiterOptions)
    {
        limiterOptions.AddSlidingWindowLimiter(
            PolicyName,
            options =>
            {
                options.PermitLimit = 100;
                options.Window = TimeSpan.FromMinutes(1);
                options.SegmentsPerWindow = 5;
                options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 10;
            });
    }
}
