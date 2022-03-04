namespace WebStore.Domain.Types.Translators
{
    public static class OrderStatusTypeRussianTranslator
    {
        public static string GetRussianTranslate(OrderStatusType orderStatusType)
        {
            switch (orderStatusType)
            {
                case OrderStatusType.AwaitingProcessing:
                    return "Ожидает обработки";
                case OrderStatusType.AwaitingPayment:
                    return "Ожидает оплаты";
                case OrderStatusType.SendingExpected:
                    return "Ожидает отгрузки";
                case OrderStatusType.Packing:
                    return "Пакуется";
                case OrderStatusType.WaitingSent:
                    return "Ожидает отправки";
                case OrderStatusType.OnWay:
                    return "В пути";
                case OrderStatusType.Arrived:
                    return "Прибыл";
                case OrderStatusType.Frozen:
                    return "Заморожен";
                case OrderStatusType.RequestСancellation:
                    return "Запрос на отмену";
                case OrderStatusType.Canceled:
                    return "Отменен";
                default:
                    return "Некорректный перевод типа 'OrderStatusType' из за неизвестного значения";
            }
        }
    }
}
