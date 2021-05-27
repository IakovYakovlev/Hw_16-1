using System;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для создания случайного типа депозита.
    /// </summary>
    class RandomDepositType
    {

        // Создаем рандом.
        static Random random = new Random();

        /// <summary>
        /// Метод для случайного типа депозита.
        /// </summary>
        /// <returns>Тип депозита</returns>
        public static DepositType RandomType()
        {
            // Записываем случайную цифру.
            int clientType = random.Next(1, 3);

            switch (clientType)
            {
                case 1:
                    return DepositType.WithoutСapitalization;
                default:
                    return DepositType.WithСapitalization;
            }
        }
    }
}
