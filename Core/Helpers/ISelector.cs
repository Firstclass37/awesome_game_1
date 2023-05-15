namespace My_awesome_character.Core.Helpers
{
    public interface ISelector<TFrom, KTo>
    {
        KTo Select(TFrom from);
    }
}