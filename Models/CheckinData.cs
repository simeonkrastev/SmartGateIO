namespace SmartGateIO.Models
{
	public class CheckinData
	{
		public int ID { get; set; }
		public int RfidTag { get; set; }
		public string Date { get; set; }

		public override string ToString()
		{
			return $"Tag: {RfidTag}, Date: {Date}";
		}
	}
}
