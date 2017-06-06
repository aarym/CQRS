﻿#region Copyright
// // -----------------------------------------------------------------------
// // <copyright company="cdmdotnet Limited">
// // 	Copyright cdmdotnet Limited. All rights reserved.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System.Runtime.Serialization;

namespace Cqrs.Domain
{
	internal class AggregateDescriptor<TAggregateRoot, TAuthenticationToken> : IAggregateDescriptor<TAuthenticationToken>
		where TAggregateRoot : IAggregateRoot<TAuthenticationToken>
	{
		IAggregateRoot<TAuthenticationToken> IAggregateDescriptor<TAuthenticationToken>.Aggregate
		{
			get { return Aggregate; }
		}

		[DataMember]
		public TAggregateRoot Aggregate { get; set; }

		[DataMember]
		public int Version { get; set; }
	}
}