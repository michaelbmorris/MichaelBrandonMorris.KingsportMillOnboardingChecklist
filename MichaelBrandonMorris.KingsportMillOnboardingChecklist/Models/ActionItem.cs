using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models.ActionItemViewModels;
using System;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models
{
    public enum ActionItemType
    {
        Onboarding,
        Offboarding,
        Both
    }

    public class ActionItem
    {
        public ActionItem()
        {
        }

        public static ActionItem Create(
            CreateViewModel model,
            User assignee,
            Policy policy)
        {
            return new ActionItem
            {
                Assignee = assignee,
                Name = model.Name,
                OffboardingDescription = model.OffboardingDescription,
                OnboardingDescription = model.OnboardingDescription,
                Policy = policy
            };
        }

        public virtual Policy Policy { get; set; }

        public virtual User Assignee
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string OffboardingDescription
        {
            get;
            set;
        }

        public string OnboardingDescription
        {
            get;
            set;
        }

        public ActionItemType Type
        {
            get;
            set;
        }
    }
}