namespace MidtermApi.Models
{
    public class UnpaidTuitionStatus
    {
        public string StudentNo { get; set; }
        public string Term { get; set; }
        public decimal UnpaidAmount { get; set; }
    }
}
