﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Envers.Configuration;
using NHibernate.Envers.Entities.Mapper.Id;
using NHibernate.Envers.Entities.Mapper.Relation;
using NHibernate.Envers.Query.Criteria;
using NHibernate.Envers.Query.Order;
using NHibernate.Envers.Query.Projection;
using NHibernate.Envers.Reader;
using NHibernate.Envers.Tools.Query;
using NHibernate.SqlCommand;

namespace NHibernate.Envers.Query.Impl
{
	using System.Threading.Tasks;
	using System.Threading;
	public partial class AuditAssociationQuery : IAuditQueryImplementor
	{


		public async Task<IList> GetResultListAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			return await (_parent.GetResultListAsync(cancellationToken)).ConfigureAwait(false);
		}

		public async Task<IList<T>> GetResultListAsync<T>(CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			return await (_parent.GetResultListAsync<T>(cancellationToken)).ConfigureAwait(false);
		}

		public async Task<object> GetSingleResultAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			return await (_parent.GetSingleResultAsync(cancellationToken)).ConfigureAwait(false);
		}

		public async Task<T> GetSingleResultAsync<T>(CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			return await (_parent.GetSingleResultAsync<T>(cancellationToken)).ConfigureAwait(false);
		}
	}
}