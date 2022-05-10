using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace EcommerceProject.Specflow.Hooks
{
    [Binding]
    public static class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Service.Instance.ValueRetrievers.Register(new MoneyValueRetriver())
        }
    }
}
