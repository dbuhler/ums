using System;
using System.Web.UI;
using System.Web.UI.WebControls;


public static class Utils
{
    public static string GetPreviousPage(this Page page)
    {
        if (page.Request.UrlReferrer == null)
        {
            return null;
        }

        return page.Request.UrlReferrer.ToString();
    }


    public static string GetString(TextBox textBox)
    {
        string text = textBox.Text.Trim();

        if (text == string.Empty)
        {
            return null;
        }

        return text;
    }


    public static char? GetChar(TextBox textBox)
    {
        string text = textBox.Text.Trim();

        if (text == string.Empty)
        {
            return null;
        }

        return text[0];
    }


    public static char? GetChar(RadioButtonList list)
    {
        if (list.SelectedIndex == -1)
        {
            return null;
        }

        return char.Parse(list.SelectedValue);
    }


    public static DateTime? GetDate(TextBox textBox)
    {
        string text = textBox.Text.Trim();

        if (text == string.Empty)
        {
            return null;
        }

        return DateTime.Parse(text);
    }


    public static int? GetInt(DropDownList list)
    {
        if (list.SelectedIndex == 0)
        {
            return null;
        }

        return int.Parse(list.SelectedValue);
    }


    public static string ToString(char? c)
    {
        if (!c.HasValue)
        {
            return null;
        }

        return c.Value.ToString();
    }


    public static string ToString(int? n)
    {
        if (!n.HasValue)
        {
            return null;
        }

        return n.Value.ToString();
    }


    public static string ToString(DateTime? date)
    {
        if (!date.HasValue)
        {
            return null;
        }

        return date.Value.ToString("yyyy-MM-dd");
    }
}