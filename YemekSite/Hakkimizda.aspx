<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="Hakkimizda.aspx.cs" Inherits="YemekSite.Hakkimizda1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="display: flex;flex-direction:column; justify-content: center; align-items: center; text-align: center;">
        <table>
            <tr style="text-align: center">
                <td>
                    <h1>Biz Kimiz?</h1>
                </td>
            </tr>
            <tr style="text-align: center">
                <td>
                    <asp:Image ID="Image2" runat="server" Width="300px" Height="200px" ImageUrl="images\hakkimizda_resim.jpg" /></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lbl_hakkimizda" runat="server" Text=""></asp:Label>

                </td>
            </tr>
        </table>
    </div>



</asp:Content>
