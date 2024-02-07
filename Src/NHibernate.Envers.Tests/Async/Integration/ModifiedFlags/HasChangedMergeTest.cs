﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using NHibernate.Envers.Tests.Entities.OneToMany;
using NHibernate.Envers.Tests.Tools;
using NUnit.Framework;
using SharpTestsEx;

namespace NHibernate.Envers.Tests.Integration.ModifiedFlags
{
	using System.Threading.Tasks;
	public partial class HasChangedMergeTest : AbstractModifiedFlagsEntityTest 
	{

		[Test]
		public async Task VerifyOneToManyInsertChildUpdateParentAsync()
		{
			var list = await (QueryForPropertyHasChangedAsync(typeof (CollectionRefEdEntity), parentId, "Data")).ConfigureAwait(false);
			list.Count.Should().Be.EqualTo(2);
			list.ExtractRevisionNumbersFromRevision().Should().Have.SameSequenceAs(1, 2);

			list = await (QueryForPropertyHasChangedAsync(typeof(CollectionRefEdEntity), parentId, "Reffering")).ConfigureAwait(false);
			list.Count.Should().Be.EqualTo(2);
			list.ExtractRevisionNumbersFromRevision().Should().Have.SameSequenceAs(1, 2);

			list = await (QueryForPropertyHasChangedAsync(typeof(CollectionRefIngEntity), childId, "Reference")).ConfigureAwait(false);
			list.Count.Should().Be.EqualTo(1);
			list.ExtractRevisionNumbersFromRevision().Should().Have.SameSequenceAs(2);
		}
	}
}