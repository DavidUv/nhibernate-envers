﻿using System;

namespace NHibernate.Envers.Configuration.Attributes
{
	/**
	 * <p>
	 * Annotation to specify a "fake" bi-directional relation. Such a relation uses {@code @OneToMany} +
	 * {@code @JoinColumn} on the one side, and {@code @ManyToOne} + {@code @Column(insertable=false, updatable=false)} on
	 * the many side. Then, Envers won't use a join table to audit this relation, but will store changes as in a normal
	 * bi-directional relation.
	 * </p>
	 *
	 * <p>
	 * This annotation is <b>experimental</b> and may change in future releases.
	 * </p>
	 *
	 * @author Simon Duduica, port of Envers Tools class by Adam Warski (adam at warski dot org)
	 */
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class AuditMappedByAttribute:Attribute 
	{
		/**
		 * @return Name of the property in the related entity which maps back to this entity. The property should be
		 * mapped with {@code @ManyToOne} and {@code @Column(insertable=false, updatable=false)}.
		 */
		public string MappedBy { get; set; }

		/**
		 * @return Name of the property in the related entity which maps to the position column. Should be specified only
		 * for indexed collection, when @{@link org.hibernate.annotations.IndexColumn} is used on the collection.
		 * The property should be mapped with {@code @Column(insertable=false, updatable=false)}.
		 */
		public string PositionMappedBy { get; set; }
	}
}