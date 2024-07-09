<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Tarifler.aspx.cs" Inherits="YemekSite.Menuler.Tarifler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Panel ID="pnl_onaylitarifliste_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_goster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_goster_Click" />
                    <asp:Button ID="btn_gizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gizle_Click" />
                </td>

                <td>
                    <asp:Label ID="lblkategori" runat="server" Text="Onaylı Tarifler" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_onaylitarifliste" runat="server">
        <asp:DataList ID="dtlist_OnayliTarifler" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("tarif_ad") %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("tarif_id") %>' OnClientClick='return confirm("Silmeyi onaylıyor musunuz?");'>
                                <asp:Image ID="img_delete" runat="server" ImageUrl="~/images/deleteicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CommandArgument='<%# Eval("tarif_id") %>'>
                                <asp:Image ID="img_refresh" runat="server" ImageUrl="~/images/refreshicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>

    <asp:Panel ID="pnl_onaysiztarifliste_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_gosterme" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gosterme_Click" />
                    <asp:Button ID="btn_gizleme" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gizleme_Click" />
                </td>

                <td>
                    <asp:Label ID="Label1" runat="server" Text="Onaysız Tarifler" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_onaysiztarifliste" runat="server">
        <asp:DataList ID="dtlist_OnaysizTarifler" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("tarif_ad") %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("tarif_id") %>' OnClientClick='return confirm("Silmeyi onaylıyor musunuz?");'>
                                <asp:Image ID="img_delete" runat="server" ImageUrl="~/images/deleteicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CommandArgument='<%# Eval("tarif_id") %>'>
                                <asp:Image ID="img_refresh" runat="server" ImageUrl="~/images/refreshicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>

</asp:Content>
