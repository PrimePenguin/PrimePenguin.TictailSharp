namespace PrimePenguin.TictailSharp.Infrastructure.Policies
{
    public partial class SmartRetryExecutionPolicy
    {
        private class LeakyBucketState
        {
            public LeakyBucketState(int capacity, int currentFillLevel)
            {
                Capacity = capacity;
                CurrentFillLevel = currentFillLevel;
            }

            public int Capacity { get; }
            public int CurrentFillLevel { get; }
        }
    }
}