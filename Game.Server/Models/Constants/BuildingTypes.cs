namespace Game.Server.Models.Constants
{
    public static class BuildingTypes
    {
        public static string[] List => typeof(BuildingTypes)
            .GetFields()
            .Select(p => (string)(p.GetValue(null) ?? string.Empty))
            .ToArray();

        public static bool Exists(string type) => List.Contains(type);


        public const string Road = nameof(Road);

        public const string Ground = nameof(Ground);

        public const string Block = nameof(Block);

        public const string TrafficLigh = nameof(TrafficLigh);

        /// <summary>
        /// Домик для рожания чуловечков
        /// </summary>
        public const string Home = nameof(Home);

        /// <summary>
        /// Солнечная батарея
        /// </summary>
        public const string SolarBattery = nameof(SolarBattery);

        /// <summary>
        /// Ветряная турбина
        /// </summary>
        public const string WindTurbine = nameof(WindTurbine);

        /// <summary>
        /// Геотермальная станция
        /// </summary>
        public const string GeothermalStation = nameof(GeothermalStation);

        /// <summary>
        /// Ядерный реактор
        /// </summary>
        public const string NuclearReactor = nameof(NuclearReactor);

        /// <summary>
        /// Накопитель
        /// </summary>
        public const string WaterStorage = nameof(WaterStorage);

        /// <summary>
        /// Влагоуловитель
        /// </summary>
        public const string MoistureTrap = nameof(MoistureTrap);

        /// <summary>
        /// Водяная помпа
        /// </summary>
        public const string WaterPump = nameof(WaterPump);

        /// <summary>
        /// Тепличная ферма
        /// </summary>
        public const string GreenhouseFarm = nameof(GreenhouseFarm);

        /// <summary>
        /// Промышленная ферма
        /// </summary>
        public const string IndustrialFarm = nameof(IndustrialFarm);

        /// <summary>
        /// Электролизный реактор
        /// </summary>
        public const string ElectrolysisReactor = nameof(ElectrolysisReactor);

        /// <summary>
        /// Завод переработки урана
        /// </summary>
        public const string UraniumProcessingPlant = nameof(UraniumProcessingPlant);

        /// <summary>
        /// Аллюминиевая шахта
        /// </summary>
        public const string AluminumMine = nameof(AluminumMine);

        /// <summary>
        /// Железная шахта
        /// </summary>
        public const string IronMine = nameof(IronMine);

        /// <summary>
        /// Сталелитейный завод
        /// </summary>
        public const string SteelPlant = nameof(SteelPlant);

        /// <summary>
        /// Угольная шахта
        /// </summary>
        public const string CoalMine = nameof(CoalMine);

        /// <summary>
        /// Урановая шахта
        /// </summary>
        public const string UranusMine = nameof(UranusMine);

        /// <summary>
        /// Кремниевый карьер
        /// </summary>
        public const string SiliconQuarry = nameof(SiliconQuarry);

        /// <summary>
        /// Химический завод
        /// </summary>
        public const string ChemicalFactory = nameof(ChemicalFactory);

        /// <summary>
        /// Стекольный завод
        /// </summary>
        public const string GlassFactory = nameof(GlassFactory);

        /// <summary>
        /// Электронный завод
        /// </summary>
        public const string ElectronicPlant = nameof(ElectronicPlant);
    }
}