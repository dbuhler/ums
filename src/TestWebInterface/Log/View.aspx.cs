﻿using API;
using System;
using System.Web.UI;


public partial class ViewLog : Page
{
    private Connection db;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            db = Repository.OpenConnection(Session["ConnectionString"] as string);
            gridViewData.DataSource = db.GetChangeLog();
            db.Close();

            gridViewData.DataBind();
        }
    }
}