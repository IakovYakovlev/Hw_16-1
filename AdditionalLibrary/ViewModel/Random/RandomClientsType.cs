using System;
using System.Threading;

namespace AdditionalLibrary
{
    /// <summary>
    /// Класс для создания случайного типа клиента.
    /// </summary>
    class RandomClientsType
    {
        /// <summary>
        /// Метод для случайного типа клиента.
        /// </summary>
        /// <returns>Тип клиента.</returns>
        public static ClientsType RandomType()
        {
            // Записываем случайную цифру.
            int clientType = StaticRandom.Rand(1, 4);

            switch (clientType)
            {
                case 1:
                    return ClientsType.Entity;
                case 2:
                    return ClientsType.Usual;
                default:
                    return ClientsType.Vip;
            }
        }
    }
}
