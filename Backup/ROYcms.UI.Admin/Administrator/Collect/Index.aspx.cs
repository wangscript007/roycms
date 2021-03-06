﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Threading;

namespace ROYcms.UI.Admin.Administrator.Caiji
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Rss : BasePage
    {

        ROYcms.Sys.BLL.ROYcms_CaiRss Bll = new Sys.BLL.ROYcms_CaiRss();
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ROYcms.Common.Request.GetQueryInt("del") > 0)
                {
                    Bll.Delete(ROYcms.Common.Request.GetQueryInt("del"));
                    PreIndex();
                    Bind();
                }
                else
                {
                    PreIndex();
                    Bind();
                }
            }
        }
        /// <summary>
        /// 初始值赋值
        /// </summary>
        public void PreIndex()
        {
            ViewState["Page"] = ROYcms.Common.Request.GetQueryString("Page") == "" ? "1" : ROYcms.Common.Request.GetQueryString("Page");
            Application["PageSize"] = Application["PageSize"] == null ? "20" : Application["PageSize"];
        }
        /// <summary>
        /// Binds this instance.
        /// </summary>
        void Bind()
        {
            string where = null;

            where += " Id>-1 ";

            //try
            //{
            Repeater_admin.DataSource = Bll.GetList(Convert.ToInt32(Application["PageSize"]), Convert.ToInt32(ViewState["Page"]), where);
            Repeater_admin.DataBind();
            ViewState["PageCon"] = Bll.GetRecordCount(where) / Convert.ToInt32(Application["PageSize"]) + 1;
            ViewState["PageContent"] = Bll.GetRecordCount(where);
            Bll.GetList("");
            //}
            //catch //异常处理
            //{
            //    Repeater_admin.DataSource = null;
            //    Repeater_admin.DataBind();
            //}

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PageSizeTextChanged(object sender, EventArgs e)
        {
            Application["PageSize"] = ((TextBox)Repeater_admin.Controls[Repeater_admin.Controls.Count - 1].FindControl("PageSize")).Text;
            Bind();
        }


    }
}
