using System;
using System.Collections.Generic;
using System.Linq;

namespace quiz.web.Helpers
{
    public static class RankExtensions
    {
        #region RankBy

        public static IEnumerable<TResult> RankBy<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, int, TResult> resultSelector)
        {
            return source.RankBy(keySelector, null, false, resultSelector);
        }

        public static IEnumerable<TResult> RankBy<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            Func<TSource, int, TResult> resultSelector)
        {
            return source.RankBy(keySelector, comparer, false, resultSelector);
        }

        public static IEnumerable<TResult> RankByDescending<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            Func<TSource, int, TResult> resultSelector)
        {
            return source.RankBy(keySelector, comparer, true, resultSelector);
        }

        public static IEnumerable<TResult> RankByDescending<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, int, TResult> resultSelector)
        {
            return source.RankBy(keySelector, null, true, resultSelector);
        }

        private static IEnumerable<TResult> RankBy<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending,
            Func<TSource, int, TResult> resultSelector)
        {
            comparer = comparer ?? Comparer<TKey>.Default;

            var grouped = source.GroupBy(keySelector);
            var ordered =
                descending
                    ? grouped.OrderByDescending(g => g.Key, comparer)
                    : grouped.OrderBy(g => g.Key, comparer);

            int totalRank = 1;
            foreach (var group in ordered)
            {
                int rank = totalRank;
                foreach (var item in group)
                {
                    yield return resultSelector(item, rank);
                    totalRank++;
                }
            }
        }

        #endregion

        #region DenseRankBy

        public static IEnumerable<TResult> DenseRankBy<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, int, TResult> resultSelector)
        {
            return source.DenseRankBy(keySelector, null, false, resultSelector);
        }

        public static IEnumerable<TResult> DenseRankBy<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            Func<TSource, int, TResult> resultSelector)
        {
            return source.DenseRankBy(keySelector, comparer, false, resultSelector);
        }

        public static IEnumerable<TResult> DenseRankByDescending<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            Func<TSource, int, TResult> resultSelector)
        {
            return source.DenseRankBy(keySelector, comparer, true, resultSelector);
        }

        public static IEnumerable<TResult> DenseRankByDescending<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, int, TResult> resultSelector)
        {
            return source.DenseRankBy(keySelector, null, true, resultSelector);
        }

        private static IEnumerable<TResult> DenseRankBy<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending,
            Func<TSource, int, TResult> resultSelector)
        {
            comparer = comparer ?? Comparer<TKey>.Default;

            var grouped = source.GroupBy(keySelector);
            var ordered =
                descending
                    ? grouped.OrderByDescending(g => g.Key, comparer)
                    : grouped.OrderBy(g => g.Key, comparer);

            int rank = 1;
            foreach (var group in ordered)
            {
                foreach (var item in group)
                {
                    yield return resultSelector(item, rank);
                }
                rank++;
            }
        }

        #endregion 
    }
}