using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AdditionalLibrary
{
    class AddMillionRecords : IButtonListener
    {
        // Создаем рандом.
        static Random random;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public AddMillionRecords()
        {
            random = new Random();
        }

        public static async Task AutoCreateFile()
        {
            // Переменная для кол-во записей.
            int numOfRecords = 1_000_000;

            // Запсываем кол-во потоков.
            int numThreads = (int)Environment.ProcessorCount;

            // Создаем массив потоков.
            Thread[] threads = new Thread[numThreads];

            // Получаем целое для дальнейшего деления на потоки.
            int chunk = GCD(numOfRecords, numThreads);

            // Переменная для определения диапазона в каждом потоке.
            int zend = (numOfRecords - chunk + (chunk / numThreads)) - (chunk / numThreads);

            // Переменная для записи начала в каждом потоке.
            int start = 0;

            await Task.Run(() =>
            {
                for (int i = 0; i < numThreads; i++)
                {
                    // Создаем переменные для диапазона.
                    int chunkStart = start;
                    int chunkEnd = (i + 1) * (chunk / numThreads) + zend;

                    // Присваиваем значение для следующей итерации.
                    start = chunkEnd;

                    // Присваиваем номер файла с клиентами.
                    int filesClietnNumber = i + 1;

                    // Создаем поток.
                    threads[i] = new Thread(() =>
                    {
                        CreateFileMillionRecords.WritingToFile(chunkStart, chunkEnd, $"Client/{filesClietnNumber}_Clients.txt");
                    });

                    // Стартуем поток.
                    threads[i].Start();
                }


                // Ожидаем завершения.
                foreach (Thread thread in threads)
                {
                    thread.Join();
                }
            });

            // Показываем, что создание файлов завершено.
            MessageBox.Show("Файлы с 1 000 000 млн. записями создались.","Создание файлов.");
        }

        /// <summary>
        /// Находит наибольший делитель.
        /// </summary>
        /// <param name="a">Размер</param>
        /// <param name="b">Кол-во потоков.</param>
        /// <returns>Наибольший делитель.</returns>
        static int GCD(int a, int b)
        {
            while (a != 0)
            {
                var temp = a;
                a = a % b;

                if (a != 0)
                    a = temp - 1;

                if (a == 0)
                {
                    a = temp;
                    break;
                }
            }
            return a;
        }

        /// <summary>
        /// Реализация интерфейса IButtonListener.
        /// </summary>
        public async void ButtonPressed()
        {
            await AutoCreateFile();
        }
    }
}
