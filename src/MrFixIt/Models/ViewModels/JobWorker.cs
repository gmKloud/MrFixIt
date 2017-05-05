using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrFixIt.ViewModels
{
    public class JobWorker
    {
        public int SelJobId { get; set; }
        public string JobDesc { get; set; }
        public string JobTitle { get; set; }
        public int SignedInId { get; set; }
        public string LoggedName { get; set; }

        public JobWorker (int selJobId, string jobDesc, string jobTitle, int signedInId, string loggedName)
        {
            SelJobId = selJobId;
            JobDesc = jobDesc;
            JobTitle = jobTitle;
            SignedInId = signedInId;
            LoggedName = loggedName;
        }

    }
}
