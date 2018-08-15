using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DevCube.Data;
using DevCube.Website.ViewModels;


namespace DevCube.Website.Validators
{
    public class DuplicateNameValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;

            var type = instance.GetType();

            string dataToValidate = null;

            if (value != null)
            {
                if (type == typeof(CreateProgrammerViewModel))
                {

                    PropertyInfo property = type.GetProperty("LastName");

                    var lastName = (string)property.GetValue(instance);

                    var allProgrammers = ProgrammerData.SelectAllProgrammers();

                    dataToValidate = (from p in allProgrammers
                                      where p.FirstName.Equals(value) && p.LastName.Equals(lastName)
                                      select p.FirstName).FirstOrDefault();
                }

                if (type == typeof(CreateSkillViewModel))
                {
                    var allSkills = SkillData.SelectAllSkills();

                    dataToValidate = (from s in allSkills
                                      where s.Name.Equals(value)
                                      select s.Name).FirstOrDefault();
                }
            }

            if (dataToValidate != null)
            {
                return new ValidationResult(ErrorMessageString);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}