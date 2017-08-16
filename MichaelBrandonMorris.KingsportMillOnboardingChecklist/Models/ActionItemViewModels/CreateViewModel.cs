using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models.ActionItemViewModels
{
    public class CreateViewModel
    {
        [DisplayName("Assignee")]
        public string AssigneeId { get; set; }
        public string Name { get; set; }
        [DisplayName("Onboarding Description")]
        public string OnboardingDescription { get; set; }
        [DisplayName("Offboarding Description")]
        public string OffboardingDescription { get; set; }
        public ActionItemType Type { get; set; }
        [DisplayName("Policy")]
        public int PolicyId { get; set; }
    }
}
