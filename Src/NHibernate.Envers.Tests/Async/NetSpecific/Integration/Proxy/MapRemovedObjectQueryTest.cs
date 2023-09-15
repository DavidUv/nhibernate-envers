﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Envers.Configuration;
using NHibernate.Envers.Query;
using NHibernate.Envers.Tests.Integration.ManyToMany.Ternary;
using NUnit.Framework;
using SharpTestsEx;

namespace NHibernate.Envers.Tests.NetSpecific.Integration.Proxy
{
	using System.Threading.Tasks;
	public partial class MapRemovedObjectQueryTest : TestBase
	{

		[Test]
		public async Task VerifyHistoryOfParentAsync()
		{
			var rev3 = await (AuditReader().CreateQuery().ForRevisionsOfEntity(typeof(TernaryMapEntity), true, true)
									 .Add(AuditEntity.Id().Eq(id))
									 .Add(AuditEntity.RevisionNumber().Eq(3))
									 .GetSingleResultAsync<TernaryMapEntity>()).ConfigureAwait(false);
			rev3.Map.Keys.Single().Number.Should().Be.EqualTo(2);
			rev3.Map.Values.Single().Str.Should().Be.EqualTo("2");
		}
	}
}