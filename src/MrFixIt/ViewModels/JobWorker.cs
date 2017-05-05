using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrFixIt.Models;

namespace MrFixIt.ViewModels
{
    public class JobWorker
    {
        public virtual Worker Worker { get; set; }
        public virtual Job[] Job { get; set; }
    }
}
