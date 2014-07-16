using System;

namespace WebSpikeApi.Core
{
	public class UniqueConstraintException: Exception
	{
		public UniqueConstraintException(string message):base(message)
		{
		}
	}
}
