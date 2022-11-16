using System;
using System.Collections.Generic;
using System.Text;

namespace RauCuXanh.ViewModels
{
    public class PersonalInformationViewModel : BaseViewModel
    {
        public List<string> Sex { get; set; }
        public PersonalInformationViewModel()
        {
            Title = "Thông tin cá nhân";
            Sex = new List<string> { "Male", "Female" };
        }
    }
}
