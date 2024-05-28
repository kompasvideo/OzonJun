namespace Route256_2_Demo
{
    public class TransientService
    {
        public int Count { get; private set; }

        public void AddOne()
        {
            Count++;
        }
    }
    
    public class ScopedService
    {
        public int Count { get; private set; }

        public void AddOne()
        {
            Count++;
        }
    }
    
    public class SingltonService
    {
        public int Count { get; private set; }

        public void AddOne()
        {
            Count++;
        }
    }
}