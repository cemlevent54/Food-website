<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Mesajlar.aspx.cs" Inherits="YemekSite.Menuler.Mesajlar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Panel ID="pnl_mesajlar_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_goster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_goster_Click" />
                    <asp:Button ID="btn_gizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gizle_Click" />
                </td>

                <td>
                    <asp:Label ID="lblkategori" runat="server" Text="Mesajlar Listesi" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_mesajlar" runat="server">
        <asp:DataList ID="dtlist_mesajlar" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("mesajlar_adsoyad") %>'></asp:Label>
                        </td>
                        
                        <td style="width: 100px;text-align:right;">
                            <asp:LinkButton ID="btnView" runat="server" OnClick="btnView_Click" CommandArgument='<%# Eval("mesajlar_id") %>'>
                                <asp:Image ID="img_view" runat="server" ImageUrl="~/images/view.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>

</asp:Content>


