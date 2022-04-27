using System;

namespace WebStore.Domain.Types.Translators
{
    public static class PaymentMethodTypeRussianTranslator
    {
        public static string GetRussianTranslate(PaymentMethodType paymentMethodType)
        {
            switch (paymentMethodType)
            {
                case PaymentMethodType.Cash:
                    return "Наличкой";
                case PaymentMethodType.ElectronicWallet:
                    return "Электронным кошельком";
                case PaymentMethodType.Card:
                    return "Картой";
                default:
                    throw new ArgumentOutOfRangeException($"Неизвестный тип 'PaymentMethodType' для перевода на русский язык: {paymentMethodType}");
            }
        }
    }
}
