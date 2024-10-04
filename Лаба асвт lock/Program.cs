using System;
using System.Threading;

class Program
{
    static int sharedCounter = 0; // Общий счетчик для всех потоков.
    static readonly object locker = new object(); // Объект для блокировки.

    static void Main()
    {
        // Создание трех потоков, каждый из которых будет увеличивать счетчик.
        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);
        Thread thread3 = new Thread(IncrementCounter);

        // Запуск потоков.
        thread1.Start();
        thread2.Start();
        thread3.Start();

        // Ожидание завершения работы всех потоков.
        thread1.Join();
        thread2.Join();
        thread3.Join();

        // Вывод итогового значения счетчика после работы всех потоков.
        Console.WriteLine($"Final counter value: {sharedCounter}");
    }

    static void IncrementCounter()
    {
        for (int i = 0; i < 400; i++)
        {
            lock (locker) // Блокировка для защиты общего ресурса.
            {
                sharedCounter++; // Безопасное увеличение значения счетчика.
            }
            Thread.Sleep(1); // Искусственная задержка для моделирования работы.
        }
    }
}
