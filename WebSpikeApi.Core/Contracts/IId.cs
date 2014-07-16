namespace WebSpikeApi.Core.Contracts
{
	public interface IId<TKey>
	{
		TKey Id { get; set; }
	}
}