namespace AnimalList
{
    class Program
    {
        static void Main(string[] args)
        {
            var zoo = new Zoo<Animal>();
            zoo.Start();
        }
    }
}
