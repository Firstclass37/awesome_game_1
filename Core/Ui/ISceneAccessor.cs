namespace My_awesome_character.Core.Ui
{
    internal interface ISceneAccessor
    {
        T FindFirst<T>(string name) where T : class;
        T GetScene<T>(string name) where T : class;
    }
}