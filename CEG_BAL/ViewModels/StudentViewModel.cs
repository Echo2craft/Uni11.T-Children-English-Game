namespace CEG_BAL.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            Account = new AccountViewModel();
        }

        public string Description { get; set; } = null!;

        public int? CurLevel { get; set; }

        public int? Age { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Image { get; set; }

        public virtual AccountViewModel Account { get; set; } = null!;

        public virtual ICollection<EnrollViewModel> Enrolls { get; set; } = new List<EnrollViewModel>();

        public virtual ParentViewModel Parents { get; set; } = null!;

        public virtual ICollection<StudentProgressViewModel> StudentProgresses { get; set; } = new List<StudentProgressViewModel>();
    }
}
