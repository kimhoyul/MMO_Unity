namespace ServerCore
{
	internal class Program
	{
		static void MainThread()
		{
			while (true)
				Console.WriteLine("Hello Thread!");
        }

		static void Main(string[] args)
		{
			Thread thread = new Thread(MainThread);
			thread.Name = "Test Thread";
			thread.Start();
			Console.WriteLine("Waiting for Thread");


			thread.Join();
			Console.WriteLine("Hello World");
		}
	}
}
