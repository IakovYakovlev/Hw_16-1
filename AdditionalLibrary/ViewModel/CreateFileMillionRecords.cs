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
        /// <summary>
        /// Метод для создания файла и внесения информации о клиентах и их счетах.
        /// </summary>
        /// <param name="start">Начальная позиция номера клиента.</param>
        /// <param name="end">Конечная позиция номера клиента.</param>
        /// <param name="path"></param>
        public static void WritingToFile(int start, int end, string path)
        {
            // Записываем в переменную только папку.
            string folder = path.Split('/')[0];

            // Если директории нет, тогда создаем ее.
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            
            // Гуид для номера клиента.
            Guid g;

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    // Записываем клиента.
                    for(int i = start; i < end; i++)
                    {
                        // Создаем рандомные типы для клиентов и счетов.
                        ClientsType clType = RandomClientsType.RandomType();
                        CreditHistoryType credType = RandomCreditType.RandomType();

                        // Записываем новый-уникальный номер клиента.
                        g = Guid.NewGuid();

                        // Записываем в переменную кол-во счетов у клиента.
                        int numAccounts = StaticRandom.Rand(1, 6);

                        // Записываем клиента в файл.
                        sw.Write($"Клиент;{g};Клиент нр. {i + 1};{clType};{credType}\n");

                        // Сохраняем счета в файл.
                        for(int j = 0; j < numAccounts; j++)
                        {
                            sw.WriteLine($"Счет;{g};{RandomDepositAmount.GetRandomDepositAmount()};{RandomDepositType.RandomType()}");
                        }
                    }
                }
            }
        }
    }
}
