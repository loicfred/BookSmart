using BookSmart.admin.manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSmart.Objects;

namespace BookSmart.Assets {
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            byte[] imageBytes = null;
            if (context.Request.QueryString["bookid"] != null) {
                imageBytes = BookObj.getBookById(int.Parse(context.Request.QueryString["bookid"])).Image;
            } else if (context.Request.QueryString["userid"] != null) {
                imageBytes = UserObj.getUserById(int.Parse(context.Request.QueryString["userid"])).Avatar;
            }
            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(imageBytes);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}