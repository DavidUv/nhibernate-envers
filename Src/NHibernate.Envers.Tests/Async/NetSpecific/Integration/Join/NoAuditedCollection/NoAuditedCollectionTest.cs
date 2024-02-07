﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using NUnit.Framework;
using SharpTestsEx;

namespace NHibernate.Envers.Tests.NetSpecific.Integration.Join.NoAuditedCollection
{
	using System.Threading.Tasks;
	public partial class NoAuditedCollectionTest : TestBase
	{

		[Test]
		public async Task VerifyRevision1Async()
		{
			var rev1 = await (AuditReader().FindAsync<Audited>(id, 1)).ConfigureAwait(false);
			rev1.Number.Should().Be.EqualTo(1);
			rev1.XCollection.Should().Be.Null();
		}

		[Test]
		public async Task VerifyRevision2Async()
		{
			var rev1 = await (AuditReader().FindAsync<Audited>(id, 2)).ConfigureAwait(false);
			rev1.Number.Should().Be.EqualTo(2);
			rev1.XCollection.Should().Be.Null();
		}
	}
}