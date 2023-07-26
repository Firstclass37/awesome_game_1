namespace My_awesome_character.Core.Constatns
{
    internal class ResourceResourceTypes
    {
        private const string prefix = "resourc_resource";

        /// <summary>
        /// Вулкан
        /// </summary>
        public const string Vulcan = $"{prefix}_{nameof(Vulcan)}";

        /// <summary>
        /// Подземные воды
        /// </summary>
        public const string Groundwater = $"{prefix}_{nameof(Groundwater)}";

        /// <summary>
        /// Аллюминевая руда
        /// </summary>
        public const string Bauxite = $"{prefix}_{nameof(Bauxite)}";

        /// <summary>
        /// Железная руда
        /// </summary>
        public const string IronOre = $"{prefix}_{nameof(IronOre)}";

        /// <summary>
        /// Минералы
        /// </summary>
        public const string Minerals = $"{prefix}_{nameof(Minerals)}";

        /// <summary>
        /// Каменноугольный кокс
        /// </summary>
        public const string Coke = $"{prefix}_{nameof(Coke)}";

        /// <summary>
        /// Залежи урана
        /// </summary>
        public const string Uranium = $"{prefix}_{nameof(Uranium)}";
    }
}