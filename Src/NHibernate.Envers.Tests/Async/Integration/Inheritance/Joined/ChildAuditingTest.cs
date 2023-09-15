﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using NHibernate.Envers.Tests.Integration.Inheritance.Entities;
using NUnit.Framework;
using SharpTestsEx;

namespace NHibernate.Envers.Tests.Integration.Inheritance.Joined
{
	using System.Threading.Tasks;
	public partial class ChildAuditingTest : TestBase
	{

		[Test]
		public async Task VerifyRevisionCountAsync()
		{
			CollectionAssert.AreEquivalent(new[] { 1, 2 }, await (AuditReader().GetRevisionsAsync(typeof(ChildEntity), id1)).ConfigureAwait(false));
		}

		[Test]
		public async Task VerifyHistoryOfChildAsync()
		{
			var ver1 = new ChildEntity { Id = id1, Data = "x", Number = 1 };
			var ver2 = new ChildEntity { Id = id1, Data = "y", Number = 2 };

			Assert.AreEqual(ver1, await (AuditReader().FindAsync<ChildEntity>(id1, 1)).ConfigureAwait(false));
			Assert.AreEqual(ver2, await (AuditReader().FindAsync<ChildEntity>(id1, 2)).ConfigureAwait(false));
		}

		[Test]
		public async Task VerifyPolymorphicQueryAsync()
		{
			var childVersion1 = new ChildEntity { Id = id1, Data = "x", Number = 1 };
			Assert.AreEqual(childVersion1, await (AuditReader().CreateQuery().ForEntitiesAtRevision(typeof(ChildEntity), 1).GetSingleResultAsync()).ConfigureAwait(false));
			Assert.AreEqual(childVersion1, await (AuditReader().CreateQuery().ForEntitiesAtRevision(typeof(ParentEntity), 1).GetSingleResultAsync()).ConfigureAwait(false));
		}

		[Test]
		public async Task VerifyPolymorphicQueryGenericAsync()
		{
			var childVersion1 = new ChildEntity { Id = id1, Data = "x", Number = 1 };
			(await (AuditReader().CreateQuery().ForEntitiesAtRevision<ChildEntity>(1).SingleAsync()).ConfigureAwait(false)).Should().Be(childVersion1);
			(await (AuditReader().CreateQuery().ForEntitiesAtRevision<ParentEntity>(1).SingleAsync()).ConfigureAwait(false)).Should().Be(childVersion1);
		}
	}
}