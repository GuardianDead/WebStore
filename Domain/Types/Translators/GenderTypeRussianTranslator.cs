using System;

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
                    throw new ArgumentOutOfRangeException($"Неизвестный тип 'GenderType' для перевода на русский язык: {genderType}");
            }
        }
    }
}
