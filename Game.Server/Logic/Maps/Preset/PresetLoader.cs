using Newtonsoft.Json;

namespace Game.Server.Logic.Maps.Preset
{
    internal class PresetLoader : IPresetLoader
    {
        public IReadOnlyCollection<Cell> Load()
        {
            var json = File.ReadAllText("C:\\Projects\\Mine\\My_awesome_character\\Game.Server\\Logic\\Maps\\Preset\\map-1.json");
            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("map preset is empty");

            return JsonConvert.DeserializeObject<Cell[]>(json);
        }
    }
}