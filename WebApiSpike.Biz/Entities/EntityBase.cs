using WebSpikeApi.Core.Contracts;

namespace WebApiSpike.Biz.Entities
{
	public class EntityBase<TKey> : IEntity<TKey>
	{
		public TKey Id { get; set; }
		public bool IsDeleted { get; set; }

		public bool IsValid()
		{
			return true;
		}
	}
}