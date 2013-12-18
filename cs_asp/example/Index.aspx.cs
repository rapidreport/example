using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace example
{
    public partial class Index : System.Web.UI.Page
    {
        protected void BtnExample1_Click(object sender, EventArgs e)
        {
            Example1.Run(Server, Response);
        }
    }
}