using Sitecore;
using Sitecore.Data;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseSwitcher.Shell.Framework.Commands
{
    public class SwitchDatabase : Command
    {
        public override void Execute(CommandContext context)
        {
            Sitecore.Data.Items.Item currentItem = context.Items[0];

            if (currentItem.Database.Name.ToLower() == "master")
            {

                Sitecore.Data.Database web = Sitecore.Configuration.Factory.GetDatabase("web");
                Sitecore.Data.Items.Item webItem = web.GetItem(currentItem.ID);

                if (webItem == null)
                {
                    SheerResponse.Alert("Item is not published yet to the web database", new string[0]);
                    return;
                }
                else
                {
                    string url = string.Format("/sitecore/shell/sitecore/content/Applications/Content Editor.aspx?sc_bw=1&id={0}&la=en&fo={0}&sc_content=web", currentItem.ID.ToString());
                    SheerResponse.Eval("window.top.location.href='" + url + "';");
                }

            }

            if (currentItem.Database.Name.ToLower() == "web")
            {
                Sitecore.Data.Database master = Sitecore.Configuration.Factory.GetDatabase("master");

                Sitecore.Data.Items.Item masterItem = master.GetItem(currentItem.ID);

                if (masterItem == null)
                {
                    SheerResponse.Alert("Item is not available in the master database", new string[0]);
                }
                else
                {
                    string url = string.Format("/sitecore/shell/sitecore/content/Applications/Content Editor.aspx?sc_bw=1&id={0}&la=en&fo={0}&sc_content=master", currentItem.ID.ToString());
                    SheerResponse.Eval("window.top.location.href='" + url + "';");

                }
            }

            return;
        }
    }
}