using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models
{
    public class Checklist
    {
        [NotMapped]
        public int OffboardingActionItemsCount => ActionItems.Count(
            a => a.Type == ActionItemType.Offboarding
                 || a.Type == ActionItemType.Both);

        [NotMapped]
        public int OnboardingActionItemsCount => ActionItems.Count(
            a => a.Type == ActionItemType.Onboarding
                 || a.Type == ActionItemType.Both);

        [DisplayName("Action Items")]
        public virtual IList<ActionItem> ActionItems
        {
            get;
            set;
        } = new List<ActionItem>();

        public int Id
        {
            get;
            set;
        }

        public virtual User User
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }
    }
}