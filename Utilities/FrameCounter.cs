namespace TheGame.Utilities
{
    public class FrameCounter
    {
        private readonly int maxCount;
        public int Frame { get; set; }

        public FrameCounter(int maxCount)
        {
            this.maxCount = maxCount;
            Frame = 0;
        }

        public int Increment()
        {
            Frame++;

            if(Frame > maxCount)
            {
                Frame = 0;
            }

            return Frame;
        }

        public int Difference(int previousFrame)
        {
            var difference = Frame - previousFrame;

            if (difference < 0)
            {
                return difference + maxCount;
            }
            return difference;
        }
    }
}
