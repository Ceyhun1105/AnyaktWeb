using System.ComponentModel.DataAnnotations;

namespace ExamBilet2.Areas.ViewModels
{
    public class AdminLoginViewModel
    {
        [StringLength(maximumLength: 50)]
        public string UserName { get; set; }
        [StringLength(maximumLength: 50),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
