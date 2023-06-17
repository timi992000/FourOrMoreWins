using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourOrMoreWins.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
	public class DependsUpon : Attribute
	{
		public string MemberName;
		public DependsUpon(string memberName)
		{
			MemberName = memberName;
		}
	}
}
