using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Ui
{
    internal interface ISceneAccessor
    {
        T GetNode<T>(string name) where T : class;

        T FindFirst<T>(string name, bool isStatic = false) where T : class;

        IEnumerable<T> FindAll<T>(Predicate<T> predicate) where T : class;

        IEnumerable<T> FindAll<T>() where T : class;

        T GetScene<T>(string name) where T : class;
    }
}