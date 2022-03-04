namespace WebStore.Domain.Types.Translators
{
    public static class OrderPaymentMethodTypeRussianTranslator
    {
        public static string GetRussianTranslate(OrderPaymentMethodType orderPaymentMethodType)
        {
            switch (orderPaymentMethodType)
            {
                case OrderPaymentMethodType.Cash:
                    return "Наличкой";
                case OrderPaymentMethodType.ElectronicWallet:
                    return "Электронным кошельком";
                case OrderPaymentMethodType.Card:
                    return "Картой";
                default:
                    return "Некорректный перевод типа 'OrderPaymentMethodType' из за неизвестного значения";
            }
        }
    }
}
