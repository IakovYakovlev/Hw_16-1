using System;
using System.Threading;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для создания случайного типа кредитной истории.
    /// </summary>
    class RandomCreditType
    {
        /// <summary>
        /// Метод для случайного типа кредитной истории.
        /// </summary>
        /// <returns>Тип клиента.</returns>
        public static CreditHistoryType RandomType()
        {
            // Записываем случайную цифру.
            int creditType = StaticRandom.Rand(1, 4);

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
