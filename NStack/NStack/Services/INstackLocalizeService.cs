using NStack.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NStack.Services
{
    public interface INstackLocalizeService<TSection, T> where TSection : ResourceData<T> where T: ResourceItem
    {
        DataWrapper<IReadOnlyCollection<Language>> GetLanguages();

        DataMetaWrapper<DataMetaWrapper<TSection>> GetResource(int id);
    }
}
