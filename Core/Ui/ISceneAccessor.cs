using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Ui
{
    internal interface ISceneAccessor
    {
        T FindFirst<T>(string name) where T : class;

        IEnumerable<T> FindAll<T>(Predicate<Godot.Node> predicate) where T : class;

        IEnumerable<T> FindAll<T>() where T : class;

        T GetScene<T>(string name) where T : class;
    }
}