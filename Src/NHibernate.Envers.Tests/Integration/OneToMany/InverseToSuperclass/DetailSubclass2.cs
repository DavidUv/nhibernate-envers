﻿using NHibernate.Envers.Configuration.Attributes;

namespace NHibernate.Envers.Tests.Integration.OneToMany.InverseToSuperclass
{
	[Audited]
	public class DetailSubclass2 : DetailSubclass
	{
		public virtual string Str3 { get; set; }
	}
}