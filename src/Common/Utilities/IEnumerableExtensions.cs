namespace Microsoft.Ciqs.Saw.Common.Utilities
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> TopologicalOrder<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, IEnumerable<TKey>> dependencyListSelector)
        {
            var entryDictionary = source.ToDictionary(keySelector);
            var explored = new HashSet<TKey>();
            var result = new List<TSource>();

            Action<TKey> dfs = null;
            dfs = (TKey key) => {
                if (explored.Contains(key))
                {
                    return;
                }
                
                explored.Add(key);
                
                var entry = entryDictionary[key];
                var dependencies = dependencyListSelector(entry) ?? Enumerable.Empty<TKey>();
                
                foreach (var dependency in dependencies)
                {
                    dfs(dependency);
                }
                
                result.Add(entry);
            };
            
            foreach (var key in entryDictionary.Keys)
            {                
                dfs(key);
            }
            
            return result;
        }
    }
}