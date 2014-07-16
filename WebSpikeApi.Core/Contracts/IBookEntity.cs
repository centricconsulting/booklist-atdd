namespace WebSpikeApi.Core.Contracts
{
	public interface IBookEntity : IEntity<int>
	{
		string Title { get; set; }
		string Author { get; set; }
		string Isbn { get; set; }
	}
}