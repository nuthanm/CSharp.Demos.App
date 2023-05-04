using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.Data
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }

        public string? Credits { get; set; }
    }
}