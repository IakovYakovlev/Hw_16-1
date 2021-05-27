using System;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для создания случайного типа кредитной истории.
    /// </summary>
    class RandomCreditType
    {
        // Создаем рандом.
        static Random random = new Random();

        /// <summary>
        /// Метод для случайного типа кредитной истории.
        /// </summary>
        /// <returns>Тип клиента.</returns>
        public static CreditHistoryType RandomType()
        {
            // Записываем случайную цифру.
            int creditType = random.Next(1, 4);

            switch (creditType)
            {
                case 1:
                    return CreditHistoryType.Zero;
                case 2:
                    return CreditHistoryType.Good;
                default:
                    return CreditHistoryType.Bad;
            }
        }
    }
}
