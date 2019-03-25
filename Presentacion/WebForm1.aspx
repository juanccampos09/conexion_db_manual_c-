<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Presentacion.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        
        <asp:TextBox ID="txt_id" Visible="false" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_nombre" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_telefono" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Guardar Cliente" OnClick="Button1_Click" />

        <br /><br />

        <asp:GridView ID="grd_listaClientes" runat="server" AutoGenerateColumns="False" Width="546px" EmptyDataText="No hay registros">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Id" Visible="False" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                <asp:BoundField DataField="TELEFONO" HeaderText="Telefono" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("ID").ToString() %>' OnCommand="LinkButton1_Command">Editar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_eliminar" runat="server" CommandArgument='<%# Eval("ID").ToString() %>' OnCommand="lnk_eliminar_Command">Eliminar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        
    </form>
</body>
</html>
