using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AdditionalLibrary
{
    class ReadFileMillionRecords : IButtonListener
    {
        /// <summary>
        /// Метод для прочтения файлов из папки.
        /// </summary>
        /// <param name="path">Путь к файлам.</param>
        /// <returns></returns>
        public static async Task ReadFromFile(string path)
        {
            // Записываем все файлы из папки.
            string[] files = Directory.GetFiles(path);

            // Создаем буфер для записи данных из файла.
            Char[] buffer;

            foreach (var fileName in files)
            {
                using (var sr = new StreamReader(fileName))
                {
                    buffer = new Char[(int)sr.BaseStream.Length];
                    await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
                }

                await AddToList(new String(buffer).Split('\n'));
            }
        }

        /// <summary>
        /// Метод для записи данных в список.
        /// </summary>
        /// <param name="data">Массив с данными из файла.</param>
        /// <returns></returns>
        private static async Task AddToList(string[] data)
        {
            // Переменная для разделения строки из файла.
            string[] dataForList;

            await Task.Run(() =>
            {
                // Пробегаем по каждой строке файла
                foreach (var dataC in data)
                {
                    // Записываем 
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate
                    {
                        // Разделяем строчку.
                        dataForList = dataC.Split(';');

                        // Записываем длинну массива.
                        int da = dataForList.Length;

                        // Добавляем данные в список
                        if (da != 1)
                        {
                            ClientsType clientType = (ClientsType)Enum.Parse(typeof(ClientsType), dataForList[1]);
                            CreditHistoryType creditHistoryType = (CreditHistoryType)Enum.Parse(typeof(CreditHistoryType), dataForList[2]);

                            Fill.client.Add(new Clients(dataForList[0], clientType, creditHistoryType));
                        }
                    });
                }
            });
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
