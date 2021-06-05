using System;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для почения случайной суммы на депозите.
    /// </summary>
    class RandomDepositAmount
    {
        /// <summary>
        /// Метод для получения случайной суммы на депозит.
        /// </summary>
        /// <returns>Сумма на депозит.</returns>
        public static double GetRandomDepositAmount()
        {
            int amount = StaticRandom.Rand(100, 100000);

            return new Random().NextDouble() * amount;
        }
    }
}
