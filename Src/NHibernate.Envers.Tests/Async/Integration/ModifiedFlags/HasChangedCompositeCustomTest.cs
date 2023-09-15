﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using NHibernate.Envers.Tests.Entities.CustomType;
using NHibernate.Envers.Tests.Tools;
using NUnit.Framework;
using SharpTestsEx;

namespace NHibernate.Envers.Tests.Integration.ModifiedFlags
{
	using System.Threading.Tasks;
	public partial class HasChangedCompositeCustomTest : AbstractModifiedFlagsEntityTest
	{

		[Test]
		public async Task VerifyHasChangedAsync()
		{
			(await (QueryForPropertyHasChangedAsync(typeof (CompositeCustomTypeEntity), id, "Component")).ConfigureAwait(false))
				.ExtractRevisionNumbersFromRevision()
				.Should().Have.SameSequenceAs(1, 2, 3);

			(await (QueryForPropertyHasNotChangedAsync(typeof(CompositeCustomTypeEntity), id, "Component")).ConfigureAwait(false))
				.ExtractRevisionNumbersFromRevision()
				.Should().Be.Empty();
		}
	}
}