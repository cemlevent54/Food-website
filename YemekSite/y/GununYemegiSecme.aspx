<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="GununYemegiSecme.aspx.cs" Inherits="YemekSite.Menuler.GununYemegiSecme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnl_yemeklistesi_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_goster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_goster_Click" />
                    <asp:Button ID="btn_gizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gizle_Click" />
                </td>

                <td>
                    <asp:Label ID="lblkategori" runat="server" Text="Yemekler Listesi" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    <asp:Panel ID="pnl_yemekListesi" runat="server">
        <asp:DataList ID="dtlist_Yemekler" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("yemek_ad") %>'></asp:Label>
                        </td>
                        <td style="width: 100px;text-align:right;">
                            <asp:LinkButton ID="btnYemekSec" runat="server" OnClick="btnYemekSec_Click" CommandArgument='<%# Eval("yemek_id") %>' OnClientClick='return confirm("Günün yemeğini onaylıyor musunuz?");'>
                                <asp:Image ID="img_delete" runat="server" ImageUrl="~/images/checked.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                        
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>

    <asp:Label ID="lblsonuc" runat="server" Text=""></asp:Label>
</asp:Content>
