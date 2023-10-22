namespace Game.Server.Common.Extentions
{
    internal static class DictionaryExntentions
    {
        public static Dictionary<TKey, TValue> AddNew<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
            where TKey : notnull
        {
            dictionary.Add(key, value);
            return dictionary;
        }

        public static Dictionary<TKey, TValue> Update<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
            where TKey : notnull
        {
            dictionary[key] = value;
            return dictionary;
        }
    }
}