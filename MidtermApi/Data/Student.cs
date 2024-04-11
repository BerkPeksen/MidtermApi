using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MidtermApi.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentNo { get; set; }
        public string Term { get; set; }
        public int TuitionTotal { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
    }
}
