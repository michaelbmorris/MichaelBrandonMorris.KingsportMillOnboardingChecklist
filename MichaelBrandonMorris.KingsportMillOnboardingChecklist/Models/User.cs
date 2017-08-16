using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models.
    UserViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models
{
    public class User : IdentityUser
    {
        [NotMapped]
        public string Name => $"{FirstName} {LastName}";

        public virtual IList<ActionItemAssignment> ActionItemAssignments
        {
            get;
            set;
        } = new List<ActionItemAssignment>();

        public virtual Checklist Checklist
        {
            get;
            set;
        }

        public string EmployeeId
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public bool HasBeenOnboarded
        {
            get;
            set;
        }

        public bool IsAsignee
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public static User Create(EditViewModel model)
        {
            return new User
            {
                Email = model.Email,
                EmployeeId = model.EmployeeId,
                FirstName = model.FirstName,
                IsAsignee = model.IsAssignee,
                LastName = model.LastName,
                UserName = model.Email
            };
        }
    }
}