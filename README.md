static int sharedCounter = 0; // Общий счетчик для всех потоков.

- static int sharedCounter = 0;: Определяет статическую переменную sharedCounter, общую для всех экземпляров класса и всех потоков. Она используется для хранения итогового значения, увеличиваемого потоками.

    static readonly object locker = new object(); // Объект для блокировки.

- static readonly object locker = new object();: Создает объект locker, который будет использоваться для блокировки доступа к sharedCounter, предотвращая конфликт между потоками (состояние гонки).


        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);
        Thread thread3 = new Thread(IncrementCounter);

- Создание трех потоков (thread1, thread2, thread3). Каждый поток будет исполнять метод IncrementCounter.

        thread1.Start();
        thread2.Start();
        thread3.Start();

- Запуск созданных потоков. Каждый поток начинает выполнение метода IncrementCounter.

        thread1.Join();
        thread2.Join();
        thread3.Join();

- Блокирует основной поток выполнения до тех пор, пока thread1, thread2, и thread3 не завершат выполнение. Это необходимо для получения корректного итогового значения sharedCounter.


        Console.WriteLine($"Final counter value: {sharedCounter}");

- Вывод на консоль итогового значения sharedCounter после того, как все потоки завершат работу.

    static void IncrementCounter()
    {

- Определение метода IncrementCounter, выполняемого каждым потоком. Он будет увеличивать sharedCounter.

        for (int i = 0; i < 400; i++)
        {

- Цикл выполняется 400 раз, каждый раз увеличивая значение sharedCounter.

            lock (locker) // Блокировка для защиты общего ресурса.
            {
                sharedCounter++; // Безопасное увеличение значения счетчика.
            }

- lock (locker): Захватывает блокировку на объекте locker, что предотвращает одновременный доступ нескольких потоков к блоку кода, который увеличивает sharedCounter. Это гарантирует, что изменения sharedCounter выполняются потоками поочередно.
- sharedCounter++: Увеливает счетчик sharedCounter на единицу принимая во внимание блокировку, которая защищает этот участок кода от одновременного изменения разными потоками.


            Thread.Sleep(1); // Искусственная задержка для моделирования работы.

- Thread.Sleep(1);: Вводит небольшую задержку (1 миллисекунда) для каждого потока, моделируя некоторую работу и повышая вероятность проявления состояния гонки без механизма блокировки.

Результат выполнения программы:

![image](https://github.com/user-attachments/assets/6d704ef9-f2ac-4d10-b101-69555ac32d43)
