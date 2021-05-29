using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для внесения новых клиентов и счетов в файлы.
    /// </summary>
    class CreateFileMillionRecords
    {
        // Создаем рандом.
        static Random random = new Random(Thread.CurrentThread.ManagedThreadId);

        /// <summary>
        /// Метод для создания файла и внесения информации о клиентах и их счетах.
        /// </summary>
        /// <param name="start">Начальная позиция номера клиента.</param>
        /// <param name="end">Конечная позиция номера клиента.</param>
        /// <param name="path"></param>
        public static void WritingToFile(int start, int end, string path)
        {
            // Создаем гуид, для индекса клиенту.
            Guid clientIndex;
            
            // Если директории нет, тогда создаем ее.
            if(!Directory.Exists(path))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    // Записываем клиента.
                    for(int i = start; i < end; i++)
                    {
                        // Записываем клиента в файл.
                        sw.Write($"Клиент нр. {i + 1};{RandomClientsType.RandomType()};{RandomCreditType.RandomType()};\n");
                    }
                }
            }
        }
    }
}
