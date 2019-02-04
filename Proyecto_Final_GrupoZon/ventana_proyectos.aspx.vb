Imports System.Data.SqlClient

Public Class ventana_proyectos
    Inherits System.Web.UI.Page

    Private fechas_Calendario1() As Date
    Private titulos_eventos_Calendario1() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CargarTabla()

    End Sub



    Private Sub Calendar1_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles Calendar1.VisibleMonthChanged
        fechas_Calendario1 = Nothing
        titulos_eventos_Calendario1 = Nothing

        CargarTabla()

    End Sub


    Private Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged

        Dim i As Integer = 0

        While i < fechas_Calendario1.Count

            If Calendar1.SelectedDate = fechas_Calendario1(i) Then
                MsgBox(titulos_eventos_Calendario1(i))
            End If

            i = i + 1
        End While


    End Sub


    Private Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar1.DayRender
        Dim i As Integer = 0

        While i < fechas_Calendario1.Count

            If e.Day.Date = fechas_Calendario1(i) Then

                e.Cell.BackColor = Drawing.Color.Orange

            End If

            i = i + 1
        End While

    End Sub

    Private Sub btnUsuarios_Click(sender As Object, e As EventArgs) Handles btnUsuarios.Click
        Response.Redirect("Listado_usuarios.aspx")
    End Sub

    Private Sub CargarTabla()
        'Realizamos la conexión a la BBDD:

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet
        Dim query As String

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open() '-->abrimos la conexión

        query = "SELECT titulo, fecha_evento FROM proyectos"


        myCmd = New SqlCommand(query, oConexion)

        oDataAdapter = New SqlDataAdapter(myCmd)

        oDataAdapter.Fill(oDataSet, "proyectos")

        oDataAdapter = Nothing

        oConexion.Close()

        CargarArrays(oDataSet)
    End Sub

    Private Sub CargarArrays(oDataset As DataSet)
        Dim i As Integer = 0
        Dim oDataRow As DataRow

        ReDim fechas_Calendario1((oDataset.Tables("proyectos").Rows.Count) - 1)
        ReDim titulos_eventos_Calendario1((oDataset.Tables("proyectos").Rows.Count) - 1)

        While i < oDataset.Tables("proyectos").Rows.Count
            oDataRow = oDataset.Tables("proyectos").Rows(i)

            titulos_eventos_Calendario1(i) = oDataRow("titulo")
            fechas_Calendario1(i) = oDataRow("fecha_evento")

            oDataRow = Nothing
            i = i + 1
        End While

    End Sub



End Class