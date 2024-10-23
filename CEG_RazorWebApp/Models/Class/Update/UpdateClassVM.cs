namespace CEG_RazorWebApp.Models.Class.Update
{
    public class UpdateClassVM
    {
        public string ClassName { get; set; } = null!;
        public string? CourseName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MinimumStudents { get; set; }
        public int? MaximumStudents { get; set; }
        public string? TeacherName { get; set; }
        public int? CurrentStudentAmount { get; set; }
    }
}
