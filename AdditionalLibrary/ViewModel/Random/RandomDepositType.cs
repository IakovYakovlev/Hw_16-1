using System;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для создания случайного типа депозита.
    /// </summary>
    class RandomDepositType
    {
        /// <summary>
        /// Метод для случайного типа депозита.
        /// </summary>
        /// <returns>Тип депозита</returns>
        public static DepositType RandomType()
        {
            // Записываем случайную цифру.
            int clientType = StaticRandom.Rand(1, 3);

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
