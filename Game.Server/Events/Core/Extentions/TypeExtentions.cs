namespace Game.Server.Events.Core.Extentions
{
    internal static class TypeExtentions
    {
        public static string GetFriendlyName(this Type type)
        {
            var friendlyName = type.Name;
            if (!type.IsGenericType)
                return friendlyName;

            var iBacktick = friendlyName.IndexOf('`');
            if (iBacktick > 0)
                friendlyName = friendlyName.Remove(iBacktick);

            friendlyName += "<";
            var typeParameters = type.GetGenericArguments();
            for (int i = 0; i < typeParameters.Length; ++i)
            {
                var typeParamName = typeParameters[i].GetFriendlyName();
                friendlyName += i == 0 ? typeParamName : "," + typeParamName;
            }
            friendlyName += ">";
            return friendlyName;
        }
    }
}