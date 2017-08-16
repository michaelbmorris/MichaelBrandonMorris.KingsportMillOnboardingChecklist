using System;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models
{
    public class ActionItemAssignment
    {
        public virtual User User
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public bool IsOnboarding
        {
            get;
            set;
        }

        public virtual ActionItem ActionItem
        {
            get;
            set;
        }

        public DateTime? CompletedOn
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }
    }
}