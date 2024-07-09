<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="GununYemegi.aspx.cs" Inherits="YemekSite.GununYemegi1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex;flex-direction:column; justify-content: center; align-items: center; text-align: center;">
        <table style="width: 100%; border-collapse: collapse;">
            <tr>
                <td colspan="2" style="text-align: center; padding: 10px;">
                    <asp:Label ID="Label1" runat="server" Text="" Font-Size="30px" BackColor="#ffff99" ForeColor="#ff6600"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; padding: 10px;">
                    <asp:Image ID="Image1" runat="server" Width="200px" Height="200px" ImageUrl='' />
                </td>
            </tr>
            <tr>
                <td style="padding: 10px;">
                    <strong>Malzemeler: </strong>
                </td>
                <td style="padding: 10px;">
                    <asp:Label ID="Label2" runat="server" Text='' Font-Size="20px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px;">
                    <strong>Tarif: </strong>
                </td>
                <td style="padding: 10px;">
                    <asp:Label ID="Label3" runat="server" Text='' Font-Size="20px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px;">
                    <strong>Puan: </strong>
                </td>
                <td style="padding: 10px;">
                    <asp:Label ID="Label4" runat="server" Text='' Font-Size="20px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px;">
                    <strong>Eklenme Tarihi: </strong>
                </td>
                <td style="padding: 10px;">
                    <asp:Label ID="Label5" runat="server" Text='' Font-Size="20px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>




</asp:Content>

