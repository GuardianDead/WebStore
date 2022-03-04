namespace WebStore.Domain.Types.Translators
{
    public static class GenderTypeRussianTranslator
    {
        public static string GetRussianTranslate(GenderType genderType)
        {
            switch (genderType)
            {
                case GenderType.Man:
                    return "Мужчина";
                case GenderType.Woman:
                    return "Женщина";
                default:
                    return "Некорректный перевод типа 'GenderType' из за неизвестного значения";
            }
        }
    }
}
