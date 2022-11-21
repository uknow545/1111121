<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>


<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" Text="Button" OnClick="Unnamed2_Click"></asp:Button>
            <asp:TextBox runat="server" OnTextChanged="Unnamed2_TextChanged" ID="txb1"></asp:TextBox>
            <asp:GridView ID="gv1" runat="server" OnSelectedIndexChanged="gv1_SelectedIndexChanged" AllowCustomPaging="False" AlternatingRowStyle-HorizontalAlign="Left" AlternatingRowStyle-VerticalAlign="Middle" AlternatingRowStyle-BorderStyle="Dotted" AllowSorting="True" AllowPaging="False" EditRowStyle-BorderStyle="Groove" EmptyDataRowStyle-HorizontalAlign="Left" AutoGenerateColumns="False" AlternatingRowStyle-Wrap="False" PagerStyle-Wrap="False" HeaderStyle-Wrap="False" FooterStyle-Wrap="False" EditRowStyle-Wrap="False" EmptyDataRowStyle-BorderStyle="Ridge" EmptyDataRowStyle-Wrap="False" EnableTheming="False" EnableViewState="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle" RowStyle-Wrap="False">
  
          </asp:GridView>

                
        </div>
    </form>
</body>
</html>
