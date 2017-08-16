using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models
{
    public class Policy
    {
        public virtual IList<ActionItem> ActionItems
        {
            get;
            set;
        }

        public string Description
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
    }
}