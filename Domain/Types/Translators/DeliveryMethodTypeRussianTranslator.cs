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
                    return "Некорректный перевод типа 'DeliveryMethodType' из за неизвестного значения";
            }
        }
    }
}
