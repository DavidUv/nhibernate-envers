﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using NUnit.Framework;

namespace NHibernate.Envers.Tests.Integration.NotInsertable
{
	using System.Threading.Tasks;
	public partial class NotInsertableTest : TestBase
	{

		[Test]
		public async Task VerifyRevisionCountAsync()
		{
			CollectionAssert.AreEquivalent(new[] { 1, 2 }, await (AuditReader().GetRevisionsAsync(typeof(NotInsertableTestEntity), id1)).ConfigureAwait(false));
		}

		[Test]
		public async Task VerifyHistoryOfId1Async()
		{
			var ver1 = new NotInsertableTestEntity { Id = id1, Data = "data1", DataCopy = "data1" };
			var ver2 = new NotInsertableTestEntity { Id = id1, Data = "data2", DataCopy = "data2" };

			var rev1 = await (AuditReader().FindAsync<NotInsertableTestEntity>(id1, 1)).ConfigureAwait(false);
			var rev2 = await (AuditReader().FindAsync<NotInsertableTestEntity>(id1, 2)).ConfigureAwait(false);

			Assert.AreEqual(ver1, rev1);
			Assert.AreEqual(ver2, rev2);
		}
	}
}