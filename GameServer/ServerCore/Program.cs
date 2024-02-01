namespace ServerCore
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int count = 0;
			while(true)
			{
				count++;
				x = y = r1 = r2 = 0;

				Task t1 = new Task(Thread_1);
				Task t2 = new Task(Thread_2);
				t1.Start();
				t2.Start();

				Task.WaitAll(t1, t2);
				if (r1 == 0 && r2 == 0)
					break;
			}
            Console.WriteLine($"{count}번만에 빠져나옴");
        }
	}
}
