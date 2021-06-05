using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для чтения информации из файлов.
    /// </summary>
    class ReadFileMillionRecords : IButtonListener
    {
        // Создаем списки для новых клиентов и счетов.
        static ObservableCollection<Clients> cl = new ObservableCollection<Clients>();
        static ObservableCollection<Accounts> acc = new ObservableCollection<Accounts>();

        /// <summary>
        /// Метод для прочтения файлов из папки.
        /// </summary>
        /// <param name="path">Путь к файлам.</param>
        /// <returns></returns>
        public static async Task ReadFromFile(string path)
        {
            // Проверяем, чтобы была директория.
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Необходимо создать директорию!");
                return;
            }

            // Записываем все файлы из папки.
            string[] files = Directory.GetFiles(path);

            // Проверяем, чтобы были файлы.
            if (files.Length == 0)
            {
                MessageBox.Show("Необходимо создать файлы");
                return;
            }

            // Сортируем список с файлами.
            var filesOrdered = files.AsParallel().Select(str =>
            {
                string fileName = Path.GetFileNameWithoutExtension(str);
                string[] parts = fileName.Split('_');
                int number = int.Parse(parts.First().Substring(0));
                return new { str, fileName, parts, number };
            })
            .OrderBy(x => x.number)
            .Select(x => x.str)
            .ToArray();

            await DataForList(filesOrdered);

            Fill.client.AddRange(cl);
            Fill.account.AddRange(acc);
        }

        /// <summary>
        /// Метод для записи информации в список.
        /// </summary>
        /// <param name="filesOrdered">Массив с файлами для чтения.</param>
        /// <returns></returns>
        private static async Task DataForList(string[] filesOrdered)
        {
            // Создаем буфер для записи данных из файла.
            Char[] buffer;

            // Создаем масси для записи информации из буфера.
            string[] data;

            // Создаем массив для записи информации из строки.
            string[] dataForList;

            foreach (var files in filesOrdered)
            {
                using (var sr = new StreamReader(files))
                {
                    buffer = new Char[(int)sr.BaseStream.Length];
                    await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
                }

                // Записываем инфомрацию из буфера.
                data = (new String(buffer).Split('\n'));

                await Task.Run(() =>
                {
                // Пробегаем по всему файлу.
                foreach (var dataC in data)
                    {
                    // Разделяем строчку.
                    dataForList = dataC.Split(';');

                    // Записываем длинну массива.
                    int da = dataForList.Length;

                    // Добавляем данные в список
                    if (da != 1)
                        {
                        // Заносим счета.
                        if (dataForList[0] == "Счет")
                            {
                                try
                                {
                                    DepositType depositType = (DepositType)Enum.Parse(typeof(DepositType), dataForList[3]);

                                    acc.Add(new Accounts(Guid.Parse(dataForList[1]), Convert.ToDouble(dataForList[2]), depositType));
                                }
                                catch { }
                            }

                        // Заносим клиентов.
                        if (dataForList[0] == "Клиент")
                            {
                                ClientsType clientType = (ClientsType)Enum.Parse(typeof(ClientsType), dataForList[3]);
                                CreditHistoryType creditHistoryType = (CreditHistoryType)Enum.Parse(typeof(CreditHistoryType), dataForList[4]);

                                cl.Add(new Clients(Guid.Parse(dataForList[1]), dataForList[2], clientType, creditHistoryType));
                            }

                        }
                    }
                });
            }

        }

        /// <summary>
        /// Реализация интерфейса IButtonListener.
        /// </summary>
        public async void ButtonPressed()
        {
            Fill.RemoveDataFromCollection();
            await ReadFromFile("Client");
        }
    }
}
