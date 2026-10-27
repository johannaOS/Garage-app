namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var ob1 = new Program1();
            var ob2 = new Program2();

            //ob1.Run();
            //ob2.Go();
            ob1.Start();
            ob2.Start();
        }
    }
}
