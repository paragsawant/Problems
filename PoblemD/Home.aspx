<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PoblemD.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="Btn_GetPolicies" runat="server" Text="GetPolicies" OnClick="Btn_GetPolicies_Click" /></td>
                    <td>
                        <asp:Button ID="Btn_EnrollPolicies" runat="server" Text="EnrollPolicy" OnClick="Btn_EnrollPolicies_Click" /></td>
                    <td>
                        <asp:Button ID="Btn_UpdatePolicy" runat="server" Text="UpdatePolicy" OnClick="Btn_UpdatePolicy_Click" /></td>
                    <td>
                        <asp:Button ID="Btn_CancelPolicy" runat="server" Text="CancelPolicy" OnClick="Btn_CancelPolicy_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:MultiView ID="MultiView1" runat="server">

            <asp:View ID="View1" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style4"><strong>Policy Details</strong></td>

                    </tr>
                    <tr>
                        <td class="auto-style4">Policy Holder Name</td>
                        <td>
                            <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Pet Name</td>
                        <td>
                            <asp:TextBox ID="txt_PetName" runat="server"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="auto-style4"></td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Next" />
                        </td>
                        <td></td>
                    </tr>
                </table>

            </asp:View>

        </asp:MultiView>
        <div>
        </div>
    </form>
</body>
</html>
