using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWyrm
{
    public partial class Startup
    {
        public Startup()
        {
            Environment.SetEnvironmentVariable("URL", "http://localhost:54846");
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}