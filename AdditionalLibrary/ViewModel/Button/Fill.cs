using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AdditionalLibrary
{
    public class Fill : IButtonListener
    {
        // Создаем список клиентов.
        public static RangeObservableCollection<Clients> client;

        // Создаем список счетов.
        public static RangeObservableCollection<Accounts> account;

        // Создаем рандом.
        static Random random;

        /// <summary>
        /// Инициализируем объекты.
        /// </summary>
        static Fill()
        {
            client = new RangeObservableCollection<Clients>();
            account = new RangeObservableCollection<Accounts>();
            random = new Random();
        }

        /// <summary>
        /// Метод для автоматического заполнения.
        /// </summary>
        public static void AutoFill()
        {
            // Записываем случайным образом кол-во клиентов.
            int clientCount = random.Next(10, 50);

            // Создаем переменную для кол-во счетов у клиента.
            int accountCount;

            // Добавляем в список новых клиентов.
            for(int i = 0; i < clientCount; i++)
            {
                client.Add(new Clients($"Клиент нр. {i + 1}", RandomClientsType.RandomType(), RandomCreditType.RandomType()));
            }

            // Создаем депозиты у всех клиентов.
            foreach(var cl in client)
            {
                // Случайным образом формируем кол-во счетов.
                accountCount = random.Next(1, 6);

                // Добавляем счета.
                for (int i = 0; i < accountCount; i++)
                {
                    account.Add(new Accounts(cl.Index, RandomDepositAmount.GetRandomDepositAmount(), RandomDepositType.RandomType()));
                }

            }
        }

        /// <summary>
        /// Метод для удаления данных из коллекций.
        /// </summary>
        public static void RemoveDataFromCollection()
        {
            client.Clear();
            account.Clear();
        }

        /// <summary>
        /// Реализация интерфейса IButtonListener.
        /// </summary>
        public void ButtonPressed()
        {
            RemoveDataFromCollection();
            AutoFill();
        }
    }
}
