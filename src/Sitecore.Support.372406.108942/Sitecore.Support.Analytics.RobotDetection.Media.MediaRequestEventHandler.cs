using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.Analytics.RobotDetection.Media
{
    // Combined patches: 108942, 372406
    public class MediaRequestEventHandler : Sitecore.Analytics.RobotDetection.Media.MediaRequestEventHandler
    {

        /// <summary>
        /// this part comes from Sitecore.Support.372406.dll
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private bool IsPartialRequest(HttpContext httpContext)
        {
            string text = httpContext.Request.Headers[HttpWorkerRequest.GetKnownRequestHeaderName(37)];
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            if (text == "bytes=0-")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This part is a combination of Sitecore.Support.372406.dll and Sitecore.Support.108942.dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void OnMediaRequest(object sender, EventArgs args)
        {
            if (!IsPartialRequest(HttpContext.Current))
            {
                SiteContext site = Context.Site;
                if (site == null || site.DisplayMode == DisplayMode.Normal)
                {
                    base.OnMediaRequest(sender, args);
                }
            }
        }
    }
}