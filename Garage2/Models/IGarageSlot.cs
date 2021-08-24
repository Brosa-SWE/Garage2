namespace Garage2.Models
{
	public interface IGarageSlot
	{
		int BlockId { get; set; }
		int Id { get; set; }
		int InUse { get; set; }
		string SlotId { get; set; }
	}
}