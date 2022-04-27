using System;

namespace WebStore.Domain.Types.Translators
{
    public static class DeliveryMethodTypeRussianTranslator
    {
        public static string GetRussianTranslate(DeliveryMethodType deliveryMethodType)
        {
            switch (deliveryMethodType)
            {
                case DeliveryMethodType.Courier:
                    return "Курьер";
                case DeliveryMethodType.Pickup:
                    return "Самовывоз";
                case DeliveryMethodType.Post:
                    return "Почта";
                default:
                    throw new ArgumentOutOfRangeException($"Неизвестный тип 'DeliveryMethodType' для перевода на русский язык: {deliveryMethodType}");
            }
        }
    }
}
