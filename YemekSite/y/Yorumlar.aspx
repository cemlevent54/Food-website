﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Yorumlar.aspx.cs" Inherits="YemekSite.Menuler.Yorumlar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <asp:Panel ID="pnl_onayliyorumliste_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_goster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_goster_Click" />
                    <asp:Button ID="btn_gizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gizle_Click" />
                </td>

                <td>
                    <asp:Label ID="lblkategori" runat="server" Text="Onaylı Yorumlar" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_onayliyorumliste" runat="server">
        <asp:DataList ID="dtlist_OnayliYorumlar" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("yorum_adsoyad") %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("yorum_id") %>' OnClientClick='return confirm("Silmeyi onaylıyor musunuz?");'>
                                <asp:Image ID="img_delete" runat="server" ImageUrl="~/images/deleteicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CommandArgument='<%# Eval("yorum_id") %>'>
                                <asp:Image ID="img_refresh" runat="server" ImageUrl="~/images/refreshicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>


    <asp:Panel ID="pnl_onaysizyorumliste_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_onaysiz_goster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_onaysiz_goster_Click" />
                    <asp:Button ID="btn_onaysiz_gizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_onaysiz_gizle_Click" />
                </td>

                <td>
                    <asp:Label ID="Label1" runat="server" Text="Onaysiz Yorumlar" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_onaysizyorumliste" runat="server">
        <asp:DataList ID="dtlist_onaysizyorum" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("yorum_adsoyad") %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("yorum_id") %>' OnClientClick='return confirm("Silmeyi onaylıyor musunuz?");'>
                                <asp:Image ID="img_delete" runat="server" ImageUrl="~/images/deleteicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CommandArgument='<%# Eval("yorum_id") %>'>
                                <asp:Image ID="img_refresh" runat="server" ImageUrl="~/images/refreshicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>


    <asp:Label ID="lblsonuc" runat="server" Text=""></asp:Label>
</asp:Content>
