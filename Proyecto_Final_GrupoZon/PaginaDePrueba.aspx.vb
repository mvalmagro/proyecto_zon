Public Class PaginaDePrueba
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblUsuario.Text = Session("usuario")
        lblPassword.Text = Session("password")
    End Sub

End Class