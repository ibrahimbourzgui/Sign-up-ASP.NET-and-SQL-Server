using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace sitewebPFF
{
    public partial class signup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnSeconnecter_Click(object sender, EventArgs e)
        {
            if(checkEmailExist())
            {
                Response.Write("<script>alert('Cet Email existe déja')</script>");
            }
            else
            {
                creercompte();
            }
        }

        bool checkEmailExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from client where email='"+TxtEmail.Text.Trim()+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count>=1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
               
                return false;
            }

            
        }
        void creercompte()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("insert into client values(@nom,@prenom,@cartenationale,@telephone,@email,@motdepasse,@adresse,@codepostal)", con);
                cmd.Parameters.AddWithValue("@nom", TxtNom.Text.Trim());
                cmd.Parameters.AddWithValue("@prenom", TxtPrenom.Text.Trim());
                cmd.Parameters.AddWithValue("@cartenationale", TxtCartenationale.Text.Trim());
                cmd.Parameters.AddWithValue("@telephone", TxtTelephone.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TxtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@motdepasse", Txtconfirm.Text.Trim());
                cmd.Parameters.AddWithValue("@adresse", TxtAdresse.Text.Trim());
                cmd.Parameters.AddWithValue("@codepostal", TxtCodepostal.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('création de compte effectuer avec succés')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}