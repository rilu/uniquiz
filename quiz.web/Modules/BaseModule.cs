using System.Collections.Generic;
using System.Dynamic;
using System.Web.UI;
using Nancy;
using quiz.web.Models;

namespace quiz.web.Modules
{
    public class BaseModule : NancyModule
    {
        public dynamic Model = new ExpandoObject();

        protected PageModel Page { get; set; }

        public BaseModule()
        {
            SetupModelDefaults();
        }

        public BaseModule(string modulepath)
            : base(modulepath)
        {
            SetupModelDefaults();
        }


        private void SetupModelDefaults()
        {
            Before += ctx =>
            {
                Page = new PageModel()
                    {
                        PreFixTitle = "Universum Quiz - "
                    };

                Model.Page = Page;

                return null;
            };
        }
    }
}