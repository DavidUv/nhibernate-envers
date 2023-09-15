﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using NHibernate.Envers.Tests.Entities;
using NHibernate.Envers.Tests.Entities.Components.Relations;
using NUnit.Framework;

namespace NHibernate.Envers.Tests.Integration.Components.Relations
{
	using System.Threading.Tasks;
	public partial class ManyToOneInComponentTest : TestBase
	{

		[Test]
		public async Task VerifyRevisionCountsAsync()
		{
			CollectionAssert.AreEqual(new[] { 2, 3 }, await (AuditReader().GetRevisionsAsync(typeof(ManyToOneComponentTestEntity), mtocte_id1)).ConfigureAwait(false));
		}

		[Test]
		public async Task VerifyHistoryAsync()
		{
			var ste1 = await (Session.GetAsync<StrTestEntity>(ste_id1)).ConfigureAwait(false);
			var ste2 = await (Session.GetAsync<StrTestEntity>(ste_id2)).ConfigureAwait(false);

			var ver2 = new ManyToOneComponentTestEntity { Id = mtocte_id1, Comp1 = new ManyToOneComponent{Entity = ste1, Data = "data1" } };
			var ver3 = new ManyToOneComponentTestEntity { Id = mtocte_id1, Comp1 = new ManyToOneComponent{Entity = ste2, Data = "data1" } };

			Assert.IsNull(await (AuditReader().FindAsync<ManyToOneComponentTestEntity>(mtocte_id1, 1)).ConfigureAwait(false));
			Assert.AreEqual(ver2, await (AuditReader().FindAsync<ManyToOneComponentTestEntity>(mtocte_id1, 2)).ConfigureAwait(false));
			Assert.AreEqual(ver3, await (AuditReader().FindAsync<ManyToOneComponentTestEntity>(mtocte_id1, 3)).ConfigureAwait(false));
		}
	}
}