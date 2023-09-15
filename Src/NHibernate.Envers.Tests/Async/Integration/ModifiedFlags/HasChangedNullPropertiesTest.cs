﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using NHibernate.Envers.Query;
using NHibernate.Envers.Tests.Integration.Basic;
using NHibernate.Envers.Tests.Tools;
using NUnit.Framework;
using SharpTestsEx;

namespace NHibernate.Envers.Tests.Integration.ModifiedFlags
{
	using System.Threading.Tasks;
	public partial class HasChangedNullPropertiesTest : AbstractModifiedFlagsEntityTest
	{

		[Test]
		public async Task VerifyHasChangedAsync()
		{
			(await (QueryForPropertyHasChangedWithDeletedAsync(typeof (BasicTestEntity1), id1, "Str1")).ConfigureAwait(false))
				.ExtractRevisionNumbersFromRevision()
				.Should().Have.SameSequenceAs(1, 3);
			(await (QueryForPropertyHasChangedWithDeletedAsync(typeof(BasicTestEntity1), id1, "Long1")).ConfigureAwait(false))
				.ExtractRevisionNumbersFromRevision()
				.Should().Have.SameSequenceAs(1);
			// str1 property was null before insert and after insert so in a way it didn't change - is it a good way to go?
			(await (QueryForPropertyHasChangedWithDeletedAsync(typeof(BasicTestEntity1), id2, "Str1")).ConfigureAwait(false))
				.ExtractRevisionNumbersFromRevision()
				.Should().Have.SameSequenceAs(4);
			(await (QueryForPropertyHasChangedWithDeletedAsync(typeof(BasicTestEntity1), id2, "Long1")).ConfigureAwait(false))
				.ExtractRevisionNumbersFromRevision()
				.Should().Have.SameSequenceAs(2);

			(await (AuditReader().CreateQuery().ForRevisionsOfEntity(typeof (BasicTestEntity1), false, true)
				.Add(AuditEntity.Property("Str1").HasChanged())
				.Add(AuditEntity.Property("Long1").HasChanged())
				.GetResultListAsync()).ConfigureAwait(false))
				.ExtractRevisionNumbersFromRevision()
				.Should().Have.SameSequenceAs(1);
		}
	}
}