<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="AnaSayfa.aspx.cs" Inherits="YemekSite.AnaSayfa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div style="display: flex;flex-direction:column; justify-content: center; align-items: center; text-align: center;">
        <h2>Ana Sayfa</h2>
        <asp:DataList ID="dtlist_AnaSayfa" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px" OnItemDataBound="dtlist_AnaSayfa_ItemDataBound">
            <ItemTemplate>
                <a style="text-decoration: none; color: inherit;" href='<%# "YemekDetay.aspx?yemek_id=" + Eval("yemek_id") %>'>
                    <asp:Label ID="LabelYemekAdi" runat="server" Text='<%# Eval("yemek_ad") %>' BackColor="#FFFF66" Font-Size="30px" ForeColor="#FF6666"></asp:Label>
                </a>
                <br />

                <asp:Image ID="Image1" runat="server" Width="200px" Height="200px" ImageUrl='<%# Eval("yemek_resim_base64") %>' /><br>

                <br />
                <table border="0" cellpadding="5" cellspacing="0" style="border-collapse: initial;">
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Eklenme Tarihi:" Font-Size="20px" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("yemek_tarih") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Yemek Puanı:" Font-Size="20px" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("yemek_puan") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </div>

    

</asp:Content>
