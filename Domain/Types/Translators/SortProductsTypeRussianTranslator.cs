using System;

namespace WebStore.Domain.Types.Translators
{
    public static class SortProductsTypeRussianTranslator
    {
        public static string GetRussianTranslate(SortProductsType filterProductsBy)
        {
            switch (filterProductsBy)
            {
                case SortProductsType.New:
                    return "Новинки";
                case SortProductsType.AscendingPrices:
                    return "Возрастанию цены";
                case SortProductsType.DescendingPrices:
                    return "Убыванию цены";
                default:
                    throw new ArgumentOutOfRangeException($"Неизвестный тип 'FilterProductsByType' для перевода на русский язык: {filterProductsBy}");
            }
        }
    }
}
