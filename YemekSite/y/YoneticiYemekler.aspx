<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="YoneticiYemekler.aspx.cs" Inherits="YemekSite.Menuler.YoneticiYemekler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .styled-textbox {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            font-size: 16px;
        }

            .styled-textbox:focus {
                border-color: #66afe9;
                outline: none;
                box-shadow: 0 0 8px rgba(102, 175, 233, 0.6);
            }

        .styled-button {
            background-color: lawngreen;
            color: white;
            padding: 10px 0; /* Adjust padding to fit within 300px */
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px; /* Adjust font size to fit within 300px */
            width: 130px; /* Set width to ensure two buttons fit within 300px */
            margin: 5px; /* Add margin for spacing between buttons */
            text-align: center;
            box-sizing: border-box;
        }

            .styled-button:hover {
                background-color: darkgreen;
            }
    </style>



    

    <asp:Panel ID="pnl_onayliyemekler_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_goster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_goster_Click" />
                    <asp:Button ID="btn_gizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gizle_Click" />
                </td>

                <td>
                    <asp:Label ID="lblkategori" runat="server" Text="Onaylı Yemekler" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_onayliyemekliste" runat="server">
        <asp:DataList ID="dtlist_OnayliYemekler" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("yemek_ad") %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("yemek_id") %>' OnClientClick='return confirm("Silmeyi onaylıyor musunuz?");'>
                                <asp:Image ID="img_delete" runat="server" ImageUrl="~/images/deleteicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CommandArgument='<%# Eval("yemek_id") %>'>
                                <asp:Image ID="img_refresh" runat="server" ImageUrl="~/images/refreshicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>


    <asp:Panel ID="pnl_onaysizyemekliste_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_gosterme" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gosterme_Click" />
                    <asp:Button ID="btn_gizleme" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_gizleme_Click" />
                </td>

                <td>
                    <asp:Label ID="Label1" runat="server" Text="Onaysız Yemekler" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_onaysizyemekliste" runat="server">
        <asp:DataList ID="dtlist_OnaysizYemekler" runat="server" CellPadding="4" ForeColor="Black" AlternatingItemStyle-BorderColor="Black" AlternatingItemStyle-BorderStyle="Solid" AlternatingItemStyle-BorderWidth="1px">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("yemek_ad") %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("yemek_id") %>' OnClientClick='return confirm("Silmeyi onaylıyor musunuz?");'>
                                <asp:Image ID="img_delete" runat="server" ImageUrl="~/images/deleteicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CommandArgument='<%# Eval("yemek_id") %>'>
                                <asp:Image ID="img_refresh" runat="server" ImageUrl="~/images/refreshicon.png" Width="30px" Height="30px" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>

    <asp:Panel ID="pnl_yemekekle_baslik" runat="server">
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Button ID="btn_yemekgoster" runat="server" Text="+" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_yemekgoster_Click" />
                    <asp:Button ID="btn_yemekgizle" runat="server" Text="-" Width="30px" Height="30px" CssClass="button-space" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px" Font-Bold="true" OnClick="btn_yemekgizle_Click" />
                </td>

                <td>
                    <asp:Label ID="Label2" runat="server" Text="Yemek Ekle" CssClass="text-right"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnl_yemekekle" runat="server">
        <table>
            <tr>
                <td>Yemek Adı:</td>
                <td>
                    <asp:TextBox ID="txtbox_yemekadi" runat="server" Width="250px" Font-Size="15px" CssClass="styled-textbox"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>Yemek Malzemeleri: </td>
                <td>
                    <asp:TextBox ID="txtbox_yemekmalzemeleri" runat="server" TextMode="MultiLine" Width="290px" Height="195px" Font-Size="15px" CssClass="styled-textbox" Font-Names="Arial"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>Yemek Tarifi: </td>
                <td>
                    <asp:TextBox ID="txtbox_yemektarifi" runat="server" TextMode="MultiLine" Width="290px" Height="195px" Font-Size="15px" CssClass="styled-textbox" Font-Names="Arial"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Yemek yayınlanma tarih: </td>
                <td>
                    <asp:TextBox ID="txtbox_yemektarihi" runat="server" Width="290px" Font-Size="15px" CssClass="styled-textbox" TextMode="DateTimeLocal" Font-Bold="true"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>Yemek puanı: </td>
                <td>
                    <asp:TextBox ID="txtbox_yemekpuan" runat="server" Width="290px" Font-Size="15px" CssClass="styled-textbox"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td>Yemek Kategori id: </td>
                <td>
                    <asp:TextBox ID="txtbox_kategoriId" runat="server" Width="290px" Font-Size="15px" CssClass="styled-textbox"></asp:TextBox>

                </td>
            </tr>--%>
            <tr>
                <td>Yemek Resim: </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="btn_resimekle" runat="server" Text="Resim Yükle" OnClick="btn_resimekle_Click" />
                    <asp:Image ID="img_yemekresim_yukleme" runat="server" Width="100px" Height="100px" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                </td>
            </tr>

            <tr>
                <td>Yemek Kategorisi Seçme: </td>
                <td>
                    <asp:DropDownList ID="dropdown_kategoriId" runat="server" Width="200px" Height="25px" Font-Size="20px" AutoPostBack="True"></asp:DropDownList>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btn_ekle" runat="server" Text="Ekle" CssClass="styled-button" OnClick="btn_ekle_Click" />
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lbl_mesaj" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Label ID="lblsonuc" runat="server" Text=""></asp:Label>
</asp:Content>
