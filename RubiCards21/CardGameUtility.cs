using System;
using System.Collections.Generic;

namespace RubiCards21
{
    public static class CardGameUtility
    {
        public static T RandomEnum<T>()
        {
            var enumArray = (T[])Enum.GetValues(typeof(T));
            int randomIndex = new Random().Next(enumArray.Length);
            return enumArray[randomIndex];
        }

        public static T GetRandomElement<T>(List<T> list)
        {
            var randomIndex = new Random().Next(list.Count);
            return list[randomIndex];
        }
        
        public static int GetRandomIndex<T>(List<T> list) => new Random().Next(list.Count);
    }
}