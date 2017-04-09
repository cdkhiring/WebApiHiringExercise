using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace WebApiHiringExorcise.Tests.Extensions
{
    public static class DbSetExtensions
    {
        public static void ProvideList<TListType>(this Mock<DbSet<TListType>> dbSet, List<TListType> list) where TListType : class
        {
            var queryable = dbSet.As<IQueryable<TListType>>();
            var data = list.AsQueryable();

            queryable.Setup(q => q.Provider).Returns(data.Provider);
            queryable.Setup(q => q.Expression).Returns(data.Expression);
            queryable.Setup(q => q.ElementType).Returns(data.ElementType);
            queryable.Setup(q => q.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}
