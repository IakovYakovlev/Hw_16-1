using System;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для почения случайной суммы на депозите.
    /// </summary>
    class RandomDepositAmount
    {
        // Создаем рандом.
        static Random random = new Random();

        /// <summary>
        /// Метод для получения случайной суммы на депозит.
        /// </summary>
        /// <returns>Сумма на депозит.</returns>
        public static double GetRandomDepositAmount()
        {
            int amount = random.Next(100, 100000);

            return random.NextDouble() * amount;
        }
    }
}
