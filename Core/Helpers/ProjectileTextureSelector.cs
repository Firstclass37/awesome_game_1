using My_awesome_character.Core.Constatns;

namespace My_awesome_character.Core.Helpers
{
    internal class ProjectileTextureSelector : TextureSelectorBase 
    {
        public ProjectileTextureSelector()
        {
            AddTexture(ProjectileTypes.Stone, "C:\\Projects\\Mine\\My_awesome_character\\Assets\\Weapons\\stone.png");    
        }
    }
}