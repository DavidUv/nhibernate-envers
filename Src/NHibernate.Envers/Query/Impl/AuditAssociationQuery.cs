﻿using System;
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
	public partial class AuditAssociationQuery : IAuditQueryImplementor
	{
		private readonly AuditConfiguration _auditConfiguration;
		private readonly IAuditReaderImplementor _auditReader;
		private readonly IAuditQueryImplementor _parent;
		private readonly QueryBuilder _queryBuilder;
		private readonly JoinType _joinType;
		private readonly string _ownerAlias;
		private readonly string _alias;
		private readonly string _entityName;
		private readonly IIdMapper _ownerAssociationIdMapper;
		private readonly Parameters _parameters;
		private readonly IList<IAuditCriterion> _criterions;
		private readonly IList<AuditAssociationQuery> _associationQueries;
		private readonly IDictionary<string, AuditAssociationQuery> _associationQueryMap;
		private bool _hasProjections;
		private bool _hasOrders;

		public AuditAssociationQuery(AuditConfiguration auditConfiguration, 
																			IAuditReaderImplementor auditReader,
																			IAuditQueryImplementor parent, 
																			QueryBuilder queryBuilder, 
																			string ownerEntityName, 
																			string propertyName, 
																			JoinType joinType, 
																			string ownerAlias)
		{
			_auditConfiguration = auditConfiguration;
			_auditReader = auditReader;
			_parent = parent;
			_queryBuilder = queryBuilder;
			_joinType = joinType;
			_ownerAlias = ownerAlias;

			var relationDescription = CriteriaTools.GetRelatedEntity(auditConfiguration, ownerEntityName, propertyName);
			if (relationDescription == null)
			{
				throw new ArgumentException("Property " + propertyName + " of entity " + ownerEntityName + " is not a valid association for queries");
			}
			_entityName = relationDescription.ToEntityName;
			_ownerAssociationIdMapper = relationDescription.IdMapper;
			_alias = queryBuilder.GenerateAlias();
			_parameters = queryBuilder.AddParameters(_alias);
			_criterions = new List<IAuditCriterion>();
			_associationQueries = new List<AuditAssociationQuery>();
			_associationQueryMap = new Dictionary<string, AuditAssociationQuery>();
		}


		public IList GetResultList()
		{
			return _parent.GetResultList();
		}

		public IList<T> GetResultList<T>()
		{
			return _parent.GetResultList<T>();
		}

		public object GetSingleResult()
		{
			return _parent.GetSingleResult();
		}

		public T GetSingleResult<T>()
		{
			return _parent.GetSingleResult<T>();
		}

		public IAuditQuery Add(IAuditCriterion criterion)
		{
			_criterions.Add(criterion);
			return this;
		}

		public IAuditQuery AddProjection(IAuditProjection projection)
		{
			_hasProjections = true;
			var projectionData = projection.GetData(_auditConfiguration);
			var propertyName = CriteriaTools.DeterminePropertyName(_auditConfiguration, _auditReader, _entityName, projectionData.Item2);
			_queryBuilder.AddProjection(projectionData.Item1, _alias, propertyName, projectionData.Item3);
			RegisterProjection(_entityName, projection);
			return this;
		}

		public IAuditQuery AddOrder(IAuditOrder order)
		{
			_hasOrders = true;
			var orderData = order.GetData(_auditConfiguration);
			var propertyName = CriteriaTools.DeterminePropertyName(_auditConfiguration, _auditReader, _entityName, orderData.Item1);
			_queryBuilder.AddOrder(_alias, propertyName, orderData.Item2);
			return this;
		}

		public IAuditQuery SetMaxResults(int maxResults)
		{
			_parent.SetMaxResults(maxResults);
			return this;
		}

		public IAuditQuery SetFirstResult(int firstResult)
		{
			_parent.SetFirstResult(firstResult);
			return this;
		}

		public IAuditQuery SetCacheable(bool cacheable)
		{
			_parent.SetCacheable(cacheable);
			return this;
		}

		public IAuditQuery SetCacheRegion(string cacheRegion)
		{
			_parent.SetCacheRegion(cacheRegion);
			return this;
		}

		public IAuditQuery SetComment(string comment)
		{
			_parent.SetComment(comment);
			return this;
		}

		public IAuditQuery SetFlushMode(FlushMode flushMode)
		{
			_parent.SetFlushMode(flushMode);
			return this;
		}

		public IAuditQuery SetCacheMode(CacheMode cacheMode)
		{
			_parent.SetCacheMode(cacheMode);
			return this;
		}

		public IAuditQuery SetTimeout(int timeout)
		{
			_parent.SetTimeout(timeout);
			return this;
		}

		public IAuditQuery SetLockMode(LockMode lockMode)
		{
			_parent.SetLockMode(lockMode);
			return this;
		}

		public IAuditQuery TraverseRelation(string associationName, JoinType joinType)
		{
			AuditAssociationQuery result;
			if (!_associationQueryMap.TryGetValue(associationName, out result))
			{
				result = new AuditAssociationQuery(_auditConfiguration, _auditReader, this, _queryBuilder, _entityName, associationName, _joinType, _alias);
				_associationQueries.Add(result);
				_associationQueryMap[associationName] = result;
			}
			return result;
		}

		public IAuditQuery Up()
		{
			return _parent;
		}

		public void RegisterProjection(string entityName, IAuditProjection projection)
		{
			_parent.RegisterProjection(entityName, projection);
		}

		private bool hasCriterions()
		{
			return _criterions.Any() || _associationQueries.Any(sub => sub.hasCriterions());
		}

		private bool hasOrders()
		{
			return _hasOrders || _associationQueries.Any(sub => sub.hasOrders());
		}

		private bool hasProjections()
		{
			return _hasProjections || _associationQueries.Any(sub => sub.hasProjections());
		}

		public void AddCriterionsToQuery(IAuditReaderImplementor versionsReader)
		{
			if (hasCriterions() || hasOrders() || hasProjections())
			{
				if (_auditConfiguration.EntCfg.IsVersioned(_entityName))
				{
					var verEntCfg = _auditConfiguration.AuditEntCfg;
					var auditEntityName = verEntCfg.GetAuditEntityName(_entityName);
					_queryBuilder.AddFrom(auditEntityName, _alias, false);

					// owner.reference_id = target.originalId.Id
					var originalIdPropertyName = verEntCfg.OriginalIdPropName;
					var idMapperTarget = _auditConfiguration.EntCfg[_entityName].IdMapper;
					var prefix = _alias + "." + originalIdPropertyName;
					_ownerAssociationIdMapper.AddIdsEqualToQuery(_queryBuilder.RootParameters, _ownerAlias, idMapperTarget, prefix);

					//filter reference of target entity
					var revisionPropertyPath = verEntCfg.RevisionNumberPath;
					var referencedIdData = new MiddleIdData(verEntCfg, _auditConfiguration.EntCfg[_entityName].IdMappingData, null, _entityName, _auditConfiguration.EntCfg.IsVersioned(_entityName));
					_auditConfiguration.GlobalCfg.AuditStrategy.AddEntityAtRevisionRestriction(_queryBuilder, _parameters, revisionPropertyPath, verEntCfg.RevisionEndFieldName, true, referencedIdData, revisionPropertyPath, originalIdPropertyName, _alias, _queryBuilder.GenerateAlias());
				}
				else
				{
					_queryBuilder.AddFrom(_entityName, _alias, false);
					//owner.reference_id = target.id
					var idMapperTarget = _auditConfiguration.EntCfg.GetNotVersionEntityConfiguration(_entityName).IdMapper;
					_ownerAssociationIdMapper.AddIdsEqualToQuery(_queryBuilder.RootParameters, _ownerAlias, idMapperTarget, _alias);
				}

				foreach (var criterion in _criterions)
				{
					criterion.AddToQuery(_auditConfiguration, versionsReader, _entityName, _queryBuilder, _parameters);
				}
				foreach (var sub in _associationQueries)
				{
					sub.AddCriterionsToQuery(versionsReader);
				}
			}
		}
	}
}