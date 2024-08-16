using FluentValidation;

namespace HandyControl.Demo
{
    public class PersonValidation:AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("请输入姓名");
        }
        
    }
}